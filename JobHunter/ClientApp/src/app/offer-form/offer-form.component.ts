import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { JobOffer } from '../models/jobOffer';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { JobOfferService } from '../services/job-offer.service';
import { TranslateService } from '@ngx-translate/core';
import { Input } from '@angular/core';
import { error } from 'protractor';

@Component({
  selector: 'app-offer-form',
  templateUrl: './offer-form.component.html',
  styleUrls: ['./offer-form.component.css']
})
export class OfferFormComponent implements OnInit {

  @Input()
  offer: JobOffer = new JobOffer();
  offerFG: FormGroup;


  @Output()
  jobOffer = new EventEmitter<JobOffer>();
  @Output()
  offerFGOutput = new EventEmitter<FormGroup>();
  get title() { return this.offerFG.get('title') }
  get category() { return this.offerFG.get('category') }
  get declaredCost() { return this.offerFG.get('declaredCost') }
  get description() { return this.offerFG.get('description') }
  get endOffer() { return this.offerFG.get('endOffer') }

  constructor(private _authService: AuthService, private fb: FormBuilder, private _router: Router, private _job: JobOfferService, private _t: TranslateService) {
 
  }
  ngOnInit() {
    this.offerFG = new FormGroup({});
    this._authService.isLoggedIn().subscribe(res => {
      if (res == false)
        this._router.navigate(["/login"]);
    })
    console.log(this.offer)

    this.offerFG = this.fb.group({
      title: new FormControl(this.offer.title, [Validators.required]),
      category: new FormControl(this.offer.categoryId, [Validators.required]),
      declaredCost: new FormControl(this.offer.declaredCost, [Validators.required]),
      description: new FormControl(this.offer.description, [Validators.required]),
      endOffer: new FormControl(this.offer.endOfferDate, [Validators.required]),

    });
 
    
    
  }

  addOffer() {
    this.offerFG.markAllAsTouched();
    if (this.offerFG.valid) {

      var offer = new JobOffer();

      offer.title = this.offerFG.controls.title.value;
      offer.description = this.offerFG.controls.description.value;
      offer.categoryId = this.offerFG.controls.category.value;
      offer.declaredCost = this.offerFG.controls.declaredCost.value;
      offer.endOfferDate = this.offerFG.controls.endOffer.value;

      if (this.offer.edited == true && this.offer.editing == false) {
        offer.id = this.offer.id;
        offer.addedById = this.offer.addedById;
        offer.status = 1;
        this._job.updateOffer(offer).subscribe(res => {
          if (res == 1) {
            alert("Sukces")

            this._router.navigate([''])
          }
          else {
            alert("Error")
          }
        })
      }
      if (this.offer.editing == true) {
        offer.id = this.offer.id;
        offer.addedById = this.offer.addedById;
        offer.status = 1;
        this._job.updateOffer(offer).subscribe(res => {
          if (res == 1) {
            alert("Sukces")
            this.jobOffer.emit(offer);
          }
          else {
            alert("Error")
          }
        })
      }
      else {
   
        this._job.add(offer).subscribe(res => {
          if (res == 1)
            this._router.navigate(['']);
          else
            alert(this._t.instant("errors.jobAddErr"));
        })
      }
    }
  }
  delete() {
    this._job.deleteOffer(this.offer.id).subscribe(res => {
      if (res == 1) {
        alert("Usunięto pomyslnie")
        this._router.navigate(['']);
      }
      else
        alert ("Błąd usuwania")
    })
  }
  handleChanges() {
    this.offerFG.statusChanges.subscribe(status => {
      this.offerFGOutput.emit(this.offerFG);
      //this.jobOffer.emit(this.offer);
      console.log(this.offerFG)
      console.log(this.offer)
    })
  }
}
