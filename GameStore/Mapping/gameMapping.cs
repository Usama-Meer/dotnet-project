using GameStore.Dtos;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class gameMapping
{
    //This is an extension method that takes parameter as CreateGameDto and it return data to the entity(for storing in database)
   public static Game ToEntity(this CreateGameDto game)
    {
        //This is returning game to the GameEndpoint where method is called
        return new Game()
        {
            Name=game.Name,
            GenreId=game.GenreId,
            Price=game.Price,
            ReleaseDate=game.ReleaseDate
        };

    }

    //here we are sending data to the GameDto

    //here we are creating ToDto method with Game(entity) as parameter and this will return the data for response
    public static GameSummaryDto ToSummaryDto(this Game game)
    {

        //returning data to the dto for response and this will be called in GameEndpoints
        return new 
        (
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate


        );
    }


    //This is created to add Genre id

       public static Game ToEntity(this UpdateGameDto game, int id)
    {
        //This is returning game to the GameEndpoint where method is called
        return new Game()
        {
            Id=id,
            Name=game.Name,
            GenreId=game.GenreId,
            Price=game.Price,
            ReleaseDate=game.ReleaseDate
        };
    }

    

    public static GameDetailDto ToDetailDto(this Game game)
    {

        //returning data to the dto for response and this will be called in GameEndpoints
        return new 
        (
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate


        );
    }
}
