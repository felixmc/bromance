using Bros.DataModel;
using System;
using System.Collections.Generic;
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
       
        public ActionResult EditCategory(Category c)
        {
            if (c != null)
            {
                using (var context = new ModelFirstContainer())
                {
                    int id = Int32.Parse(Request["id"]);
                    Category cat = context.Categories.FirstOrDefault(x => x.Id == id);
                    cat.Name = c.Name;
                    context.SaveChanges();
                }
            }
            return View();

        }
        public ActionResult ViewProductsInCategory()
        {
            return View();
        }
       

        [HttpGet]
        public ActionResult CreateProduct()
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                ViewBag.Categories = context.Categories.Select(c => new { Id = c.Id, Name = c.Name }).ToList();
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            ActionResult result;
            if (ModelState.IsValid)
            {
                int productId;

                using (ModelFirstContainer context = new ModelFirstContainer())
                {
                    product.DateCreated = DateTime.Now;

                    context.Products.Add(product);
                    context.Categories.FirstOrDefault(x => x.Id == product.Category.Id).Products.Add(product);

                    context.SaveChanges();

                    productId = product.Id;
                }
                result = RedirectToAction("ViewProduct", productId);
            }
            else
            {
                result = View();
            }

            return result;
        }

        public ActionResult ViewProduct(int productID)
        {
            Product targetProduct;

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                targetProduct = context.Products.Include("Category").FirstOrDefault(p => p.Id == productID);
            }

            return View(targetProduct);
        }

        [HttpGet]
        public ActionResult EditProduct(int productID)
        {
            return View();
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