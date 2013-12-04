using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bros.DataModel;

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
                Category cat = new Category{Name =(string)ViewData["Name"] };
                context.Categories.Add(cat);
                context.SaveChanges();
            }
            return View();
        }
       
        public ActionResult EditCategory(Category c)
        { 
            using(var context = new ModelFirstContainer())
            {
                int id = Int32.Parse(Request["id"]);
                Category cat = context.Categories.FirstOrDefault(x => x.Id == id);
                cat.Name = c.Name;
                context.SaveChanges();
            }
            return View();

        }
        public ActionResult ViewProductsInCategory()
        {
            return View();
        }
        public ActionResult AddProductToCategory()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
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

        public ActionResult EditProduct()
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