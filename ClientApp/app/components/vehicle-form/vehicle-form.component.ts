import { Component, OnInit } from '@angular/core';
import { MakeService } from './../../services/make.service'
import { ActivatedRoute, Router } from '@angular/router';

    @Component({
   selector: 'app-vehicle-form',
          templateUrl: './vehicle-form.component.html',
          styleUrls: ['./vehicle-form.component.css']
        })
    export class VehicleFormComponent implements OnInit {
        makes: any[]; // add find, entre outros no objeto makes
        models: any[]; 
        vehicle: any = {}; //objeto em branco
 
        constructor(private makeService: MakeService) { }

        ngOnInit() {
         
            this.makeService.getMakes().subscribe(makes => {
                this.makes = makes;
                //console.log(this.makes);

            });
                
        }
        onMakeChange()
        {
            // console.log(this.vehicle);
            var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
            this.models = selectedMake ? selectedMake.models : [];

        }

    }