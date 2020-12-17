using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sagar_bookstore;

namespace Sagar_bookstore.Controllers
{
    public class productController : Controller
    {
        private sagarbooksEntities1 db = new sagarbooksEntities1();

        // GET: product
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.category).Include(p => p.order);
            return View(products.ToList());
        }

        // GET: product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: product/Create
        public ActionResult Create()
        {
            ViewBag.cat_id = new SelectList(db.categories, "cat_id", "cat_name");
            ViewBag.o_id = new SelectList(db.orders, "o_id", "o_name");
            return View();
        }

        // POST: product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,p_name,p_price,p_qty,p_details,cat_id,o_id")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cat_id = new SelectList(db.categories, "cat_id", "cat_name", product.cat_id);
            ViewBag.o_id = new SelectList(db.orders, "o_id", "o_name", product.o_id);
            return View(product);
        }

        // GET: product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_id = new SelectList(db.categories, "cat_id", "cat_name", product.cat_id);
            ViewBag.o_id = new SelectList(db.orders, "o_id", "o_name", product.o_id);
            return View(product);
        }

        // POST: product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,p_name,p_price,p_qty,p_details,cat_id,o_id")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_id = new SelectList(db.categories, "cat_id", "cat_name", product.cat_id);
            ViewBag.o_id = new SelectList(db.orders, "o_id", "o_name", product.o_id);
            return View(product);
        }

        // GET: product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
