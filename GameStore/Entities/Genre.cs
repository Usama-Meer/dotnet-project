namespace GameStore.Entities;

public class Genre
{
    public int Id { get; set; }

    //required is used to prevent from adding null
    //another method to add prevent from adding null;
    // public string Name {get;set;}=string.Empty;
    public required string Name { get; set; }


}
