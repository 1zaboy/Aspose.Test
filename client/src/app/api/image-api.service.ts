import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { ConfigurationService } from '../configuration/configuration.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CropOptions } from '../models/crop-options';

@Injectable({
  providedIn: 'root',
})
export class ImageApiService extends BaseApiService {

  private readonly UPLOAD_IMAGE = 'api/Image/load';
  private readonly CROP_IMAGE = 'api/Image/crop';
  private readonly LOAD_IMAGE = 'api/Image';

  constructor(
    private _http: HttpClient,
    configService: ConfigurationService,
  ) {
    super(configService);
  }

  uploadImage(file: File): Observable<string> {
    const image = new FormData();
    image.append('image', file, file.name);
    return this._http.post<string>(`${this.baseUrl}${this.UPLOAD_IMAGE}`, image);
  }

  cropImage(imageKey: string, options: CropOptions): Observable<string> {
    return this._http.post<string>(`${this.baseUrl}${this.CROP_IMAGE}/${imageKey}`, options);
  }

  getImage(key: string): Observable<string> {
    return this._http.get<string>(`${this.baseUrl}${this.LOAD_IMAGE}/${key}`);
  }
}
