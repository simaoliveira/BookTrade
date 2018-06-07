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
    public class LivroesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Livroes
        public ActionResult Index()
        {
            var livro = db.Livro.Include(l => l.Autores);
            return View(livro.ToList());
        }

        // GET: Livroes/Details/5
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

        // GET: Livroes/Create
        public ActionResult Create()
        {
            ViewBag.AutorId = new SelectList(db.Autor, "Id", "Nome");
            return View();
        }

        // POST: Livroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Sinopse,AnoLanc,Editora,Idioma,NumeroDePaginas,AutorId,Fotografia")] Livro livro, HttpPostedFileBase uploadFotografia)
        {
            if (ModelState.IsValid)
            {
                db.Livro.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // especificar (escolher) o nome do ficheiro
            string nomeImagem = "Livro_" + livro + ".jpg";

            // var. auxiliar
            string path = "";
            // validar se a imagem foi fornecida
            if (uploadFotografia != null) {
                // o ficheiro foi fornecido
                // validar se o q foi fornecido é uma imagem ----> fazer em casa
                // formatar o tamanho da imagem

                // criar o caminho completo até ao sítio onde o ficheiro
                // será guardado
                path = Path.Combine(Server.MapPath("~/imagens/"), nomeImagem);

                // guardar o nome do ficheiro na BD
                livro.Fotografia = nomeImagem;
            } else {
                // não foi fornecido qq ficheiro
                // gerar uma mensagem de erro
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                // devolver o controlo à View
                return View(livro);
            }


            ViewBag.AutorId = new SelectList(db.Autor, "Id", "Nome", livro.AutorId, "Fotografia");
            return View(livro);
        }

        // GET: Livroes/Edit/5
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

        // POST: Livroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Livroes/Delete/5
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

        // POST: Livroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livro.Find(id);
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
    }
}
