import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { map, Observable } from 'rxjs';
import { UserPreview } from '../interfaces/User';

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
}