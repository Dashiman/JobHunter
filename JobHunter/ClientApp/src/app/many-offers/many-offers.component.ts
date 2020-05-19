import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { JobOfferService } from '../services/job-offer.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-many-offers',
  templateUrl: './many-offers.component.html',
  styleUrls: ['./many-offers.component.css']
})
export class ManyOffersComponent implements OnInit {
  fd: FormData;
  uploadFg: FormGroup;
  baseUrl: string;
  constructor(private fb: FormBuilder, private job: JobOfferService, private http: HttpClient) {
    this.baseUrl = window.location.origin;
  }

  ngOnInit() {
    this.uploadFg = this.fb.group({
      file: new FormControl('', [Validators.required]),

    });
  }
  fileChange(event): void {
    this.uploadFg.markAllAsTouched();
    if (this.uploadFg.valid) {

    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      const file = fileList[0];

      const formData = new FormData();
      formData.append('file', file, file.name);


      this.http.post(this.baseUrl +"/api/jobs/Upload", formData).subscribe(res => {
        if (res == 1) {
          alert("Pomyslny upload")
        }
        else
          alert("Błąd uploadu csv")
      })
      }

    }
  }

  }

