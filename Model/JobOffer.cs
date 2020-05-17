using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class JobOffer
    {
        [Key] public int Id { get; set; }

        public string Title { get; set; }
        public decimal DeclaredCost { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        [ForeignKey("TakenBy")] public int? TakenById { get; set; }

        public virtual Users TakenBy { get; set; }

        [ForeignKey("AddedBy")] public int AddedById { get; set; }

        public virtual Users AddedBy { get; set; }
        public DateTime EndOfferDate { get; set; }


        public ICollection<BidOffer> BidOffers { get; set; }
        public int? EndedAs { get; set; }
        [ForeignKey("Category")] public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}