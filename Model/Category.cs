using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
