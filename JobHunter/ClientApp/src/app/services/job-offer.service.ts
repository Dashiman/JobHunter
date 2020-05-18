import { Injectable } from '@angular/core';
import { PlatformLocation } from '@angular/common';
import { JobOffer } from '../models/jobOffer';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs/operators";
import { BidOffer } from '../models/BidOffer';

@Injectable({
  providedIn: 'root'
})
export class JobOfferService {
  baseUrl: string;
  constructor(private http: HttpClient, pl: PlatformLocation) {
    this.baseUrl = window.location.origin;
  }


  add(offer: JobOffer): Observable<number> {
    return this.http.post<number>(this.baseUrl + "/api/jobs/AddOffer", offer).pipe(
      (res) => {
        return res;
      }
    )
  }
  applyFor(BidOffer: BidOffer): Observable<number> {
    return this.http.post<number>(this.baseUrl + "/api/jobs/ApplyFor", BidOffer).pipe(
      (res) => {
        return res;
      }
    )
  }
  get(): Observable<JobOffer[]> {
    return this.http.get(this.baseUrl + "/api/jobs/GetOffers").pipe(map(res => { return res as JobOffer[] }));


  }
  getOffer(offId: number): Observable<JobOffer> {
    return this.http.get(this.baseUrl + "/api/jobs/GetOffer/" + offId).pipe(map(res => { return res as JobOffer }));


  }
}
