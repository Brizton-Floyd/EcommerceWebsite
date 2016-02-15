using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class ProcessOrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            if (Session["id"] != null && Session["type"].ToString() == "admin")
                return RedirectToAction("Index", "Admin");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult PaymentOption()
        {
            if (Session["id"] != null && Session["type"].ToString() == "admin")
                return RedirectToAction("Index", "Admin");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public JsonResult ProcessRequest(string job, string media, string mediaCatalog, string content)
        {
            string response = " ";
            if (!string.IsNullOrEmpty(job) && !string.IsNullOrEmpty(media) && !string.IsNullOrEmpty(mediaCatalog) &&
                !string.IsNullOrEmpty(content))
            {
                Session["job"] = job;
                Session["media"] = media;
                Session["mediaCatalog"] = mediaCatalog;
                Session["content"] = content;
                response = "pass";
            }
            else
            {
                response = "fail";
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddOrder(string payOption)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            Order o = new Order();

            o.JobType = Session["job"].ToString();
            o.Media = Session["media"].ToString();
            o.CatalogNumber = int.Parse(Session["mediaCatalog"].ToString());
            o.Content = Session["content"].ToString();
            o.UserId = int.Parse(Session["id"].ToString());
            o.PaymentMethod = payOption;
            o.Status = "PENDING";

            string response = "pass";
            ctx.Orders.Add(o);
            ctx.SaveChanges();

            return this.Json(response, JsonRequestBehavior.AllowGet);
        }


    }
}
