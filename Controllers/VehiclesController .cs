using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext context;
        public VehiclesController(IMapper mapper, VegaDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await context.Models.FindAsync(vehicleResource.ModelId);

            if(model == null)
            {
                ModelState.AddModelError("modelId", "Invalid Model ID");
                return BadRequest(ModelState);
            }


            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

    }
}