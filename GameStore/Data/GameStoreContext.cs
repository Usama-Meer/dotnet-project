using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

    public class GameStoreContext(DbContextOptions <GameStoreContext> options):DbContext(options)
    {

        //here we need to define objects if relational database
        public DbSet<Game> Games=>Set<Game>();
        public DbSet<Genre> Genres =>Set<Genre>();

        
    }
    
