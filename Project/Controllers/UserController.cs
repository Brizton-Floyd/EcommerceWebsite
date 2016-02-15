using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Project.Models;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            if (Session["id"] != null && Session["type"].ToString() == "admin")
                return RedirectToAction("Index", "Admin");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");

            ViewBag.visible = 1;
            return View();
        }

        public ActionResult Setting()
        {
            if (Session["id"] != null && Session["type"].ToString() == "admin")
                return RedirectToAction("Index", "Admin");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");


            ViewBag.visible = 3;
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            int userId = int.Parse(Session["id"].ToString());
            User u = ctx.Users.FirstOrDefault(m => m.UserId == userId);
            return View(u);
        }

        public ActionResult TotalOrders()
        {
            if (Session["id"] != null && Session["type"].ToString() == "admin")
                return RedirectToAction("Index", "Admin");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");

            ViewBag.visible = 2;
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            int userId = int.Parse(Session["id"].ToString());
            List<Order> u = ctx.Orders.Where(x => x.UserId == userId).ToList();
            return View(u);
        }

        public JsonResult AddOrder(string job, string media, string mediaCatalog, string content)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            Order o = new Order();

            o.JobType = job;
            o.Media = media;
            o.CatalogNumber = int.Parse(mediaCatalog);
            o.Content = content;
            o.UserId = int.Parse(Session["id"].ToString());
            o.Status = "PENDING";
            string response = "failed";
            ctx.Orders.Add(o);
            ctx.SaveChanges();
            response = "success";
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
