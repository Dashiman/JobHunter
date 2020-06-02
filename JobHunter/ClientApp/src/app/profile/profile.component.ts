import { Component, OnInit } from '@angular/core';
import { JobOfferService } from '../services/job-offer.service';
import { TakenOffer, JobOffer, EndModel } from '../models/jobOffer';
import { ProfileData } from '../models/profileData';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  loading: boolean;
  profileData: ProfileData;
  endFg: FormGroup;
  userId: number;
  endOption: number;
  constructor(private job: JobOfferService, private auth: AuthService, private ar: ActivatedRoute, private route: Router, private fb: FormBuilder) {
    this.loading = true;
    this.profileData = new ProfileData();

  }
  get endSelect() { return this.endFg.get('endSelect') }

  ngOnInit() {
    var id = this.ar.snapshot.params.id;
    this.endFg = new FormGroup({});
    this.endFg = this.fb.group({
      endSelect: new FormControl(this.endOption, [Validators.required]),

    });
    this.job.getPD(id).subscribe(res => {
      res.editingProfile = false;
      this.profileData = res;
      this.loading = false;
    })
  }
  edit() {
    this.profileData.editingProfile = !this.profileData.editingProfile;
  }
  showDetails(offerId: number) {
    this.route.navigate(['offer/' + offerId])
  }
  endCourse(offerId: number) {
    this.endFg.markAllAsTouched();
    if (this.endFg.valid) {
      var end = new EndModel();
      end.offerId = offerId;
      end.statusId = this.endFg.controls.endSelect.value;

      this.job.endCourse(end).subscribe(res => {
        if (res == 1) {
          alert("Pomyślnie zakończono kurs");
          this.route.navigate([''])
        }
        else
          alert("Błąd podczas zamykania kursu")
      })
    }
  }
}
