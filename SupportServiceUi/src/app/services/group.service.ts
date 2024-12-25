import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Group } from '../interfaces/Group';

@Injectable({
  providedIn: 'root'
})

export class GroupService {

  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  /*getGroups(userId: number): Observable<Group[]> {
    let params = new HttpParams().set('userId', userId.toString());

    return this.http.get<Group[]>(this.baseUrl + '/group', { params })
    .pipe(
      map(data => data)
    );
  }*/

    getGroups(userId: number): Observable<Group[]> {
      // Захардкодженные группы
      const groups: Group[] = [
          { id: 1, name: 'Group A' },
          { id: 2, name: 'Group B' },
          { id: 3, name: 'Group C' }
      ];

      // Возвращаем группы как Observable
      return of(groups).pipe(
          map(data => data) // Здесь можно добавить дополнительные преобразования, если нужно
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
}