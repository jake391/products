using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace products.Models
{
    public class Product
    {
        [Key]
        public int? ProductId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public double price { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public List<Joins> Categories { get; set; }

        public Product()
        {
            Categories = new List<Joins>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}