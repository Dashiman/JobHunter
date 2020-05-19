import { Users } from "./users";
import { BidOffer } from "./BidOffer";

export class JobOffer {
  public  id:number;
public title:string
public  declaredCost:number;
public  description :string;
public  status :number;
public takenById :number;
public   takenBy:Users;
public addedById :number;
public  addedBy:Users;
public endOfferDate :Date;
  public categoryId: number;
  public category: Category;

public bidOffers:BidOffer[];
  public endedAs: number;
  public edited: boolean;

  public bidding:boolean
}
export  class TakenOffer {
  public id: number;
  public title: string
  public declaredCost: number;
  public description: string;
  public status: number;
  public takenById: number;
  public takenBy: Users;
  public addedById: number;
  public addedBy: Users;
  public finishDate: Date;
}
export class Category {
  public id: number;
  public description: string;
}
/*
 JobOffer
 1- do wzięcia
 2- wzięte

Taken Offer
1- w trakcie realizacji
2- przerwane
3- zakończone z rekomendacją
4- zakończone
5 - zakończone z "reklamacją"
*/
