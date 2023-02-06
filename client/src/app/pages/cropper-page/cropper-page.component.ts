import {
  Component,
  OnDestroy,
  ViewChild,
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ImageApiService } from '../../api/image-api.service';
import { MatStepper } from '@angular/material/stepper';
import {
  forkJoin,
  Observable,
  Subject,
  switchMap,
  takeUntil,
} from 'rxjs';
import { ImageTransform } from 'ngx-image-cropper/lib/interfaces';
import { ImageCropperComponent } from 'ngx-image-cropper/lib/component/image-cropper.component';
import { CropOptions } from '../../models/crop-options';
import { bytesToBase64 } from '../../utils/imgParser';

@Component({
  selector: 'app-cropper-page',
  templateUrl: './cropper-page.component.html',
  styleUrls: ['./cropper-page.component.scss'],
})
export class CropperPageComponent implements OnDestroy {
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('cropper') cropper: ImageCropperComponent;

  isLinear = false;

  imageKey: string;
  image: string;
  croppedImageKey: string;
  croppedImage: string;

  private readonly destroy$ = new Subject<void>();

  constructor(
    private readonly imageApi: ImageApiService,
    private _formBuilder: FormBuilder,
  ) {
    //
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  transform(e: ImageTransform): void {
    console.log(e)
  }

  crop(): void {
    const options: CropOptions = {
      startPoint: {
        x: this.cropper.cropper.x1,
        y: this.cropper.cropper.y1,
      },
      width: this.cropper.cropper.x2 - this.cropper.cropper.x1,
      height: this.cropper.cropper.y2 - this.cropper.cropper.y1,
    }
    this.imageApi
      .cropImage(this.imageKey, options)
      .pipe(
        switchMap(res => {
          this.croppedImageKey = res;
          this.stepper.next();
          return forkJoin([this.getImage(this.imageKey), this.getImage(this.croppedImageKey)]) ;
        }),
        takeUntil(this.destroy$),
      )
      .subscribe(([img, croppedImg]) => {
        this.image = bytesToBase64(img);
        this.croppedImage = bytesToBase64(croppedImg);
      });
  }

  selectImg(event: any): void {
    const file = event.target.files[0];
    this.imageApi
      .uploadImage(file)
      .pipe(
        switchMap(res => {
          this.imageKey = res;
          this.stepper.next();
          return this.getImage(this.imageKey);
        }),
        takeUntil(this.destroy$),
      )
      .subscribe((res) => this.image = bytesToBase64(res));
  }

  private getImage(key: string): Observable<string> {
    return this.imageApi.getImage(key);
  }

  reset(): void {
    this.image = null;
    this.imageKey = null;
    this.croppedImage = null;
    this.croppedImageKey = null;
    this.stepper.reset();
  }
}
