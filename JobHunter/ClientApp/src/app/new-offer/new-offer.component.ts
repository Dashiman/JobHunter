import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { JobOffer } from '../models/jobOffer';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-offer',
  templateUrl: './new-offer.component.html',
  styleUrls: ['./new-offer.component.css']
})
export class NewOfferComponent implements OnInit {

  constructor(private _authService: AuthService, private fb: FormBuilder, private _router: Router) {


  }
  ngOnInit() {
    this._authService.isLoggedIn().subscribe(res => {
      if (res == false)
      this._router.navigate(["/signIn"]);
    })

  }

}
