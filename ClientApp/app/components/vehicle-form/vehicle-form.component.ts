import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from './../../services/vehicle.service';



@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
    makes: any[]; // add find, entre outros no objeto makes
    models: any[];
    vehicle: any = {}; //objeto em branco
    features: any[];

    constructor(
        private vehicleService: VehicleService,
        
    ) { }

    ngOnInit() {
        
        this.vehicleService.getMakes().subscribe(makes => {
            this.makes = makes;

     
        });

        this.vehicleService.getFeatures().subscribe(features => {
            this.features = features;
            //console.log(features);
        });  

      

    }
    onMakeChange() {
        // console.log(this.vehicle);
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];

        //if (selectedMake.id == 5)
        //{
        //    selectedMake.ativo = true;
        //}

       



    }

}