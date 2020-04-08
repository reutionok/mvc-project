using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesCollection.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Film> Films { get; set; }
        public Genre()
        {
            Films = new List<Film>();
        }
    }
}