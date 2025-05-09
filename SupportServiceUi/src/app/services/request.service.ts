import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CreateServiceRequestDto, EditServiceRequest, ServiceRequestOverview, ServiceRequestPreview } from '../interfaces/ServiceRequest';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  getRequests(groupId: number): Observable<ServiceRequestPreview[]> {
    let params = new HttpParams().set('groupId', groupId.toString());

    return this.http.get<ServiceRequestPreview[]>(this.baseUrl + '/servicerequest/requestspreview', { params })
    .pipe(
      map(data => data)
    );
  }

  getRequest(requestId: number): Observable<ServiceRequestOverview> {
    let params = new HttpParams().set('requestId', requestId.toString());
    
    return this.http.get<ServiceRequestOverview>(this.baseUrl + '/ServiceRequest/GetOverview', { params })
    .pipe(
      map(data => data)
    );
  }

  getEditRequest(requestId: number): Observable<EditServiceRequest> {
    let params = new HttpParams().set('requestId', requestId.toString());
    
    return this.http.get<EditServiceRequest>(this.baseUrl + '/ServiceRequest/GetEditRequest', { params })
    .pipe(
      map(data => data)
    );
  }

  createRequest(serviceRequest: CreateServiceRequestDto): Observable<any> {
    const body = {
      title: serviceRequest.title,
      description: serviceRequest.description,
      groupId: serviceRequest.groupId,
      createdById: serviceRequest.createdById,
      appointedId: serviceRequest.appointedId
    };

    return this.http.post<any>(`${this.baseUrl}/servicerequest`, body).pipe(
        catchError(error => {
            console.error('Ошибка при создании группы:', error);
            return throwError(error);
        })
    );
  }

  editRequest(request: EditServiceRequest) : Observable<any> {
    const url = `${this.baseUrl}/servicerequest/UpdateRequest`;

    return this.http.put<any>(url, request).pipe(
      catchError(error => {
        console.error('Ошибка при обновлении запроса:', error);
        return throwError(() => error);
      })
    );
  }
}
