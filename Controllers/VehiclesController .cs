using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Vega.Controllers.Resources;
using Vega.Core.Models;
using Vega.Core;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {

         //  throw new Exception();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            #region Exemplo de ModelState.AddModelError

            //var model = await context.Models.FindAsync(vehicleResource.ModelId);

            //if (model == null)
            //{
            //    ModelState.AddModelError("modelId", "Invalid Model ID");
            //    return BadRequest(ModelState);
            //}
            #endregion

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repository.Add(vehicle);
            await unitOfWork.CompleteAsyc();

            //Get no Vehicle inteiro
            #region AntesdoRepository
            //vehicle = await context.Vehicles
            //  .Include(v => v.Features)
            //    .ThenInclude(vf => vf.Feature)
            //  .Include(v => v.Model)
            //    .ThenInclude(m => m.Make)
            //  .SingleOrDefaultAsync(v => v.Id == vehicle.Id);
            #endregion

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Get no Vehicle inteiro
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsyc();

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includedRelated: false);

            if (vehicle == null)
                return NotFound();

            repository.Remove(vehicle);
            await unitOfWork.CompleteAsyc();

            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            //Get no Vehicle inteiro
            var vehicle = await repository.GetVehicle(id);


            if (vehicle == null)
                return NotFound();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

    }
}