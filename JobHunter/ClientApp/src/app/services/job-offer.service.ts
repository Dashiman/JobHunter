import { Injectable } from '@angular/core';
import { PlatformLocation } from '@angular/common';
import { JobOffer, TakenOffer, EndModel } from '../models/jobOffer';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs/operators";
import { BidOffer } from '../models/BidOffer';
import { ProfileData } from '../models/profileData';

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
  giveJob(offer: BidOffer): Observable<number> {
    return this.http.post<number>(this.baseUrl + "/api/jobs/GiveJob", offer).pipe(
      (res) => {
        return res;
      }
    )
  }
  updateOffer(offer: JobOffer): Observable<number> {
    return this.http.put<number>(this.baseUrl + "/api/jobs/EditOffer", offer).pipe(
      (res) => {
        return res;
      }
    )
  }
  endCourse(endModel: EndModel): Observable<number> {
    return this.http.put<number>(this.baseUrl + "/api/jobs/CloseCourse", endModel).pipe(
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


  } getPD(usId: number): Observable<ProfileData> {
    return this.http.get(this.baseUrl + "/api/jobs/GetPD/" + usId).pipe(map(res => { return res as ProfileData }));


  }
  deleteOffer(offId: number): Observable<number> {
    return this.http.delete(this.baseUrl + "/api/jobs/DeleteOffer/" + offId).pipe(map(res => { return res as number }));
  }
}
