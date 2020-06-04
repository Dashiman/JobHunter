import { Component, OnInit } from '@angular/core';
import { Users } from '../models/users';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: Users;
  constructor(private _authService: AuthService, private _router: Router) {
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
         alert("Brak użytkownika")
          break;

        case 3:
          alert("Błędne dane logowania")
          break;

        default:
          alert("Nieznany kod błędu")
          break;

        }

      }
    );
  }
}
