﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includedRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery filter);

    }
}
