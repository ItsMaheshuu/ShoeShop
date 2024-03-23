using SMEProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMEProject.Controllers
{
    
    public class HomeController : Controller
    {
        SMEPROJECTEntities1 db= new SMEPROJECTEntities1();
        
        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult About()
        {
     

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
       
        
        public ActionResult ManageCustomers()
        {
            return View(db.Registrations.ToList());
        }
        






        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        //POST
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Registrations.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                if (user.IsAdmin)
                {
                    Session["Username"] = user.Username.ToString();
                    Session["id"] = user.customer_id;
                    Session["CustomerId"] = user.customer_id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string customer_id = Convert.ToString(user.customer_id);
                    Session["Username"] = user.Username.ToString();
                    Session["id"] = user.customer_id;
                    Session["CustomerId"] = customer_id;
                    return RedirectToAction("UserDisplay", "Products", new { id = customer_id });
                }
            }
            else
            {
                ViewBag.Error = "Invalid username and Password";
                return View();
            }


        }
        //GET
       
        public ActionResult SignUp()
        {
            return View();
        }

       
        //POST
        [HttpPost]
        public ActionResult SignUp(Registration rs)
        {
            if (db.Registrations.Any(u => u.Username == rs.Username))
            {
                ViewBag.Error = "Username is already exists. Please choose different name.";
                return View();
            }
            var newUser = new Registration
            {
                Username = rs.Username,
                Password = rs.Password,
                Email = rs.Email,
                Firstname = rs.Firstname,
                Lastname = rs.Lastname,
                Phone_no = rs.Phone_no,
                Address = rs.Address,
                IsAdmin = false // i want only one admin
            };
            db.Registrations.Add(newUser);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}