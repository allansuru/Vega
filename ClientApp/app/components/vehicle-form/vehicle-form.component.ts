import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from './../../services/vehicle.service';
import { ToastyService } from "ng2-toasty";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';



@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
    makes: any[]; // add find, entre outros no objeto makes
    models: any[];
    features: any[];
    vehicle: any = {
        features: [],
        contact: {
          
        }
    }; 

    constructor(
        private route: ActivatedRoute,
        private router : Router,
        private vehicleService: VehicleService,
        private toastyService: ToastyService


        
    ) { 
        route.params.subscribe(p => {
            this.vehicle.id = +p['id'] || 0;
        });
    }

    ngOnInit() {

        var sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures(),
        ];

        if (this.vehicle.id)
            sources.push(this.vehicleService.getVehicle(this.vehicle.id));

        //preencho aqui meus objetos oriundo do array sources, joguei dentro desse array!! boas praticas, poderia dentro do forkJoin([..,..,..])
        Observable.forkJoin(sources).subscribe(data => {
            this.makes = data[0];
            this.features = data[1];

            if(this.vehicle.id)
                 this.vehicle = data[2];
            }, err => {
                     if (err.status == 404)
                            this.router.navigate(['/home']);
                    

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
            }));

    }

}