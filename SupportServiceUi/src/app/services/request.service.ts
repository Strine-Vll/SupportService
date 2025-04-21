import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CreateServiceRequestDto, ServiceRequestOverview, ServiceRequestPreview } from '../interfaces/ServiceRequest';

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
    const exampleRequest: ServiceRequestOverview = {
        id: requestId,
        title: "Запрос на обслуживание",
        description: "При заходе на сайт отображается ошибка 401. Логин и пароль ввожу правильные, проверял. После сброса и повторного входа та же ошибка.",
        status: "В обработке",
        createdDate: new Date("2024-12-01T10:00:00"),
        updatedDate: new Date("2024-12-25T15:30:00"),
        createdBy: "Иван Иванов",
        appointed: "Сергей Сергеев"
    };
    
    return of(exampleRequest);
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
}
