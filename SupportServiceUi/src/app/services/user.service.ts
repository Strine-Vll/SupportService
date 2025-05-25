import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { catchError, map, Observable, throwError } from 'rxjs';
import { EditUser, UserPreview } from '../interfaces/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

  getGroupUsers(groupId: number){
    let params = new HttpParams().set('groupId', groupId);
    
    return this.http.get<UserPreview[]>(this.baseUrl + '/user/groupusers', { params })
    .pipe(
      map(data => data)
    );
  }

  getUsersToInvite(groupId: number){
    let params = new HttpParams().set('groupId', groupId);
    
    return this.http.get<UserPreview[]>(this.baseUrl + '/user/UsersToInvite', { params })
    .pipe(
      map(data => data)
    );
  }

  getUsersToManage() {
    return this.http.get<UserPreview[]>(this.baseUrl + '/user/UsersToManage')
    .pipe(
      map(data => data)
    );
  }

  getActiveUsers() {
    return this.http.get<UserPreview[]>(this.baseUrl + '/user/ActiveUsers')
    .pipe(
      map(data => data)
    );
  }

  getUserToEdit(userId: number) {
    let params = new HttpParams().set('userId', userId);
    
    return this.http.get<EditUser>(this.baseUrl + '/user/GetEditUser', { params })
    .pipe(
      map(data => data)
    );
  }

  register(name: string, email: string, password: string){
    var body = {
      Name: name,
      Email: email,
      Password: password
    }

    return this.http.post(this.baseUrl + '/auth', body);
  }

  login(email: string, password: string){
    var body = {
      Email: email,
      Password: password
    }

    return this.http.post(this.baseUrl + '/auth/login', body);
  }

  editUser(user: EditUser) {
    const url = `${this.baseUrl}/user/UpdateUser`;

    return this.http.put<any>(url, user).pipe(
      catchError(error => {
        console.error('Ошибка при обновлении пользователя:', error);
        return throwError(() => error);
      })
    );
  }

  deactivateUser(userId: number) {
    return this.http.post<any>(`${this.baseUrl}/user/DeactivateUser?userId=${userId}`, {}).pipe(
      catchError(error => {
        console.error('Ошибка при деактивации пользователя:', error);
        return throwError(() => error);
      })
    );
  }
}