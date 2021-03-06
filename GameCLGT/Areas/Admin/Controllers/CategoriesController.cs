﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PagedList;
using GameCLGT.Areas.Admin.Models;

namespace CLGTgame.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private CLGTgame_item_data_mainEntities db = new CLGTgame_item_data_mainEntities();

        // GET: Admin/Categories
        public ActionResult Index(int? page)
        {
            var categories = db.Categories.Include(c => c.List_game_cate);
            var pt = page ?? 1;
            var content = categories.OrderBy(c => c.ID).ToPagedList(pt, 20);
            ViewBag.Content = content;
            return View(content);
        }
        [HttpPost]
        public ActionResult Index(FormCollection f, int? page)
        {
            string s = f["search"];
            var categories = db.Categories.Where(c => c.Category_name.Contains(s));
            var pt = page ?? 1;
            var content = categories.OrderBy(c => c.ID).ToPagedList(pt, 20);
            ViewBag.Content = content;
            return View(content);
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.List_game_cate, "ID", "ID_game");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Category_name,Category_image")] Category category, HttpPostedFileBase file)
        {
            if(file != null)
            {
                string img = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Images/Categories"), img);
                file.SaveAs(path);
                category.Category_image = "/Content/Images/Categories/" + file.FileName;
            }

            if (ModelState.IsValid)
            {
                category.ID = Guid.NewGuid();
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.List_game_cate, "ID", "ID_game", category.ID);
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.List_game_cate, "ID", "ID_game", category.ID);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Category_name,Category_image")] Category category, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/Images/Categories/"), fileName);
                file.SaveAs(filePath);
                category.Category_image = "/Content/Images/Categories/" + file.FileName;
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.List_game_cate, "ID", "ID_game", category.ID);
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Category category = db.Categories.Find(id);

            var fileName = category.Category_image;
            var filePath = Server.MapPath(fileName);
            System.IO.File.Delete(filePath);

            List<List_game_cate> lstCate = new List<List_game_cate>();
            foreach(var item in lstCate)
            {
                db.List_game_cate.Remove(item);
            }

            db.Categories.Remove(category);
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
