using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookTrade.Models;

namespace BookTrade.Controllers
{
    public class LivrosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Livros
        public ActionResult Index()
        {
            var livro = db.Livro.Include(l => l.Autores);
            return View(livro.ToList());
        }

        // GET: Livros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // GET: Livros/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.AutorId = new SelectList(db.Autor, "Id", "Nome");
            ViewBag.Categorias = new SelectList(db.Categorias, "Id", "Nome");
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Titulo,Sinopse,AnoLanc,Editora,Idioma,NumeroDePaginas,AutorId,Fotografia")] Livro livro, HttpPostedFileBase Fotografia, List<int> Categorias)
        {
            if(Fotografia != null)
            {
                livro.Fotografia = Fotografia.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Livro.Add(livro);
                IQueryable<Categorias> temp2 = db.Categorias.Where(a => Categorias.Any(aa => a.Id == aa));
                livro.Categorias = temp2.ToList();
                db.SaveChanges();
                Fotografia.SaveAs(Path.Combine(Server.MapPath("~/imagens/" + Fotografia.FileName)));
                return RedirectToAction("Index");
            }

            ViewBag.AutorId = new SelectList(db.Autor, "Id", "Nome", livro.AutorId);
            return View(livro);
        }

        // GET: Livros/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutorId = new SelectList(db.Autor, "Id", "Nome", livro.AutorId,"Fotografia");
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Sinopse,AnoLanc,Editora,Idioma,NumeroDePaginas,AutorId,Fotografia")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutorId = new SelectList(db.Autor, "Id", "Nome", livro.AutorId,"Fotografia");
            return View(livro);
        }

        // GET: Livros/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livro.Find(id);
            livro.Categorias.Clear();
            db.Livro.Remove(livro);
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

        // GET: About
        public ActionResult About() {
            return View();
        }
    }
}
