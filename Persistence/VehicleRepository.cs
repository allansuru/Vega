﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includedRelated = true)
        {
            if (!includedRelated)
                return await context.Vehicles.FindAsync(id);


            return await context.Vehicles
                 .Include(v => v.Features)
                   .ThenInclude(vf => vf.Feature)
                 .Include(v => v.Model)
                   .ThenInclude(m => m.Make)
                 .SingleOrDefaultAsync(v => v.Id == id);

        }

   
 
        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }
        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}
