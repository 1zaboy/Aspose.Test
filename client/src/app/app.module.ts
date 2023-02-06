import {
  APP_INITIALIZER,
  NgModule,
} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ConfigurationService } from './configuration/configuration.service';
import { HttpClientModule } from '@angular/common/http';
import { CropperPageComponent } from './pages/cropper-page/cropper-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const appInitializerFn = (appConfig: ConfigurationService) => {
  return () => {
    return appConfig.loadConfig();
  };
};

@NgModule({
  declarations: [
    AppComponent,
    CropperPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatStepperModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    ImageCropperModule,
    HttpClientModule,
    BrowserAnimationsModule,
  ],
  providers: [
    ConfigurationService,
    {provide: APP_INITIALIZER, useFactory: appInitializerFn, multi: true, deps: [ConfigurationService]},
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
