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
    public class HistoriesController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/Histories
        public ActionResult Index()
        {
            var histories = db.Histories.Include(h => h.Game).Include(h => h.User);
            return View(histories.ToList());
        }

        // GET: Admin/Histories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // GET: Admin/Histories/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name");
            ViewBag.ID = new SelectList(db.Users, "ID", "User_name");
            return View();
        }

        // POST: Admin/Histories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_game,ID_user,History_date")] History history)
        {
            if (ModelState.IsValid)
            {
                history.ID = Guid.NewGuid();
                db.Histories.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", history.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "User_name", history.ID);
            return View(history);
        }

        // GET: Admin/Histories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", history.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "User_name", history.ID);
            return View(history);
        }

        // POST: Admin/Histories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_game,ID_user,History_date")] History history)
        {
            if (ModelState.IsValid)
            {
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", history.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "User_name", history.ID);
            return View(history);
        }

        // GET: Admin/Histories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // POST: Admin/Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            History history = db.Histories.Find(id);
            db.Histories.Remove(history);
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
