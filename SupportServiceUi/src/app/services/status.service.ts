import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { map, Observable } from 'rxjs';
import { UserPreview } from '../interfaces/User';
import { Status } from '../interfaces/Status';

@Injectable({
  providedIn: 'root'
})
export class StatusService {
  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  getStatuses(){
    return this.http.get<Status[]>(this.baseUrl + '/Status/all')
    .pipe(
      map(data => data)
    );
  }
}