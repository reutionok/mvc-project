using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoviesCollection.Models;
using System.IO;

namespace MoviesCollection.Controllers
{
    public class FilmsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Films
        public  ActionResult Index()
        {
             IEnumerable<Film> films = db.Films;
            ViewBag.Films = films;
            SelectList genres = new SelectList(db.Genres, "Id", "Name");
            ViewBag.Genres = genres;
            ViewBag.GenreList = db.Genres.Where(x => x.Id > 0).ToList<Genre>();
            return View();
        }

        // GET: Films/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = await db.Films.FindAsync(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.Genres = db.Genres.Where(x => x.Id > 0).ToList<Genre>();
            return View(film);
        }

        // GET: Films/Create
        [HttpGet]
        public ActionResult Create()
        {
            SelectList genres = new SelectList(db.Genres, "Id", "Name");
            ViewBag.Genres = genres;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Film film, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                film.Poster = imageData;
                film.Genre = db.Genres.Where(g => g.Id == film.idGenre).FirstOrDefault();
                db.Films.Add(film);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Create");
        }


        // GET: Films/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = await db.Films.FindAsync(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            SelectList genres = new SelectList(db.Genres, "Id", "Name");
            ViewBag.Genres = genres;
            return View(film);
        }

        // POST: Films/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Film film, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    // установка массива байтов
                    film.Poster = imageData;
                }
                else
                {
                    film.Poster = db.Films.Where(i => i.Id == film.Id).Select(i => i.Poster).FirstOrDefault();
                }
                film.Genre = db.Genres.Where(g => g.Id == film.idGenre).FirstOrDefault();
                db.Entry(film).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Films/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = await db.Films.FindAsync(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.Genres = db.Genres.Where(x => x.Id > 0).ToList<Genre>();
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Film film = await db.Films.FindAsync(id);
            db.Films.Remove(film);
            await db.SaveChangesAsync();
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
