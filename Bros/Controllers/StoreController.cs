using Bros.DataModel;
using Bros.Models;
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
        List<Product> prodList = ProductFactory.ProductList(10);
        public ActionResult StoreIndex()
        {

            return RedirectToAction("ViewAllProducts");
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
                        Image = product.Image,
                        Description = product.Description
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
            ViewBag.AddToCart = true;
            //GenerateProducts();
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
            ViewBag.AddToCart = true;
            return View(product);
        }

        public ActionResult ViewProductById(int productID)
        {
            ViewBag.AddToCart = true;
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

            int categoryId = Int32.Parse(Request["Category"]);

            if (product.Name != null && product.Price > 0 && Request["Category"] != null)
            {
                int productId;
                //Category tempCat = null;
                using (ModelFirstContainer context = new ModelFirstContainer())
                {

                    Product target = context.Products.SingleOrDefault(x => x.Id == product.Id);

                    product.Orders = target.Orders;

                    if (ImageFile != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            ImageFile.InputStream.CopyTo(ms);
                            product.Image = ms.ToArray();
                        }
                    }
                    else
                    {
                        product.Image = target.Image;
                    }

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
            ViewBag.AddToCart = true;
            using (ModelFirstContainer context = new ModelFirstContainer()){

                int userId = WebSecurity.CurrentUserId;
                User user = context.Users.SingleOrDefault(x => x.Id == userId);
                ShoppingCart cart = context.ShoppingCarts.Single(x => x.User.Id == user.Id);
                if(cart == null)
                {
                    cart = new ShoppingCart{ User = user};
                }
                Product product = context.Products.SingleOrDefault(x => x.Id == productID);
                ProductQuantity quantity = cart.ProductQuantities.FirstOrDefault(x => x.ProductId == productID);
                if(quantity == null)
                {
                    quantity = new ProductQuantity{ ProductId = productID, Product = product,Quantity = 1};
                    user.ShoppingCart.ProductQuantities.Add(quantity);

                }
                else
                {
                    quantity.Quantity += 1;
                }


                context.SaveChanges();
            }

            return RedirectToAction("ViewAllProducts");
        }

        public ActionResult RemoveProductFromCart(int productID = -10)
        {
            ViewBag.RemoveFromCart = true;
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int userId = WebSecurity.CurrentUserId;
                User user = context.Users.SingleOrDefault(x => x.Id == userId);
                ShoppingCart cart = context.ShoppingCarts.Single(x => x.User.Id == user.Id);
                Product product = context.Products.SingleOrDefault(x => x.Id == productID);
                ProductQuantity quantity = cart.ProductQuantities.Single(x => x.ProductId == productID);
                quantity.Quantity -= 1;
                if (quantity.Quantity == 0)
                {
                    cart.ProductQuantities.Remove(quantity);
                }
                //user.ShoppingCart.Products.Remove(product);

                context.SaveChanges();
            }

            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            int sessionId = (int)Session["UserId"];
            List<Product> products;
            ViewBag.RemoveFromCart = true;
            using(var context = new ModelFirstContainer())
            {
                User user = context.Users.Single(x => x.Id == WebSecurity.CurrentUserId);
                
                ShoppingCart cart = context.ShoppingCarts.Single(x => x.User.Id == sessionId);
                ViewBag.ItemsInShoppingCart = cart.ProductQuantities;
            }

           
            return View();
        }

        public ActionResult ViewCheckout()
        {

            using(var context = new ModelFirstContainer())
            {
                ShoppingCart cart = context.ShoppingCarts.Single(x => x.User.Id == WebSecurity.CurrentUserId);
                ViewBag.ItemsInShoppingCart = cart.ProductQuantities;
            }
            return View();

        }
           
        public ActionResult Checkout()
        {
            int id = WebSecurity.CurrentUserId;
            using(var context = new ModelFirstContainer())
            {
                User user = context.Users.Single(x => x.Id == id);
                ShoppingCart cart = context.ShoppingCarts.Single(x => x.User.Id == id);
                foreach (ProductQuantity p in cart.ProductQuantities)
                {
                    user.ShoppingCart.ProductQuantities.Remove(p);
                }

                context.SaveChanges();

            }

            return View();
        }

        void GenerateProducts()
        {
            using (var context = new ModelFirstContainer())
            {
                foreach(Product prod in prodList)
                {
                    context.Products.Add(prod);
                    
                }
                context.SaveChanges();
            }
        }
    }
}