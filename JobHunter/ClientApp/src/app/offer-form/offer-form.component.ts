import { Component, OnInit } from '@angular/core';
import { JobOffer } from '../models/jobOffer';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { JobOfferService } from '../services/job-offer.service';
import { TranslateService } from '@ngx-translate/core';
import { Input } from '@angular/core';

@Component({
  selector: 'app-offer-form',
  templateUrl: './offer-form.component.html',
  styleUrls: ['./offer-form.component.css']
})
export class OfferFormComponent implements OnInit {

  offerFG: FormGroup;
  //@Input
  offer: JobOffer = new JobOffer();
  constructor(private _authService: AuthService, private fb: FormBuilder, private _router: Router, private _job: JobOfferService,private _t:TranslateService) {

  }
  ngOnInit() {
    this._authService.isLoggedIn().subscribe(res => {
      if (res == false)
        this._router.navigate(["/login"]);
    })
    this.offerFG = this.fb.group({
      title: new FormControl(this.offer.title, [Validators.required]),
      category: new FormControl(this.offer.categoryId, [Validators.required]),
      declaredCost: new FormControl(this.offer.declaredCost, [Validators.required]),
      description: new FormControl(this.offer.description, [Validators.required]),
      endOffer: new FormControl(this.offer.endOfferDate, [Validators.required]),

    });
  }
  get title() { return this.offerFG.get('title') }
  get category() { return this.offerFG.get('category') }
  get declaredCost() { return this.offerFG.get('declaredCost') }
  get description() { return this.offerFG.get('description') }
  get endOffer() { return this.offerFG.get('endOffer') }

  addOffer() {
    this.offerFG.markAllAsTouched();
    if (this.offerFG.valid) {
      console.log(this.offerFG.controls.title.value)
      var offer = new JobOffer();

      offer.title = this.offerFG.controls.title.value;
      offer.description = this.offerFG.controls.description.value;
      offer.categoryId = this.offerFG.controls.category.value;
      offer.declaredCost = this.offerFG.controls.declaredCost.value;
      offer.endOfferDate = this.offerFG.controls.endOffer.value;
      this._job.add(offer).subscribe(res => {
        if (res == 1)
          this._router.navigate(['']);
        else
          alert(this._t.instant("errors.jobAddErr"));
      })

    }
  }
}
