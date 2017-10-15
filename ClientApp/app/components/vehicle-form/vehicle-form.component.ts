import { Component, OnInit } from '@angular/core';
import { MakeService } from './../../services/make.service'
import { ActivatedRoute, Router } from '@angular/router';

    @Component({
   selector: 'app-vehicle-form',
          templateUrl: './vehicle-form.component.html',
          styleUrls: ['./vehicle-form.component.css']
        })
    export class VehicleFormComponent implements OnInit {
        makes: any[]; 
 
        constructor(private makeService: MakeService) { }

        ngOnInit() {
         
            this.makeService.getMakes().subscribe(makes => {
                this.makes = makes;
                console.log(this.makes);

            });
                
      }

    }