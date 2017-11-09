using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CLGTgame.Areas.Admin.Models;
using PagedList;
using System.IO;

namespace CLGTgame.Areas.Admin.Controllers
{
    public class TypesController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/Types
        public ActionResult Index(int? page)
        {
            var types = db.Types.Include(t => t.List_type_game);
            var pt = page ?? 1;
            var content = types.OrderBy(t => t.ID).ToPagedList(pt, 20);
            ViewBag.Content = content;
            return View(content);
        }
        [HttpPost]
        public ActionResult Index(int? page, FormCollection f)
        {
            string s = f["search"];
            var types = db.Types.Where(t => t.Type_name.Contains(s));
            var pt = page ?? 1;
            var content = types.OrderBy(t => t.ID).ToPagedList(pt, 20);
            ViewBag.Content = content;
            return View(content);
        }

        // GET: Admin/Types/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: Admin/Types/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.List_type_game, "ID", "ID_game");
            return View();
        }

        // POST: Admin/Types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type_name,Type_image")] Models.Type type, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string img = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Images/Types"), img);
                file.SaveAs(path);
                type.Type_image = "/Content/Images/Types/" + file.FileName;
            }

            if (ModelState.IsValid)
            {
                type.ID = Guid.NewGuid();
                db.Types.Add(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.List_type_game, "ID", "ID_game", type.ID);
            return View(type);
        }

        // GET: Admin/Types/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.List_type_game, "ID", "ID_game", type.ID);
            return View(type);
        }

        // POST: Admin/Types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type_name,Type_image")] Models.Type type, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images/Types/"), fileName);
                file.SaveAs(filePath);
                type.Type_image = "/Content/Images/Types/" + file.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.List_type_game, "ID", "ID_game", type.ID);
            return View(type);
        }

        // GET: Admin/Types/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Admin/Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Models.Type type = db.Types.Find(id);

            var fileName = type.Type_image;
            var filePath = Server.MapPath(fileName);
            System.IO.File.Delete(filePath);

            List<List_type_game> lstType = new List<List_type_game>();
            foreach(var item in lstType)
            {
                db.List_type_game.Remove(item);
            }

            db.Types.Remove(type);
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
