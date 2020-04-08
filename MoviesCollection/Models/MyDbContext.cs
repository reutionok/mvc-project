using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesCollection.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnection")
        {
        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
     }

   
}