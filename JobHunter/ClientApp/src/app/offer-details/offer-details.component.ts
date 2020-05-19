import { Component, OnInit } from '@angular/core';
import { JobOfferService } from '../services/job-offer.service';
import { JobOffer, TakenOffer } from '../models/jobOffer';
import { map } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { FormGroup } from '@angular/forms';
import { BidOffer } from '../models/BidOffer';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.css']
})
export class OfferDetailsComponent implements OnInit {
  offer: JobOffer = new JobOffer();
  editFG: FormGroup
  loading: boolean;

  constructor(private job: JobOfferService, private ac: ActivatedRoute, private auth: AuthService, private router: Router) {
    this.loading = true;
  }

  ngOnInit() {
    this.editFG = new FormGroup({});
    this.auth.isLoggedIn().subscribe(res => {
      if (res == true) {
        var offId = this.ac.snapshot.params.id;
        this.job.getOffer(offId).subscribe(res => {
          res.edited = true;
          this.offer = res;

          this.loading = false;
        })
      }

      else
        this.router.navigate(['']);
    })


  }
  seteditFG(event: FormGroup): void {
    this.editFG = event;
    console.log(event)
  }

  setOffer(offer: JobOffer): void {
    this.offer = offer
    console.log(offer)

  }
  giveJob(offer: BidOffer) {
    console.log(offer)
    var jobid = offer.jobOfferId;
    //var taken = new TakenOffer();
    //taken.addedById = this.offer.addedById;
    //taken.description = this.offer.description;
    //taken.title = this.offer.title;
    //taken.status = 1;
    //taken.takenById = offer.userId;
    //taken.declaredCost = offer.proposition;
    this.job.giveJob(offer).subscribe(res => {
      if (res == 1) {
          alert("Zlecono pomyślnie")
          this.router.navigate([''])


      }
    })
  }
}
