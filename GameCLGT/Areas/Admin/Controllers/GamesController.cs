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
    public class GamesController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/Games
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.History).Include(g => g.List_game_cate).Include(g => g.List_type_game).Include(g => g.Vote);
            return View(games.ToList());
        }

        // GET: Admin/Games/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Admin/Games/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Histories, "ID", "ID_game");
            ViewBag.ID = new SelectList(db.List_game_cate, "ID", "ID_game");
            ViewBag.ID = new SelectList(db.List_type_game, "ID", "ID_game");
            ViewBag.ID = new SelectList(db.Votes, "ID", "ID_game");
            return View();
        }

        // POST: Admin/Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Game_name,Game_link,Game_detail,Game_description,Game_rate,Game_image,Game_countPlay,Game_KPIpoint,Game_KPIexp")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.ID = Guid.NewGuid();
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Histories, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.List_game_cate, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.List_type_game, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.Votes, "ID", "ID_game", game.ID);
            return View(game);
        }

        // GET: Admin/Games/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Histories, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.List_game_cate, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.List_type_game, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.Votes, "ID", "ID_game", game.ID);
            return View(game);
        }

        // POST: Admin/Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Game_name,Game_link,Game_detail,Game_description,Game_rate,Game_image,Game_countPlay,Game_KPIpoint,Game_KPIexp")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Histories, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.List_game_cate, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.List_type_game, "ID", "ID_game", game.ID);
            ViewBag.ID = new SelectList(db.Votes, "ID", "ID_game", game.ID);
            return View(game);
        }

        // GET: Admin/Games/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Admin/Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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
