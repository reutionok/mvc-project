using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace MoviesCollection.Models
{
    public class FilmDbInitializer : DropCreateDatabaseAlways<MyDbContext>
    {
        protected override void Seed(MyDbContext db)
        {
            db.Genres.Add(new Genre { Name = "Трагикомедия" });
            db.Genres.Add(new Genre { Name = "Комедия" });
            db.Genres.Add(new Genre { Name = "Мелодрама" });
            db.Genres.Add(new Genre { Name = "Триллер" });
            db.Genres.Add(new Genre { Name = "Ужасы" });
            db.Genres.Add(new Genre { Name = "Мультфильм" });
            base.Seed(db);
        }
    }
}