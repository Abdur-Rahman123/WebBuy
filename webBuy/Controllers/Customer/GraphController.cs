using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBuy.Repositories;



namespace webBuy.Controllers.Customer
{
    public class GraphController : Controller
    {
        // GET: Graph
        ProductRepository pr = new ProductRepository();

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            int male = pr.GetAll().Where(x => x.categoryId == 1).Count();
            int female = pr.GetAll().Where(x => x.categoryId == 2).Count();

            Ratio obj = new Ratio();
            obj.Male = male;
            obj.Female = female;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int Male { get; set; }
            public int Female { get; set; }
        }
    }

    }