import { NgModule, ErrorHandler } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { sharedConfig } from './app.module.shared';
import { FormsModule } from '@angular/forms';
import { ToastyModule } from 'ng2-toasty';
import { AppErrorHandler } from './app.error-handler';
import { VehicleService } from './services/vehicle.service';





@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: sharedConfig.declarations,
    imports: [

        ServerModule,
        FormsModule,
             ToastyModule.forRoot(),
        ...sharedConfig.imports
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler }, 
      VehicleService,
     //   FeatureService
    ]
})
export class AppModule {
}
