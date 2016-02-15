using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
                if (Session["type"].ToString() == "admin")
                    return RedirectToAction("Index", "Admin");
                else if (Session["type"].ToString() == "user")
                    return RedirectToAction("Index", "User");
                else
                    Session.Abandon();
            }
            return View();
        }

        // Validate Login Attempt
        public JsonResult Validate(string email, string password)
        {
            User u = null;
            string response = "failed";
            using (MyDatabaseEntities ctx = new MyDatabaseEntities())
            {
                if (email == "admin")
                {
                    u = ctx.Users.FirstOrDefault(x => x.UserId == 2);
                    if (u.Password == password)
                    {
                        response = "admin";
                        Session["id"] = u.UserId;
                        Session["type"] = "admin";
                        Session["firstname"] = u.FirstName;
                        Session["lastname"] = u.LastName;
                    }
                }
                else
                {
                    u = ctx.Users.FirstOrDefault(x => x.Email == email);
                    if (u == null)
                    {
                        try
                        {
                            u = ctx.Users.FirstOrDefault(x => x.UserId == int.Parse(email));
                        }
                        catch
                        {
                            u = null;
                        }
                    }

                    if (u != null && u.Password == password)
                    {
                        Session["id"] = u.UserId;
                        Session["type"] = "user";
                        Session["firstname"] = u.FirstName;
                        Session["lastname"] = u.LastName;
                        response = "user";
                    }
                }

            } 
            

            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        // Validate Signup Attempt
        public JsonResult SingupValidate(string firstname, string lastname, string email, string password)
        {
            string response = "";
            using (MyDatabaseEntities ctx = new MyDatabaseEntities())
            {
                User u = ctx.Users.FirstOrDefault(x => x.Email == email);
                response = "failed";
                if (u == null && email != "admin")
                {
                    u = new User();

                    u.Email = email;
                    u.FirstName = firstname;
                    u.LastName = lastname;
                    u.Password = password;

                    ctx.Users.Add(u);
                    ctx.SaveChanges();

                    Session["id"] = u.UserId;
                    Session["type"] = "user";
                    Session["firstname"] = u.FirstName;
                    Session["lastname"] = u.LastName;
                    response = "success";
                }
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public JsonResult UpdateUser(int id, string firstname, string lastname, string email, string password)
        {
            MyDatabaseEntities ctx = new MyDatabaseEntities();
            User u = ctx.Users.FirstOrDefault(x => x.UserId == id);

            string response = "failed";
            if (u != null && email != "admin")
            {
                u.Email = email;
                u.FirstName = firstname;
                u.LastName = lastname;
                u.Password = password;

                ctx.SaveChanges();
                response = "success";
                Session["firstname"] = firstname;
            }
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
