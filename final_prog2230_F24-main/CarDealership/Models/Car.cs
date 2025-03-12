// Models/Car.cs
using System.ComponentModel.DataAnnotations;

public class Car
{
    [Key]
    public int CarId { get; set; }

    [Required]
    public string? Make { get; set; } 

    [Required]
    public string? Model { get; set; } 

    public int? Year { get; set; } 

    [DataType(DataType.Currency)]
    public decimal? Price { get; set; } 
    //public int Id { get; internal set; }

}
