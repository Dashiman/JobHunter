import { Component, OnInit } from '@angular/core';
import { JobOffer } from '../models/jobOffer';
import { JobOfferService } from '../services/job-offer.service';
import { BidOffer } from '../models/BidOffer';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-offerlist',
  templateUrl: './offerlist.component.html',
  styleUrls: ['./offerlist.component.css']
})
export class OfferlistComponent implements OnInit {
  loading: boolean;
  offerEd: JobOffer = new JobOffer();
  editFG: FormGroup
  p: number = 1;
  cost: number;
  userId: number;
  offer: JobOffer[];
  canBid: boolean;
  constructor(private job: JobOfferService, private auth: AuthService, private route: Router) {
    this.offer = [];
    this.loading = true;
    this.cost = 0;
    this.userId = 0;
    this.canBid = false;
  }

  ngOnInit() {
    this.job.get().subscribe(res => {
      this.offer = res;
      this.offer.forEach(x => {
        x.bidding = false;
        x.editing = false;
      })
      this.loading = false;
    })
    this.auth.isLoggedIn().subscribe(res => this.canBid = res);
    this.auth.getUserId().subscribe(res => this.userId = res);
  }
  applyFor(offerId: number) {
    if (this.cost != 0 ) {
      console.log(this.cost)
      var bid = new BidOffer();
      bid.jobOfferId = offerId;
      bid.offerDate = new Date();
      bid.proposition = this.cost;
      this.job.applyFor(bid).subscribe(res => {
        if (res == 1)
          alert("Sukces")
        else ("Error")
        this.offer.forEach(x => x.bidding = false)

      })
    }
  }
  bid(offerId: number) {
    this.offer.forEach(x => x.bidding = false)
    console.log(offerId)
    this.offer.forEach(x => {
      if (x.id == offerId)
        x.bidding = true;


    })
 
  }
  showDetails(offerId: number) {
    this.route.navigate(['offer/' + offerId])
  
  }
  refresh() {
    this.job.get().subscribe(res => {
      this.offer = res;
      this.offer.forEach(x => {
        x.bidding = false;
        x.editing = false;
      })
      this.loading = false;
    })


  }
  editCourse(courseOffer: JobOffer) {

    var index = this.offer.findIndex(x => x.id == courseOffer.id);
    if (this.offer[index].editing != true) {
      this.offer[index].editing = true;
      this.offer[index].edited = true;
    }
    else
      this.offer[index].editing = false;;

  }
  seteditFG(event: FormGroup): void {
    this.editFG = event;
  }

  setOffer(offer: JobOffer): void {

    var offoutput = offer;
    offoutput.edited = false;
    offoutput.editing = false;
    var index = this.offer.findIndex(x => x.id == offer.id);

    this.offer[index] = offoutput;

  }
}
