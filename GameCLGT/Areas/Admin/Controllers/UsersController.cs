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
    public class UsersController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.History).Include(u => u.Vote);
            return View(users.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Histories, "ID", "ID_game");
            ViewBag.ID = new SelectList(db.Votes, "ID", "ID_game");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,User_name,User_id,User_pass,User_email,User_phone,User_avatar,User_point,User_level")] User user)
        {
            if (ModelState.IsValid)
            {
                user.ID = Guid.NewGuid();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Histories, "ID", "ID_game", user.ID);
            ViewBag.ID = new SelectList(db.Votes, "ID", "ID_game", user.ID);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Histories, "ID", "ID_game", user.ID);
            ViewBag.ID = new SelectList(db.Votes, "ID", "ID_game", user.ID);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,User_name,User_id,User_pass,User_email,User_phone,User_avatar,User_point,User_level")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Histories, "ID", "ID_game", user.ID);
            ViewBag.ID = new SelectList(db.Votes, "ID", "ID_game", user.ID);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
