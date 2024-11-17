import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  baseUrl: string = environment.apiBaseUrl;

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