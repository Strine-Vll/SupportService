import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Group } from '../interfaces/Group';
import { UserPreview } from '../interfaces/User';

@Injectable({
  providedIn: 'root'
})

export class GroupService {

  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  getGroups(userId: number): Observable<Group[]> {
    let params = new HttpParams().set('userId', userId.toString());

    return this.http.get<Group[]>(this.baseUrl + '/group/groups', { params })
    .pipe(
      map(data => data)
    );
  }

  getGroup(groupId: number): Observable<Group> {
    let params = new HttpParams().set('groupId', groupId.toString());

    return this.http.get<Group>(this.baseUrl + '/group', { params })
    .pipe(
      map(data => data)
    );
  }

  createGroup(group: Group, userId: number): Observable<any> {
    const body = {
        Name: group.name
    };

    return this.http.post<any>(`${this.baseUrl}/group?userId=${userId}`, body).pipe(
        catchError(error => {
            console.error('Ошибка при создании группы:', error);
            return throwError(error);
        })
    );
  }

  updateUserList(users: UserPreview[], groupId: number): Observable<any> {
    console.log(users, groupId);

    const url = `${this.baseUrl}/group/updateUsers?groupId=${groupId}`;
    return this.http.post<any>(url, users).pipe(
      catchError(error => {
        console.error('Ошибка при изменении списка пользователей:', error);
        return throwError(() => error);
      })
    );
  }

  updateGroup(group: Group): Observable<any> {
    const body = {
        Name: group.name
    };

    return this.http.put<any>(`${this.baseUrl}/group/update`, group).pipe(
        catchError(error => {
            console.error('Ошибка при создании группы:', error);
            return throwError(error);
        })
    );
  }

  deleteGroup(groupId: number): Observable<any> {
    let params = new HttpParams().set('groupId', groupId);

    return this.http.delete<any>(`${this.baseUrl}/group`, { params }).pipe(
      catchError(error => {
        console.error('Ошибка при удалении группы:', error);
        return throwError(() => error);
      })
    );
  }
}