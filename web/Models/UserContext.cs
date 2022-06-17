using Microsoft.EntityFrameworkCore;


namespace RoiWeb.Models{
    public class UserContext : DbContext{

        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            =>options.UseSqlite(@"Data Source=users.db");
        
    }
}