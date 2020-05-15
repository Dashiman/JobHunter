using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
   public class BidOffer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual Users User { get; set; }
        [ForeignKey("JobOffer")]
        public int JobOfferId { get; set; }
        public virtual JobOffer JobOffer { get; set; }
        public DateTime OfferDate { get; set; }
        public decimal Proposition { get; set; }
    }
}
