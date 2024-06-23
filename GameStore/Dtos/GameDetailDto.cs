﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record class GameDetailDto
( 
    [Required]int Id,
    [Required][StringLength(100)] string Name,
    [Required]int GenreId,
    [Required][Range(1,100)] decimal Price,
    [Required] DateOnly ReleaseDate    
    );
