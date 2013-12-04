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
                Category cat = new Category{Name =(string)ViewData["Name"] };
                context.Categories.Add(cat);
                context.SaveChanges();
            }
            return View();
        }
       
        public ActionResult EditCategory()
        {
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

        public ActionResult CreateProduct()
        {
            return View();
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