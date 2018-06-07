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
    public class AutorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Autors
        public ActionResult Index()
        {
            return View(db.Autor.ToList());
        }

        // GET: Autors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // GET: Autors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,DataNasc,Descricao,Fotografia")] Autor autor, HttpPostedFileBase uploadFotografia)
        {
            if (ModelState.IsValid)
            {
                db.Autor.Add(autor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ADICIONADO
            // especificar (escolher) o nome do ficheiro
            string nomeImagem = "Autor_" + autor + ".jpg";

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
                autor.Fotografia = nomeImagem;
            } else {
                // não foi fornecido qq ficheiro
                // gerar uma mensagem de erro
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                // devolver o controlo à View
                return View(autor);
            }
            // ModelState.IsValid -> confronta os dados fornecidos da View
            //                       com as exigências do Modelo
            if (ModelState.IsValid) {
                try {
                    // adiciona o novo Autor à BD
                    db.Autor.Add(autor);
                    // faz 'Commit' às alterações
                    db.SaveChanges();
                    // escrever o ficheiro com a fotografia no disco rígido, na pasta 'imagens'
                    uploadFotografia.SaveAs(path);

                    // se tudo correr bem, redireciona para a página de Index
                    return RedirectToAction("Index");
                } catch (Exception) {
                    ModelState.AddModelError("", "Houve um erro com a criação do novo Autor...");            

                }
            }
            //ATÉ AQUI

            return View(autor);
        }

        // GET: Autors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,DataNasc,Descricao,Fotografia")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: Autors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autor autor = db.Autor.Find(id);
            db.Autor.Remove(autor);
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
