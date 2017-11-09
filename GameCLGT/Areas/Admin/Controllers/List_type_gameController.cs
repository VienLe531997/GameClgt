using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CLGTgame.Areas.Admin.Models;

namespace CLGTgame.Areas.Admin.Controllers
{
    public class List_type_gameController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/List_type_game
        public ActionResult Index()
        {
            var list_type_game = db.List_type_game.Include(l => l.Game).Include(l => l.Type);
            return View(list_type_game.ToList());
        }

        // GET: Admin/List_type_game/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_type_game list_type_game = db.List_type_game.Find(id);
            if (list_type_game == null)
            {
                return HttpNotFound();
            }
            return View(list_type_game);
        }

        // GET: Admin/List_type_game/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name");
            ViewBag.ID = new SelectList(db.Types, "ID", "Type_name");
            return View();
        }

        // POST: Admin/List_type_game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_game,ID_type")] List_type_game list_type_game)
        {
            if (ModelState.IsValid)
            {
                list_type_game.ID = Guid.NewGuid();
                db.List_type_game.Add(list_type_game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", list_type_game.ID);
            ViewBag.ID = new SelectList(db.Types, "ID", "Type_name", list_type_game.ID);
            return View(list_type_game);
        }

        // GET: Admin/List_type_game/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_type_game list_type_game = db.List_type_game.Find(id);
            if (list_type_game == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", list_type_game.ID);
            ViewBag.ID = new SelectList(db.Types, "ID", "Type_name", list_type_game.ID);
            return View(list_type_game);
        }

        // POST: Admin/List_type_game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_game,ID_type")] List_type_game list_type_game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(list_type_game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", list_type_game.ID);
            ViewBag.ID = new SelectList(db.Types, "ID", "Type_name", list_type_game.ID);
            return View(list_type_game);
        }

        // GET: Admin/List_type_game/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_type_game list_type_game = db.List_type_game.Find(id);
            if (list_type_game == null)
            {
                return HttpNotFound();
            }
            return View(list_type_game);
        }

        // POST: Admin/List_type_game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            List_type_game list_type_game = db.List_type_game.Find(id);
            db.List_type_game.Remove(list_type_game);
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
