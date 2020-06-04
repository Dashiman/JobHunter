import { Component, OnInit } from '@angular/core';
import { JobOfferService } from '../services/job-offer.service';
import { TakenOffer, JobOffer, EndModel } from '../models/jobOffer';
import { ProfileData } from '../models/profileData';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Users } from '../models/users';
import { RegistrationService } from '../services/registration.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {
  loading: boolean;
  profileData: ProfileData;
  user: Users;
  endFg: FormGroup;
  editFg: FormGroup;
  userId: number;
  endOption: number;
  constructor(private registrationService: RegistrationService, private job: JobOfferService, private auth: AuthService, private ar: ActivatedRoute, private route: Router, private fb: FormBuilder) {
    this.loading = true;
    this.profileData = new ProfileData();
    this.user = new Users();

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
      this.user = res.user;
    })

    this.editFg = this.fb.group({
      username: new FormControl(this.user.username, [Validators.required, Validators.minLength(3)]),
      name: new FormControl(this.user.firstname, [Validators.required]),
      lastname: new FormControl(this.user.lastname, [Validators.required]),
      phoneNumber: new FormControl(this.user.phone, [Validators.required, Validators.minLength(9)]),
      email: new FormControl(this.user.email, [Validators.required, Validators.email])
    });
  }

  get username() { return this.editFg.get('username') }
  get name() { return this.editFg.get('name') }
  get lastname() { return this.editFg.get('lastname') }
  get phoneNumber() { return this.editFg.get('phoneNumber') }
  get email() { return this.editFg.get('email') }

  edit() {
    this.profileData.editingProfile = !this.profileData.editingProfile;
    this.editFg.controls.username.setValue(this.user.username);
    this.editFg.controls.name.setValue(this.user.firstname);
    this.editFg.controls.lastname.setValue(this.user.lastname);
    this.editFg.controls.phoneNumber.setValue(this.user.phone);
    this.editFg.controls.email.setValue(this.user.email);
  }

  resetForm() {
    this.editFg.controls.username.setValue(this.user.username);
    this.editFg.controls.name.setValue(this.user.firstname);
    this.editFg.controls.lastname.setValue(this.user.lastname);
    this.editFg.controls.phoneNumber.setValue(this.user.phone);
    this.editFg.controls.email.setValue(this.user.email);
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

  editUser(profileEditForm: any) {
    this.editFg.markAllAsTouched();
    console.log(this.editFg)
    if (this.editFg.valid) {
      this.user.username = profileEditForm.controls.username.value;
      this.user.firstname = profileEditForm.controls.name.value;
      this.user.lastname = profileEditForm.controls.lastname.value;
      this.user.phone = parseInt(profileEditForm.controls.phoneNumber.value);
      this.user.email = profileEditForm.controls.email.value;
      this.user.authority = 1;
    }

    this.registrationService.update(this.user).subscribe(
      (res) => {
        if (res) {
          alert("Pomyślnie zaktualizowano");
        }
        else
          alert("Błąd podczas aktualizacji danych użytkownika");
      }
    );
  }

}
