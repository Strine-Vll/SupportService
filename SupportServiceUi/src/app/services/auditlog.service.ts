import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Comment, CommentToCreate } from '../interfaces/Comment';
import { AuditLog } from '../interfaces/AuditLog';

@Injectable({
  providedIn: 'root'
})

export class AuditLogService {

  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  getLogs(requestId: number): Observable<AuditLog[]> {
    let params = new HttpParams().set('requestId', requestId);

    return this.http.get<AuditLog[]>(this.baseUrl + '/AuditLog', { params })
    .pipe(
      map(data => data)
    );
  }
}