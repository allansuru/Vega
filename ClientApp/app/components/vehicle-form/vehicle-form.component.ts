import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from './../../services/vehicle.service';
import { ToastyService } from "ng2-toasty";



@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
    makes: any[]; // add find, entre outros no objeto makes
    models: any[];
    vehicle: any = {
        features: [],
        contact: {
          
        }
    }; 
    features: any[];
  

    vehicles: any[];

    constructor(
        private vehicleService: VehicleService,
        private toastyService: ToastyService
        
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
        delete this.vehicle.modelId;

        //if (selectedMake.id == 5)
        //{
        //    selectedMake.ativo = true;
        //}

    }
    onFeaturesToggle(featureId, $event)
    {
        if ($event.target.checked)
            this.vehicle.features.push(featureId);
        else
        {
            var index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }
    }
    submit()
    {

        this.vehicleService.create(this.vehicle)
            .subscribe(x => this.toastyService.success({

                title: 'Sucesso',
                msg: 'Veículo Salvo com sucesso!',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            }),
            err => {
                this.toastyService.error({
                    title: 'Error 2 ',
                    msg: 'An unexpected error happened. ',
                    theme: 'bootstrap',
                    showClose: true,
                    timeout : 5000

                })
            });

    }

}