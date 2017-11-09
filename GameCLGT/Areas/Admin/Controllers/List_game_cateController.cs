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
    public class List_game_cateController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/List_game_cate
        public ActionResult Index()
        {
            var list_game_cate = db.List_game_cate.Include(l => l.Category).Include(l => l.Game);
            return View(list_game_cate.ToList());
        }

        // GET: Admin/List_game_cate/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_game_cate list_game_cate = db.List_game_cate.Find(id);
            if (list_game_cate == null)
            {
                return HttpNotFound();
            }
            return View(list_game_cate);
        }

        // GET: Admin/List_game_cate/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Categories, "ID", "Category_name");
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name");
            return View();
        }

        // POST: Admin/List_game_cate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_game,ID_category")] List_game_cate list_game_cate)
        {
            if (ModelState.IsValid)
            {
                list_game_cate.ID = Guid.NewGuid();
                db.List_game_cate.Add(list_game_cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Categories, "ID", "Category_name", list_game_cate.ID);
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", list_game_cate.ID);
            return View(list_game_cate);
        }

        // GET: Admin/List_game_cate/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_game_cate list_game_cate = db.List_game_cate.Find(id);
            if (list_game_cate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Categories, "ID", "Category_name", list_game_cate.ID);
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", list_game_cate.ID);
            return View(list_game_cate);
        }

        // POST: Admin/List_game_cate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_game,ID_category")] List_game_cate list_game_cate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(list_game_cate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Categories, "ID", "Category_name", list_game_cate.ID);
            ViewBag.ID = new SelectList(db.Games, "ID", "Game_name", list_game_cate.ID);
            return View(list_game_cate);
        }

        // GET: Admin/List_game_cate/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List_game_cate list_game_cate = db.List_game_cate.Find(id);
            if (list_game_cate == null)
            {
                return HttpNotFound();
            }
            return View(list_game_cate);
        }

        // POST: Admin/List_game_cate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            List_game_cate list_game_cate = db.List_game_cate.Find(id);
            db.List_game_cate.Remove(list_game_cate);
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
