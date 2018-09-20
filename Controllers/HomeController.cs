using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using products.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace products.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
    
        public HomeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("products/new")]
        public IActionResult Products()
        {
            return View();
        }

        [HttpPost("newProduct")]
        public IActionResult newProduct(Product product)
        {
            if(ModelState.IsValid)
            {

                    _context.Add(product);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("Id", (int)product.ProductId);

                return RedirectToAction("showProduct", new{pId = product.ProductId});
            }
            else
            {
                return View("Products");
            }
            
        }
        [HttpGet("showProduct/{pId}")]
        public IActionResult showProduct(int pId, int cId)
        {
            Product product = _context.products.SingleOrDefault(p => p.ProductId == pId);
            Category cat = _context.categories.SingleOrDefault(c => c.CategoryId == cId);
            // Console.WriteLine(product.Categories[0].CategoryId);
            ViewModel viewModel = new ViewModel();
            
            viewModel.usedCategories = GetCategories();
            viewModel.unusedCategories = GetCategories();

            List<Category> usedCategories = new List<Category>();
            List<Category> unusedCategories = new List<Category>();
            List<Category> Categorys = _context.categories
                        .Include( C => C.Products )
                            .ThenInclude( con => con.Product)
                        .ToList();

            foreach(var x in Categorys){
                if(x.Products.SingleOrDefault(c=>c.ProductId ==pId) != null){
                usedCategories.Add(x);
                }
                else{
                    unusedCategories.Add(x);
                }
            }

            // foreach(Category category in GetCategories()){
            //     var counter = 0;
            //     foreach(var p in category.Products){
            //         if(p.ProductId == pId){
            //             counter++;
            //         }
            //     }
            //     if(counter == 0){
            //         unusedCategories.Add(category);
            //     }
            //     Console.WriteLine(counter);
            // }

            // foreach(Product x in GetProducts()){
            //     if(x.ProductId == pId){
                    
            //         foreach(var c in x.Categories){
            //             usedCategories.Add(c.Category);
                        
            //         }
            //     }
            // }
            Console.WriteLine(unusedCategories + "==============");
            Console.WriteLine(usedCategories + "---------------");
            viewModel.usedCategories = usedCategories;
            viewModel.unusedCategories = unusedCategories;

            int? iddd = HttpContext.Session.GetInt32("Id");
            var prod = _context.products.SingleOrDefault(p => p.ProductId  == iddd);
            ViewBag.name = prod.name;
            ViewBag.ProductId = iddd;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult prodToCategory(Joins joins)
        {
                    Console.WriteLine(joins.CategoryId);
                    Console.WriteLine(joins.ProductId);
                    
                    Product product = _context.products.SingleOrDefault(p => p.ProductId == joins.ProductId);
                    Category category = _context.categories.SingleOrDefault(c => c.CategoryId == joins.CategoryId);
                    
                    joins.Product = product;
                    joins.Category = category;
                    
                    _context.joins.Add(joins);
                    _context.SaveChanges();
                    
                    int pId = joins.ProductId;
                    HttpContext.Session.SetInt32("Id", (int)joins.ProductId);
                    
                    
                return RedirectToAction("showProduct", new{pId = product.ProductId});
            }

        [HttpGet("categories/new")]
        public IActionResult Categories()
        {
            return View("Categories");
        }

        [HttpPost("newCategory")]
        public IActionResult newCategory(Category category)
        {
            if(ModelState.IsValid)
            {

                    _context.Add(category);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("Id", (int)category.CategoryId);
                return RedirectToAction("showCategory", new{cId = category.CategoryId});
            }
            else
            {
                return View("Categories");
            }
        }

        [HttpGet("showCategory/{cId}")]
        public IActionResult showCategory(int pId, int cId)
        {
            Product product = _context.products.SingleOrDefault(p => p.ProductId == pId);
            Category cat = _context.categories.SingleOrDefault(c => c.CategoryId == cId);
            // Console.WriteLine(product.Categories[0].CategoryId);
            ViewModel viewModel = new ViewModel();
            
            viewModel.usedProducts = GetProducts();
            viewModel.unusedProducts = GetProducts();

            List<Product> usedProducts = new List<Product>();
            List<Product> unusedProducts = new List<Product>();
            List<Product> Products = _context.products
                        .Include( P => P.Categories )
                            .ThenInclude( con => con.Category)
                        .ToList();

            foreach(var x in Products){
                if(x.Categories.SingleOrDefault(p => p.CategoryId == cId) != null){
                usedProducts.Add(x);
                }
                else{
                    unusedProducts.Add(x);
                }
            }


            Console.WriteLine(unusedProducts + "==============");
            Console.WriteLine(usedProducts + "---------------");
            viewModel.usedProducts = usedProducts;
            viewModel.unusedProducts = unusedProducts;

            int? iddd = HttpContext.Session.GetInt32("Id");
            var catt = _context.categories.SingleOrDefault(c => c.CategoryId  == iddd);
            ViewBag.name = catt.name;
            ViewBag.CategoryId = iddd;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult catToProduct(Joins joins)
        {
                    Console.WriteLine(joins.CategoryId);
                    Console.WriteLine(joins.ProductId);
                    
                    Product product = _context.products.SingleOrDefault(p => p.ProductId == joins.ProductId);
                    Category category = _context.categories.SingleOrDefault(c => c.CategoryId == joins.CategoryId);
                    
                    joins.Product = product;
                    joins.Category = category;
                    
                    _context.joins.Add(joins);
                    _context.SaveChanges();
                    
                    int cId = joins.CategoryId;
                    HttpContext.Session.SetInt32("Id", (int)joins.CategoryId);
                    
                    
                return RedirectToAction("showCategory", new{cId = category.CategoryId});
            }


        private List<Product> GetProducts()
        {
            return _context.products.ToList();
        }

        private List<Category> GetCategories()
        {
            return _context.categories.ToList();
        }
    }
}
