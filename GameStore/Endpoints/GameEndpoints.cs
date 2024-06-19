using System.Text.RegularExpressions;
using GameStore.Dtos;
namespace GameStore.Endpoints;

public static class GameEndpoints
{
    public const string getGameEndpoint="games";
    private static readonly List<GameDto> games=[
        new (
            1,
            "Street Fighter",
            "Fighting",
            20.99M,
            new DateOnly(2010,01,01)
        ),
        new (
            2,
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
        group.MapGet("/",()=>games);

        //GET/games/{id}
        group.MapGet("{id}",(int id)=>{
            GameDto? game=games.Find(game=>game.Id==id);
            
            return game is null?Results.NotFound():Results.Ok(game);
            
        });

        //POST/games/
        group.MapPost("/",(CreateGameDto newGame)=>{

            GameDto? game= new(
                games.Count+1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);
            return Results.CreatedAtRoute(getGameEndpoint,game);


        });

        //PUT/games/{id}
        group.MapPut("/{id}",(int id, UpdateGameDto updateGame)=>{
            var index=games.FindIndex(game=>game.Id==id);

            if (index >= 0) {
                games[index]=new (
                    id,
                    updateGame.Name,
                    updateGame.Genre,
                    updateGame.Price,
                    updateGame.ReleaseDate
                );
                // return Results.NoContent();
                // return Results.Redirect("https://localhost:5062/games");
                return Results.RedirectToRoute(getGameEndpoint);
            }
            else{
                return Results.NotFound();
            }

        });


        //DELETE/{id}
        group.MapDelete("/{id}",(int id)=>{
            var index=games.FindIndex(game=>game.Id==id);

            if (index>=0) {
                games.RemoveAt(index);
                return Results.NoContent();
            }
            else{
                return Results.NotFound();
            }
        }).WithName(getGameEndpoint);


        return group;
    }
}
