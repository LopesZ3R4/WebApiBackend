// File Path: ./Models/Duration.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Duration
{
    [Key]
    public int DurationId { get; set; }
    public string Type { get; set; }
    public int ValueAsInteger { get; set; }
    public string Unit { get; set; }

    // Foreign key
    [ForeignKey("Value")]
    public int ValueId { get; set; }

    // Navigation property
    public Value Value { get; set; }
}