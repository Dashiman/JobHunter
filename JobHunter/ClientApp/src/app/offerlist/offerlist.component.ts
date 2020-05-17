import { Component, OnInit } from '@angular/core';
import { JobOffer } from '../models/jobOffer';
import { JobOfferService } from '../services/job-offer.service';

@Component({
  selector: 'app-offerlist',
  templateUrl: './offerlist.component.html',
  styleUrls: ['./offerlist.component.css']
})
export class OfferlistComponent implements OnInit {
  loading: boolean;
  offer: JobOffer[];
  constructor(private job: JobOfferService) {
    this.offer = [];
    this.loading = true;
  }

  ngOnInit() {
    this.job.get().subscribe(res => {
      this.offer = res;
      console.log(this.offer)
      this.loading = false;
    })
  }
}
