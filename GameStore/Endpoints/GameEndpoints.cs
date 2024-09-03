using System.Text.RegularExpressions;
using AutoMapper;
using GameStore.Data;
using GameStore.Dtos;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Endpoints;

public static class GameEndpoints
{
    public const string getGameEndpoint="games";
    private static readonly List<GameSummaryDto> games=[
        new (
            1,
            "Street Fighter",
            "Fighting",
            20.99M,
            new DateOnly(2010,01,01)
        ),
        new (
            4,
            "Street Fighter II",
            "Fighting",
            30.99M,
            new DateOnly(2014,01,01)
        ),
        new (
            3,
            "Need For Speed",
            "Racing",
            20.99M,
            new DateOnly(2015,01,01)
        )

];

    public static RouteGroupBuilder mapRouteEndpoints(this WebApplication app){
        
        var group=app.MapGroup("games").WithParameterValidation();

        //GET / --homepage
        
        //GET/games
        group.MapGet("/",async (GameStoreContext dbContext)=>
        
        await dbContext.Games
        .Include(game=>game.Genre)
        .Select(game=>game.ToSummaryDto())
        .AsNoTracking()
        .ToListAsync()
        );

        //GET/games/{id}
        group.MapGet("{id}",async (int id, GameStoreContext dbContext)=>{
            // GameDto? game=games.Find(game=>game.Id==id);

            //This is a new updated code that use Game object context
            // Game? game=dbContext.Games.Find(id);

            var game=await dbContext.Games.FindAsync(id);

            
            
            return game is null?Results.NotFound():Results.Ok(game.ToDetailDto());
            
        });

        //POST/games/
        group.MapPost("/",async (IMapper _mapper,CreateGameDto newGame, GameStoreContext dbContext)=>{


            // GameDto? game= new(
            //     games.Count+1,
            //     newGame.Name,
            //     newGame.Genre,
            //     newGame.Price,
            //     newGame.ReleaseDate
            // );


            //This is moved to the gameMapping file and now will be called using the ToEntity method
            
            // Game game=new(){
            //     Name=newGame.Name,
            //     Genre=dbContext.Genres.Find(newGame.GenreId),
            //     GenreId=newGame.GenreId,
            //     Price=newGame.Price,
            //     ReleaseDate=newGame.ReleaseDate
            // };

            
            //using this command we are calling the mapping game methods
            // Game game=newGame.ToEntity();

            
            Game game=_mapper.Map<Game>(newGame);
            
            

            //using this line we are going to add Genre
            
            //This line is removed bcoz Genre Name is added into the extension class already
            // game.Genre=dbContext.Genres.Find(newGame.GenreId);

            // games.Add(game);
            //adding into the database
            await dbContext.Games.AddAsync(game);
            


            //save changes into the database
            await dbContext.SaveChangesAsync();
            

            
            //This is done to respond to the game to the web back so that not id of the genre of is send 
            
            /* This is moved to the gameMapping file and now will be called using the ToDto method*/
            // GameDto gameDto= new(
            //     game.Id,
            //     game.Name,
            //     //! is added bcoz if genre is null although it can never be null. This is added to end error from code
            //     game.Genre!.Name,
            //     game.Price,
            //     game.ReleaseDate
            // );
            
            //here game.ToDto() is added to import ToDto from mappingGame Extension
            
GameDetailDto gamedto=_mapper.Map<GameDetailDto>(game);
            // return Results.CreatedAtRoute(getGameEndpoint, new {id=game.Id},game.ToDetailDto());

            return Results.CreatedAtRoute(getGameEndpoint, new {id=game.Id},gamedto);

        });

        //PUT/games/{id}
        group.MapPut("/{id}",async (int id, UpdateGameDto updateGame, GameStoreContext dbContext)=>
        {
            //await is used for asynchronous coding 
            var existingGame= await dbContext.Games.FindAsync(id);

            if (existingGame is null) 
            {
                return Results.NotFound();
            }
            else {
                dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));

                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            }


            // var index=games.FindIndex(game=>game.Id==id);

            // if (index >= 0) {
            //     games[index]=new (
            //         id,
            //         updateGame.Name,
            //         updateGame.Genre,
            //         updateGame.Price,
            //         updateGame.ReleaseDate
            //     );
                // return Results.NoContent();
                // return Results.Redirect("https://localhost:5062/games");
                // return Results.RedirectToRoute(getGameEndpoint);
            

        });


        //DELETE/{id}
        group.MapDelete("/{id}",async (int id, GameStoreContext dbContext)=>{
            // var index=games.FindIndex(game=>game.Id==id);
            // if (index>=0) {
            //     games.RemoveAt(index);
            //     return Results.NoContent();
            // }
            // else{
            //     return Results.NotFound();
            // }    
            await dbContext.Games.Where(game=>game.Id==id).ExecuteDeleteAsync();
            return Results.NoContent();

        }).WithName(getGameEndpoint);


        return group;
    }
}
