import { Component } from '@angular/core';
import { AuthService } from "../services/auth.service";
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  authority: number;
  username: string;
  lang:string;
  requestCount:number;
  userId:number;
  isLoggedIn: boolean;
  constructor(private translate: TranslateService, private _auth: AuthService, private _router: Router) {
    translate.setDefaultLang('pl');
    this.lang = 'pl';
    this.isLoggedIn = false;

  }
  ngOnInit() {

    this.isLoggedInCheck();
    this.getAuthority();
    this.getUsername();

  }
  useLanguage(language: string) {
    this.translate.use(language);
    this.lang = this.translate.currentLang;
  }
  isLoggedInCheck() {
    this._auth.isLoggedIn().subscribe(res => {
      this.isLoggedIn = res;
      this.getAuthority();

    });
  }
  getAuthority() {
    this._auth.getAuthority().subscribe(res => {
      this.authority = res;
      console.log(this.isLoggedIn)
    });
  }
  getUsername() {
      this._auth.getUserName().subscribe(res => {
        this.username = res.username;
      });
    
  }
  logout() {
    this._auth.logout().subscribe(res => {
      console.log(res)
      this.isLoggedIn = false;
      this.authority = 0;
      this._router.navigate([""]);
      // return res as boolean;
    });
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  goProfile() {
    this._auth.getUserId().subscribe(res => {
      this.userId = res
      this._router.navigate(['user/' + this.userId])
    })
  }
}
