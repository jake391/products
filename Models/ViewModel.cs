using System.Collections.Generic;

namespace products.Models
{
    public class ViewModel
    {
        public Category Category;

        public Product Product;

        public Joins Joins;

        public List<Category> usedCategories = new List<Category>();

        public List<Product> usedProducts = new List<Product>();

        public List<Category> unusedCategories = new List<Category>();

        public List<Product> unusedProducts = new List<Product>();

        public List<Category> AllCategories = new List<Category>();

        public List<Product> AllProducts = new List<Product>();
    }
}