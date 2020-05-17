import { Users } from "./users";
import { BidOffer } from "./BidOffer";

export class JobOffer {
  public  Id:number;
public Title:string
public  DeclaredCost:number;
public  Description :string;
public  Status :number;
public TakenById :number;
public   TakenBy:Users;
public AddedById :number;
public  AddedBy:Users;
public EndOfferDate :Date;
  public CategoryId: number;


public BidOffers:BidOffer[];
public EndedAs :number;

}
export  class TakenOffer {
  public Id: number;
  public Title: string
  public DeclaredCost: number;
  public Description: string;
  public Status: number;
  public TakenById: number;
  public TakenBy: Users;
  public AddedById: number;
  public AddedBy: Users;
  public FinishDate: Date;
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
