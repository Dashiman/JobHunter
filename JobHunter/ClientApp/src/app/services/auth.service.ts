import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PlatformLocation } from '@angular/common';
import { Users } from '../models/users';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { map } from "rxjs/operators";
import { session } from "../models/session";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl: string;

  constructor(private http: HttpClient, pl: PlatformLocation) {
    this.baseUrl = window.location.origin;
  }



  loginUser(user: Users): Observable<number> {
    return this.http.post<number>(this.baseUrl + "/api/auth/Login", user).pipe(
      (res) => {
        return res;
      }
    )
  }
  getAuthority(): Observable<number> {
    return this.http.get(this.baseUrl + "/api/auth/GetAuthority").pipe(map(res => { return res as number }));
  }
  getUserId(): Observable<number> {
    return this.http.get(this.baseUrl + "/api/auth/GetUserId").pipe(map(res => { return res as number }));
  }
  getUserName(): Observable<session> {
    return this.http.get(this.baseUrl + "/api/auth/GetUsername").pipe(map(res => { return res as session}));
  }
  logout(): Observable<number> {
    return this.http.get(this.baseUrl + "/api/auth/Logout").pipe(map(res => { return res as number }));
  }
  isLoggedIn(): Observable<boolean> {
    return this.http.get(this.baseUrl + "/api/auth/CanAccess").pipe(map(res => { return res as boolean }));
  }

}
