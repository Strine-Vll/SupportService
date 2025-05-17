import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Stat, StatFilter } from '../interfaces/Stat';

@Injectable({
  providedIn: 'root'
})

export class StatService {

  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  getRequestStat(requestId: number): Observable<Stat> {
    return this.http.get<Stat>(`${this.baseUrl}/ServiceRequestStat?id=${requestId}`)
    .pipe(
      map(data => data)
    );
  }

  filterStat(filter: StatFilter): Observable<Stat[]> {
    const params = new HttpParams()
        .set('startDate', filter.startDate ? filter.startDate.toISOString() : '')
        .set('endDate', filter.endDate ? filter.endDate.toISOString() : '')
        .set('userId', filter.userId?.toString() ?? '');

    return this.http.get<Stat[]>(`${this.baseUrl}/ServiceRequestStat/filter`, { params })
      .pipe(
        map(data => data)
      );
  }
}