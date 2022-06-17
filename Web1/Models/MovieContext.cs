using Microsoft.EntityFrameworkCore;

namespace Movies.Models {
    public class MovieContext: DbContext {
        public DbSet<MoviesModel> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            =>options.UseSqlite("Data Source=Movies.db");
        
    }
}