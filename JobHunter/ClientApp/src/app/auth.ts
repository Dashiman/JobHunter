import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from "./services/auth.service";

@Injectable()
export class AuthGuardService implements CanActivate {
  baseUrl: string;
  constructor(public auth: AuthService, public router: Router, private http: HttpClient) {
    this.baseUrl = window.location.origin;

  }
  canActivate(): boolean {
    var result = false;
    this.http.get(this.baseUrl + "/api/auth/CanAccess").subscribe(res => {

        result = res as boolean;
        if (result == false) {

          this.router.navigate(['/login']);
          return result
        }
        if (result == true)
          return result;

      },
      err => {
        console.log(err);
        return false;
      }
    )


    return true;
  }
}
