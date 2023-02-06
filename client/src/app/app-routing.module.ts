import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CropperPageComponent } from './pages/cropper-page/cropper-page.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'cropper',
    pathMatch: 'full',
  },
  {
    path: 'cropper',
    component: CropperPageComponent,
    pathMatch: 'full',
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
