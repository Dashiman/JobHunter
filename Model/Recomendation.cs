using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Recomendation
    {
        [Key] public int Id { get; set; }

        public string Content { get; set; }

        [ForeignKey("ForUser")] public int? ForUserId { get; set; }

        public virtual Users ForUser { get; set; }

        [ForeignKey("AddedBy")] public int AddedById { get; set; }

        public virtual Users AddedBy { get; set; }

    }
}