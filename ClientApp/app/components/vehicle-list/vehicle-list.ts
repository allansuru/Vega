import { Vehicle, KeyValuePair  } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';


@Component({
    templateUrl: 'vehicle-list.html'
})

export class VehicleListComponent implements OnInit
{
    vehicles: Vehicle[];
    makes: KeyValuePair[];
    AllVehicles: Vehicle[];
    filter: any = {};
    


    constructor(private vehicleService: VehicleService)
    {}

    ngOnInit()
    {
        this.vehicleService.getMakes()
            .subscribe(m => this.makes = m);



        this.vehicleService.getVehicles()
            .subscribe(v =>  this.vehicles = this.AllVehicles = v);

  
    }
    onFilterChange()
    {
       
        let vehicles = this.AllVehicles;

        if (this.filter.makeId)
            vehicles = vehicles.filter(v => v.make.id == this.filter.makeId)

        if (this.filter.modelId)
            vehicles = vehicles.filter(v => v.model.id == this.filter.modelId)

        this.vehicles = vehicles;
    }

    resetFilter()
    {
        this.filter = {}; 
        this.onFilterChange(); 
    }
}