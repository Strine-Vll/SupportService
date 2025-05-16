import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { CreateNotification, Notification } from '../interfaces/Notification';
import { Observable, catchError, map, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  getNotifications(userId: string): Observable<Notification[]> {
    let params = new HttpParams().set('userId', userId);

    return this.http.get<Notification[]>(this.baseUrl + '/notification', { params })
    .pipe(
      map(data => data)
    );
  }

  createNotification(notification: CreateNotification): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/notification`, notification).pipe(
        catchError(error => {
            console.error('Ошибка при создании оповещения:', error);
            return throwError(error);
        })
    );
  }

  getNotificationCount(userId: string): Observable<number> {
    let params = new HttpParams().set('userId', userId);

    return this.http.get<number>(this.baseUrl + '/notification/count', { params })
    .pipe(
      map(data => data)
    );
  }

  deleteNotification(id: string): Observable<any> {
    let params = new HttpParams().set('id', id);

    return this.http.delete<number>(this.baseUrl + '/notification', { params })
    .pipe(
      map(data => data)
    );
  }
}
