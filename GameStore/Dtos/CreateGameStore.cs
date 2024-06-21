using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record class CreateGameDto
( 
    
    [Required][StringLength(100)] string Name,

    // [Required][StringLength(100)] string GenreId,
    //updated GenreId
    [Required] int GenreId,
    [Required][Range(1,100)] decimal Price,
    [Required] DateOnly ReleaseDate    
);
