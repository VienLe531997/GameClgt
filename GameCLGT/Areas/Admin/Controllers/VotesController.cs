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
    public class VotesController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/Votes
        public ActionResult Index()
        {
            var votes = db.Votes.Include(v => v.Game).Include(v => v.User);
            return View(votes.ToList());
        }

        // GET: Admin/Votes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            return View(vote);
        }

        // GET: Admin/Votes/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name");
            ViewBag.ID = new SelectList(db.Users, "ID", "User_name");
            return View();
        }

        // POST: Admin/Votes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_game,ID_user,Vote_rate")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                vote.ID = Guid.NewGuid();
                db.Votes.Add(vote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", vote.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "User_name", vote.ID);
            return View(vote);
        }

        // GET: Admin/Votes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", vote.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "User_name", vote.ID);
            return View(vote);
        }

        // POST: Admin/Votes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_game,ID_user,Vote_rate")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", vote.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "User_name", vote.ID);
            return View(vote);
        }

        // GET: Admin/Votes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return HttpNotFound();
            }
            return View(vote);
        }

        // POST: Admin/Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Vote vote = db.Votes.Find(id);
            db.Votes.Remove(vote);
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
