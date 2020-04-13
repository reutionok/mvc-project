using MoviesCollection.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MoviesCollection.Controllers
{
    public class HomeController : Controller
    {
        MyDbContext db = new MyDbContext();
        public ActionResult Index()
        {           
            IEnumerable<Film> films = db.Films;
            ViewBag.Films = films;
            SelectList genres = new SelectList(db.Genres, "Id", "Name");
            ViewBag.Genres = genres;
            return View();
        }

        [HttpPost]
        public ActionResult FilmSearch(Film film )
        {
            var allfilms = new List<Film>();

            if (film.idGenre != 0)
            {
                allfilms = db.Films.Where(g => g.idGenre.Equals(film.idGenre)).ToList();
            }
            else
            {
                allfilms = db.Films.ToList();
            }
            if (allfilms.Count <= 0)
            {
                ViewBag.Message = "К сожалению фильмов данного жанра нет на сайте";
                return PartialView("NotFound");
            }
            return PartialView(allfilms);
        }

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

       
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}