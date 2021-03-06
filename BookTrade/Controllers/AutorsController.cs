﻿using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookTrade.Models;

namespace BookTrade.Controllers {
    public class AutorsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Autors
        public ActionResult Index() {
            return View(db.Autor.ToList());
        }

        // GET: Autors/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null) {
                return HttpNotFound();
            }
            return View(autor);
        }

        // GET: Autors/Create
        [Authorize(Roles = "Admin")] //Administrador
        public ActionResult Create() {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] //Administrador
        public ActionResult Create([Bind(Include = "Id,Nome,DataNasc,Descricao,AutorFotografia")] Autor autor, HttpPostedFileBase AutorFotografia) {
            //Introdução da fotografia
            if (AutorFotografia != null) {
                autor.AutorFotografia = AutorFotografia.FileName;
            }
            if (ModelState.IsValid) {
                db.Autor.Add(autor);
                db.SaveChanges();
                //gravação da capa do livro
                AutorFotografia.SaveAs(Path.Combine(Server.MapPath("~/imagens/" + AutorFotografia.FileName)));
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: Autors/Edit/5
        [Authorize(Roles = "Admin")] //Administrador
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null) {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] //Administrador
        public ActionResult Edit([Bind(Include = "Id,Nome,DataNasc,Descricao,AutorFotografia")] Autor autor) {
            if (ModelState.IsValid) {
                db.Entry(autor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: Autors/Delete/5
        [Authorize(Roles = "Admin")] //Administrador
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null) {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] //Administrador
        public ActionResult DeleteConfirmed(int id) {
            Autor autor = db.Autor.Find(id);
            db.Autor.Remove(autor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
