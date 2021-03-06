import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {BehaviorSubject, Observable, retry} from 'rxjs';
import {environment} from "./environment";
const AUTH_API = environment.baseUrl + 'User/login';
const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {
    let token = this.getToken();
    if(token.length > 0)
      this.isUserLoggedIn.next(true);
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post(AUTH_API, { 'userName':username, 'password':password});
  }

  logOff(): void {
    window.localStorage.clear();
    this.isUserLoggedIn.next(false);
  }

  public getToken(): string {
    let result = window.localStorage.getItem(TOKEN_KEY);
    if (result === null || result == undefined)
      return '';
    return result;
  }

  public saveLogin(user: any, token:string): void {
    window.localStorage.removeItem(TOKEN_KEY);
    window.localStorage.setItem(TOKEN_KEY, token);
    window.localStorage.removeItem(USER_KEY);
    window.localStorage.setItem(USER_KEY, JSON.stringify(user));
    this.isUserLoggedIn.next(true);
  }
  public getUser(): any {
    const user = window.localStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }
    return {};
  }

}
