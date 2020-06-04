import { Component, OnInit } from '@angular/core';
import { Users } from '../models/users';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: Users;
  constructor(private toastr: ToastrService, private _authService: AuthService, private _router: Router) {
    this.user = new Users();

  }
  ngOnInit() {
    this._authService.isLoggedIn().subscribe(res => {
      if (res == true)
        this._router.navigate([""]);
    })
  }

  login(loginForm: any) {
    this.user.username = loginForm.controls.username.value;
    this.user.password = loginForm.controls.password.value;

    this._authService.loginUser(this.user).subscribe(
      (res) => {
        switch (res) {
        case 1:
        {
              this._router.navigate(['/listOfOffers'])

          break;

        }


        case 2:
          this.toastr.error("Brak użytkownika")
          break;

        case 3:
          this.toastr.error("Błędne dane logowania")
          break;

        default:
          this.toastr.error("Nieznany kod błędu")
          break;

        }

      }
    );
  }
}
