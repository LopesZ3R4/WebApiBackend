// File Path: ./Models/Location.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Location
{
    [Key]
    public int LocationId { get; set; }
    public string Type { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }

    // Foreign key
    [ForeignKey("Value")]
    public int ValueId { get; set; }

    // Navigation property
    public Value Value { get; set; }
}