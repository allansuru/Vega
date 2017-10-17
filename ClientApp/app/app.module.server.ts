import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { sharedConfig } from './app.module.shared';
import { MakeService } from './services/make.service';
import { FeatureService } from './services/feature.service';

@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: sharedConfig.declarations,
    imports: [

        ServerModule,
        ...sharedConfig.imports
    ],
    providers: [
       
      //  MakeService,
     //   FeatureService
    ]
})
export class AppModule {
}
