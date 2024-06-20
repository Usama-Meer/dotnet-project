using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

    public class GameStoreContext(DbContextOptions <GameStoreContext> options):DbContext(options)
    {

        //here we need to define objects if relational database
        public DbSet<Game> Games=>Set<Game>();
        public DbSet<Genre> Genres =>Set<Genre>();


        //This is going to add the following data into the table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new {Id=1,Name="Fighting"},
                new {Id=2,Name="Roleplaying"},
                new {Id=3, Name="Sports"},
                new {Id=4, Name="Racing"},
                new {Id=5, Name="Kids and Family"}
            );
    }

}
    
