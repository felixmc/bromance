using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bros.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        public ActionResult StoreIndex()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddCategory()
        {
            using (var context = new ModelFirstContainer())
            {
                Category cat = new Category{Name =(string)Request["Name"]};
                context.Categories.Add(cat);
                context.SaveChanges();
            }
            return View();
        }
       
        public ActionResult EditCategory()
        {
           
                using (var context = new ModelFirstContainer())
                {
                    int id = Int32.Parse(Request["id"]);
                    Category cat = context.Categories.FirstOrDefault(x => x.Id == id);
                    cat.Name = Request["name"];
                    context.SaveChanges();
                }
            
            return View();

        }

        [HttpPost]
        public ActionResult AddTag()
        {
            using (var context = new ModelFirstContainer())
            {
                Tag tag = new Tag { Name = (string)Request["Name"] };
                context.Tags.Add(tag);
                context.SaveChanges();
            }
            return View();
        }

        public ActionResult EditTag(Tag t)
        {
            if (t != null)
            {
                using (var context = new ModelFirstContainer())
                {
                    int id = Int32.Parse(Request["id"]);
                    Tag tag = context.Tags.FirstOrDefault(x => x.Id == id);
                    tag.Name = t.Name;
                    context.SaveChanges();
                }
            }
            return View();

        }
        public ActionResult ViewProductsInCategory()
        {
            return View();
        }
       
        public ActionResult LoadProductCreation()
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                ViewBag.Categories = context.Categories.Select(c => new { Id = c.Id, Name = c.Name }).ToList();
            }

            return View("CreateProduct");
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product, HttpPostedFileBase ImageFile)
        {
            using (var ms = new MemoryStream())
            {
                ImageFile.InputStream.CopyTo(ms);
                product.Image = ms.ToArray();
            }

            int categoryId = Int32.Parse(Request["Category"]);
            
          

            ActionResult result;
            if (product.Name != null && product.Price > 0 && Request["Category"] != null)
            {
                int productId;
                //Category tempCat = null;
                using (ModelFirstContainer context = new ModelFirstContainer())
                {
                    Product p = new Product()
                    {
                        Category = context.Categories.Single(x => x.Id == categoryId),
                        DateCreated = DateTime.Now,
                        //Category = tempCat,
                        Name = product.Name,
                        Price = product.Price,
                        Image = product.Image
                    };
                    
                    context.Products.Add(p);
                    context.SaveChanges();
                    //tempCat.Products.Add(p);

                   // context.SaveChanges();

                    productId = p.Id;
                    result = View("ViewProduct", p);
                }
            }
            else
            {
                using (ModelFirstContainer context = new ModelFirstContainer())
                {
                    ViewBag.Categories = context.Categories.Select(c => new { Id = c.Id, Name = c.Name }).ToList();
                    result = View();
                }
            }

            return result;
        }

        public ActionResult DeleteProductById(int productId)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                context.Products.FirstOrDefault(x => x.Id == productId).IsDeleted = true;
                context.SaveChanges();

                ViewBag.Products = context.Products.Include("Tags").Include("Category").Where(x => !x.IsDeleted).ToList();
            }


            return View("StoreIndex");
        }

        public ActionResult ViewAllProducts()
        {

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                ViewBag.Products = context.Products.Include("Tags").Include("Category").Where(x => !x.IsDeleted).ToList(); 
            }

            return View();
        }

        //public ActionResult ViewProduct(int productID)
        //{
        //    Product targetProduct;

        //    using (ModelFirstContainer context = new ModelFirstContainer())
        //    {
        //        targetProduct = context.Products.Include("Category").FirstOrDefault(p => p.Id == productID);
        //    }

        //    return View(targetProduct);
        //}

        public ActionResult ViewProduct(Product product)
        {
            return View(product);
        }

        public ActionResult ViewProductById(int productID)
        {
            Product product;
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                product = context.Products.Include("Tags").Include("Category").FirstOrDefault(x => x.Id == productID);
            }
            return View("ViewProduct", product);
        }

        public ActionResult EditProduct(int productID)
        {
            Product targetProduct;

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                targetProduct = context.Products.Include("Category").Include("Tags").FirstOrDefault(x => x.Id == productID);
            }

            return View(targetProduct);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            return View();
        }

        public ActionResult AddProductToCart(int productID)
        {
            return View();
        }

        public ActionResult RemoveProductFromCart(int productID)
        {
            return View();
        }

        public ActionResult ViewCart()
        {
            return View();
        }
      
    }
}