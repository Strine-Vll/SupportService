import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private jwtHelper: JwtHelperService;

  constructor() {
    this.jwtHelper = new JwtHelperService();
  }

  public getToken() : string | null {
    return localStorage.getItem('token');
  }

  public decodeToken(): any {
    var token = this.getToken();
    if (token != null){
      return this.jwtHelper.decodeToken(token);
    }
  }

  public getUserId(): string {
    const decodedToken = this.decodeToken();
    return decodedToken['userId'];
  }

  public getUserRole(): string {
    const decodedToken = this.decodeToken();
    return decodedToken['role'] || '';
  }

  public getClaim(claimKey: string): any {
    const decodedToken = this.decodeToken();
    return decodedToken ? decodedToken[claimKey] : null;
  }

  public hasRole(expectedRole: string): boolean {
    return this.getUserRole() === expectedRole;
  }

  public hasAnyRole(expectedRoles: string[]): boolean {
    return expectedRoles.includes(this.getUserRole());
  }

  public isAuthenticated(): boolean {
    return !!this.getToken();
  }
}