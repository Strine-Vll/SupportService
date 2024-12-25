import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ServiceRequestOverview, ServiceRequestPreview } from '../interfaces/ServiceRequest';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  /*getGroups(userId: number): Observable<Group[]> {
    let params = new HttpParams().set('userId', userId.toString());

    return this.http.get<Group[]>(this.baseUrl + '/group', { params })
    .pipe(
      map(data => data)
    );
  }*/

  getRequests(groupId: number): Observable<ServiceRequestPreview[]> {
    const requests: ServiceRequestPreview[] = [
        { id: 1, title: 'Test Task', status: ''},
        { id: 2, title: 'Test Task', status: ''},
        { id: 3, title: 'Test Task', status: ''}
    ];
    
    return of(requests).pipe(
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
}
