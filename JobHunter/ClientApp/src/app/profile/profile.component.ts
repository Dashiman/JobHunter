import { Component, OnInit } from '@angular/core';
import { JobOfferService } from '../services/job-offer.service';
import { TakenOffer, JobOffer } from '../models/jobOffer';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  loading: boolean;
  takenByUser: TakenOffer[];
  addedByUser: JobOffer[];
  addadTaken: TakenOffer[];

  constructor(private job: JobOfferService) {
    this.loading = true;
  }

  ngOnInit() {
  }

}
