import { Injectable } from '@angular/core';
import { ConfigurationService } from '../configuration/configuration.service';

@Injectable({
  providedIn: 'root'
})
export class BaseApiService {
  protected baseUrl: string;

  constructor(
    configService: ConfigurationService
  ) {
    this.baseUrl = configService.settings.api.url;
  }
}
