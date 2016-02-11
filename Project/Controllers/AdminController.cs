using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Project.Models;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (Session["id"] != null && Session["type"].ToString() == "user")
                return RedirectToAction("Index", "User");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");


            ViewBag.visible = 1;
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            List<Order> or = ctx.Orders.Where(x => x.Status == "PENDING").ToList();
            return View(or);

        }

        public ActionResult Orders()
        {
            if (Session["id"] != null && Session["type"].ToString() == "user")
                return RedirectToAction("Index", "User");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");

            ViewBag.visible = 1;
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            List<Order> or = ctx.Orders.Where(x => x.Status != "PENDING" && x.Status != "DISCARD").ToList();
            return View(or);
        }

        public ActionResult Setting()
        {
            if (Session["id"] != null && Session["type"].ToString() == "user")
                return RedirectToAction("Index", "User");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");

            ViewBag.visible = 4;
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            int userId = int.Parse(Session["id"].ToString());
            User u = ctx.Users.FirstOrDefault(m => m.Id == userId);
            return View(u);
        }

        public ActionResult Users()
        {
            if (Session["id"] != null && Session["type"].ToString() == "user")
                return RedirectToAction("Index", "User");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");

            ViewBag.visible = 3;
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            List<User> list = ctx.Users.Where(x => x.Id != 1).ToList();
            return View(list);
        }

        public ActionResult Catalog()
        {
            if (Session["id"] != null && Session["type"].ToString() == "user")
                return RedirectToAction("Index", "User");
            else if (Session["id"] == null)
                return RedirectToAction("Index", "Login");

            ViewBag.visible = 2;
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            List<Catalog> cat = ctx.Catalogs.ToList();
            return View(cat);
        }

        public JsonResult UpdateCatalog(int id, string name)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            Catalog u = ctx.Catalogs.FirstOrDefault(x => x.Id == id);

            string response = "failed";
            if (u != null)
            {
                u.Name = name;
                ctx.SaveChanges();
                response = "success";
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCatalog(int id)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            Catalog u = ctx.Catalogs.FirstOrDefault(x => x.Id == id);

            string response = "failed";
            if (u != null )
            {
                ctx.Catalogs.Remove(u);
                ctx.SaveChanges();
                response = "success";
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCatalog(string name)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            Catalog u = new Catalog();
            u.Name = name;
            ctx.Catalogs.Add(u);
            ctx.SaveChanges();
            string response = "success";
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUser(int id)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            User u = ctx.Users.FirstOrDefault(x => x.Id == id);

            return View(u);
        }

        public JsonResult DeleteUser(int id)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            User u = ctx.Users.FirstOrDefault(x => x.Id == id);

            string response = "failed";
            if (u != null)
            {
                ctx.Users.Remove(u);
                ctx.SaveChanges();
                response = "success";
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateOrder(int id)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            Order u = ctx.Orders.FirstOrDefault(x => x.Id == id);

            string response = "failed";
            if (u != null)
            {
                u.Status = "VALIDATE";
                ctx.SaveChanges();
                response = "success";
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DiscardOrder(int id)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            Order u = ctx.Orders.FirstOrDefault(x => x.Id == id);

            string response = "failed";
            if (u != null)
            {
                u.Status = "DISCARD";
                ctx.SaveChanges();
                response = "success";
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompleteOrder(int id)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            Order u = ctx.Orders.FirstOrDefault(x => x.Id == id);

            string response = "failed";
            if (u != null)
            {
                u.Status = "DONE";
                ctx.SaveChanges();
                response = "success";
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}
