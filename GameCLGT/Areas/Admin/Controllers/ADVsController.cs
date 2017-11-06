using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameCLGT.Areas.Admin.Models;

namespace GameCLGT.Areas.Admin.Controllers
{
    public class ADVsController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/ADVs
        public ActionResult Index()
        {
            return View(db.ADVs.ToList());
        }

        // GET: Admin/ADVs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADV aDV = db.ADVs.Find(id);
            if (aDV == null)
            {
                return HttpNotFound();
            }
            return View(aDV);
        }

        // GET: Admin/ADVs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ADVs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ADV_name,ADV_image,ADV_link,ADV_description")] ADV aDV)
        {
            if (ModelState.IsValid)
            {
                aDV.ID = Guid.NewGuid();
                db.ADVs.Add(aDV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aDV);
        }

        // GET: Admin/ADVs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADV aDV = db.ADVs.Find(id);
            if (aDV == null)
            {
                return HttpNotFound();
            }
            return View(aDV);
        }

        // POST: Admin/ADVs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ADV_name,ADV_image,ADV_link,ADV_description")] ADV aDV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aDV);
        }

        // GET: Admin/ADVs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADV aDV = db.ADVs.Find(id);
            if (aDV == null)
            {
                return HttpNotFound();
            }
            return View(aDV);
        }

        // POST: Admin/ADVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ADV aDV = db.ADVs.Find(id);
            db.ADVs.Remove(aDV);
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
