using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace products.Models
{
    public class Category
    {
        [Key]
        public int? CategoryId { get; set; }
        [Required]
        public string name { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public List<Joins> Products { get; set; }

        public Category()
        {
            Products = new List<Joins>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}