using dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dashboard.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts()
        {
            return Json(

             new { data = db.Products.ToList() }
          , JsonRequestBehavior.AllowGet); ;

        }
    }
}