using System;
using System.Collections.Generic;
using System.Text;

namespace Services.VM
{
   public class BidOffer
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual Users User { get; set; }
        public int JobOfferId { get; set; }
        public virtual JobOffer JobOffer { get; set; }
        public DateTime OfferDate { get; set; }
        public decimal Proposition { get; set; }
    }
}
