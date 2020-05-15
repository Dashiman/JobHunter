import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Users } from '../models/users';
import { Observable } from 'rxjs';
import { PlatformLocation } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  baseUrl: string;
  constructor(private http: HttpClient, pl: PlatformLocation) {
    this.baseUrl = window.location.origin;
  }


  register(user: Users): Observable<number> {
    console.log(window.location.origin);
    return this.http.post<number>(this.baseUrl + "/api/registration/CreateAccount", user).pipe(
      (res) => {
        return res;
      }
    )
  }
}
