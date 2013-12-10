using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bros.Controllers
{
    //[Authorize(Roles = "Admin, User, StoreAdmin")]
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        public ActionResult StoreIndex()
        {
            return View();
        }
        
        [HttpPost]
        //[Authorize(Roles="Admin, StoreAdmin")]
        public ActionResult AddCategory()
        {
            using (var context = new ModelFirstContainer())
            {
                Category cat = new Category{Name =(string)Request["Name"]};
                context.Categories.Add(cat);
                context.SaveChanges();
            }
            return RedirectToAction("LoadProductCreation");
        }

        public ActionResult LoadCategorys()
        {
            using (var context = new ModelFirstContainer())
            {
                List<Category> cats = context.Categories.ToList();
                ViewBag.Category = cats;
            }
            return View();
        }

        public ActionResult HandleCategory(string catId)
        {
            if (catId == null)
            {
                ViewBag.partialView = "_AddCategory";
                return View();
            }

            else
            {
                using (var context = new ModelFirstContainer())
                {
                    int categoryId = Int32.Parse(catId);
                    Category cat = context.Categories.Single(x => x.Id == categoryId);
                    ViewBag.partialView = "_EditCategory";

                    ViewBag.cat = cat;
                    return View();
                }
            }

        }

        //[Authorize(Roles = "Admin, StoreAdmin")]
       [HttpPost]
        public ActionResult EditCategory()
        {
           
                using (var context = new ModelFirstContainer())
                {
                    int id = Int32.Parse(Request["id"]);
                    Category cat = context.Categories.FirstOrDefault(x => x.Id == id);
                    cat.Name = Request["name"];
                    context.SaveChanges();
                }

                return RedirectToAction("LoadCategorys");

        }
       public ActionResult LoadTags()
       {
           using (var context = new ModelFirstContainer())
           {
               List<Tag> tags = context.Tags.ToList();
               ViewBag.Tags = tags;
           }
           return View();
       }

       public ActionResult HandleTag(string tagId)
       {
           if (tagId == null)
           {
               ViewBag.partialView = "_AddTag";
               return View();
           }

           else
           {
               using (var context = new ModelFirstContainer())
               {
                   int tId = Int32.Parse(tagId);
                   Category tag = context.Categories.Single(x => x.Id == tId);
                   ViewBag.partialView = "_EditTag";
                   //
                   ViewBag.tag = tag;
                   return View();
               }
           }

       }

        [HttpPost]
        //[Authorize(Roles = "Admin, StoreAdmin")]
        public ActionResult AddTag()
        {
            using (var context = new ModelFirstContainer())
            {
                Tag tag = new Tag { Name = (string)Request["Name"] };
                context.Tags.Add(tag);
                context.SaveChanges();
            }
            return RedirectToAction("LoadTags");
        }

        //[Authorize(Roles = "Admin, StoreAdmin")]
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
            return RedirectToAction("LoadTags");

        }
        public ActionResult ViewProductsInCategory()
        {
            return View();
        }

      //  [Authorize(Roles = "Admin, StoreAdmin")]
        public ActionResult LoadProductCreation()
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                ViewBag.Categories = context.Categories.Select(c => new { Id = c.Id, Name = c.Name }).ToList();
                //ViewBag.Tags = context.Categories.Select(c => new { Id = c.Id, Name = c.Name }).ToList();
            }

            return View("CreateProduct");
        }

        [HttpPost]
        //[Authorize(Roles = "Admin, StoreAdmin")]
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
                    //ViewBag.Tags = context.Categories.Select(c => new { Id = c.Id, Name = c.Name }).ToList();
                    result = View();
                }
            }

            return result;
        }

        //[Authorize(Roles = "Admin, StoreAdmin")]
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

        //[Authorize(Roles = "Admin, StoreAdmin")]
        public ActionResult EditProduct(int productID)
        {
            Product targetProduct;

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                ViewBag.Categories = context.Categories.Select(c => new { Id = c.Id, Name = c.Name }).ToList();
                targetProduct = context.Products.Include("Category").Include("Tags").FirstOrDefault(x => x.Id == productID);
            }

            return View(targetProduct);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin, StoreAdmin")]
        public ActionResult EditProduct(Product product, HttpPostedFileBase ImageFile)
        {
            ActionResult result;

            if (ImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    ImageFile.InputStream.CopyTo(ms);
                    product.Image = ms.ToArray();
                }
            }

            int categoryId = Int32.Parse(Request["Category"]);

            if (product.Name != null && product.Price > 0 && Request["Category"] != null)
            {
                int productId;
                //Category tempCat = null;
                using (ModelFirstContainer context = new ModelFirstContainer())
                {

                    Product target = context.Products.SingleOrDefault(x => x.Id == product.Id);

                    if (target != null)
                    {
                        context.Entry(target).CurrentValues.SetValues(product);
                        target.Category = context.Categories.Single(x => x.Id == categoryId);
                    }

                    context.SaveChanges();
                    //tempCat.Products.Add(p);

                    // context.SaveChanges();

                    result = View("ViewProduct", target);
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

        public ActionResult AddProductToCart(int productID)
        {
            using (ModelFirstContainer context = new ModelFirstContainer()){

                int userId = WebSecurity.CurrentUserId;

                User user = context.Users.SingleOrDefault(x => x.Id == userId);

                Product product = context.Products.SingleOrDefault(x => x.Id == productID);

                user.ShoppingCart.Products.Add(product);

                context.SaveChanges();
            }

            return RedirectToAction("StoreIndex");
        }

        public ActionResult RemoveProductFromCart(int productID)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int userId = WebSecurity.CurrentUserId;

                User user = context.Users.SingleOrDefault(x => x.Id == userId);

                Product product = context.Products.SingleOrDefault(x => x.Id == productID);

                user.ShoppingCart.Products.Remove(product);

                context.SaveChanges();
            }

            return RedirectToAction("StoreIndex");
        }

        public ActionResult ViewCart()
        {
            return View();
        }
      
    }
}