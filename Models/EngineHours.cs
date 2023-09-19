// File Path: ./Models/EngineHours.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EngineHours
{
    [Key]
    public int EngineHoursId { get; set; }
    public string Type { get; set; }
    public double ValueAsDouble { get; set; }
    public string Unit { get; set; }

    // Foreign key
    [ForeignKey("Value")]
    public int ValueId { get; set; }

    // Navigation property
    public Value Value { get; set; }
}