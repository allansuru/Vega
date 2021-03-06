﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vega.Extensions;
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

        public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var query = context.Vehicles
            .Include(v => v.Model)
              .ThenInclude(m => m.Make)
            .Include(v => v.Features)
              .ThenInclude(vf => vf.Feature)
              .AsQueryable();

          
           

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

            if (queryObj.ModelId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.ModelId.Value);

       
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {

                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contacName"] = v => v.ContactName
             

            };

            //estudar esse cara
            query = query.ApplyOrdering(queryObj, columnsMap);
            
            return await query.ToListAsync();
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
