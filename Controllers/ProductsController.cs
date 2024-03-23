using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMEProject.Models;
using PagedList.Mvc;
using PagedList;


namespace SMEProject.Controllers
{
    
    public class ProductsController : Controller
    {
        private SMEPROJECTEntities1 db = new SMEPROJECTEntities1();

        // GET: Products
       
        public ActionResult Index(int? i)
        {
            /*List<Product> product = db.Products.ToList();
            ViewBag.Products = new SelectList(product, "shoe_brand");*/
            return View(db.Products.ToList().ToPagedList(i ?? 1, 4));
        }
        [HttpPost]
        public ActionResult Index(string Search, int? i)
        {
            if (Search != null)
            {
                return View(db.Products.Where(x => x.shoe_brand.Contains(Search) || x.shoe_type.Contains(Search) || x.shoe_name.Contains(Search) || x.shoe_color.Contains(Search)).ToList().ToPagedList(i ?? 1, 4));
            }
            return View(db.Products.ToList().ToPagedList(i ?? 1, 4));


        }

        

        public ActionResult UserDisplay(int? i)

        {
            return View(db.Products.ToList().ToPagedList(i ?? 1, 12));
        }

            // GET: Products/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                filename = filename + extension;
                product.shoe_image = "~/ProductImg/" + filename;
                filename = Path.Combine(Server.MapPath("~/ProductImg"), filename);
                product.ImageFile.SaveAs(filename);
                db.Products.Add(product);
                int a = db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "shoe_id,shoe_image,shoe_name,shoe_brand,shoe_type,shoe_color,shoe_size,shoe_quantity,shoe_price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
