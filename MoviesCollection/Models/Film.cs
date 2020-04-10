using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesCollection.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public int idGenre { get; set; }
        public Genre Genre { get; set; }
        public string Director { get; set; }
        public string Starring { get; set; }
        public byte[] Poster { get; set; }
        public string Trailer { get; set; }


    }
}