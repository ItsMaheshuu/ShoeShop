using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using SMEProject.Models;



namespace SMEProject.Controllers
{
    
    public class OrdersController : Controller
    {
        private SMEPROJECTEntities1 db = new SMEPROJECTEntities1();

        // GET: Orders
        /*public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Registration).Include(o => o.Product);
            return View(orders.ToList());
        }*/

        public ActionResult Index()
        {
            List<Order> cart = Session["Cart"] as List<Order>;
            if (cart == null)
            {
                cart = new List<Order>();
                Session["Cart"] = cart;
            }
            return View(cart);
        }

        public ActionResult CartView()
        {
            List<Order> cart = Session["Cart"] as List<Order>;
            if (cart == null)
            {
                cart = new List<Order>();
                Session["Cart"] = cart;
            }
            return View(cart);
        }

        public ActionResult AddToCart(/*int id*/)
        {
            
            return View();
        }
        // GET: Products/AddToCart
        [HttpPost]
        public ActionResult AddToCart(int productId,decimal price, int quantity,string Pimage,DateTime ddd)
        {
            List<Order> cart = Session["Cart"] as List<Order> ?? new List<Order>();
           
            Order existingItem = cart.FirstOrDefault(item => item.order_id == productId);
            if (existingItem != null)
            {
                existingItem.order_quantity += quantity;
            }
            else
            {


                cart.Add(new Order { order_id = productId, order_total = price, order_quantity = quantity, order_image = Pimage });


            }

            Session["Cart"] = cart;


            return RedirectToAction("CartView");


        }
        public ActionResult RemoveFromCart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            List<Order> cart = Session["Cart"] as List<Order>;

            if (cart != null)
            {
                cart.RemoveAll(item => item.order_id == productId);
                
                Session["Cart"] = cart;
            }
            return RedirectToAction("CartView");
        }


        public ActionResult MyOrders(int? i)
        {
            int customer_id = int.Parse(Session["id"].ToString());
            var row=db.Orders.Where(model=>model.customer_id== customer_id).ToList().ToPagedList(i ?? 1, 5);   


            return View(row);
        }

        public ActionResult CustOrders(int? i)
        {
            
            var row = db.Orders.ToList().ToPagedList(i ?? 1, 5);


            return View(row);
        }

        public ActionResult Payment(DateTime ddd)
        {
            List<Order> cart = Session["Cart"] as List<Order>;
            int itemNo = cart.Count;

            for (int i = 0; i < itemNo; i++)
            {
                var newUser = new Order
                {
                    order_id = cart[i].order_id,
                    order_image = cart[i].order_image,
                    order_date = ddd,
                    order_quantity = cart[i].order_quantity,
                    order_total = cart[i].order_total * cart[i].order_quantity,
                    customer_id = int.Parse(Session["id"].ToString()),
                    shoe_id = cart[i].order_id
                };
                db.Orders.Add(newUser);
                db.SaveChanges();
            }

            Session["Cart"] = null;
            






            return View();
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.Registrations, "customer_id", "Firstname");
            ViewBag.shoe_id = new SelectList(db.Products, "shoe_id", "shoe_image");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,order_image,order_date,order_quantity,order_total,customer_id,shoe_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.Registrations, "customer_id", "Firstname", order.customer_id);
            ViewBag.shoe_id = new SelectList(db.Products, "shoe_id", "shoe_image", order.shoe_id);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.Registrations, "customer_id", "Firstname", order.customer_id);
            ViewBag.shoe_id = new SelectList(db.Products, "shoe_id", "shoe_image", order.shoe_id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,order_image,order_date,order_quantity,order_total,customer_id,shoe_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.Registrations, "customer_id", "Firstname", order.customer_id);
            ViewBag.shoe_id = new SelectList(db.Products, "shoe_id", "shoe_image", order.shoe_id);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
