// File Path: ./Models/AlertLink.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AlertLink
{
    [Key]
    public int AlertLinkId { get; set; }
    public string Type { get; set; }
    public string Rel { get; set; }
    public string Uri { get; set; }

    // Foreign key
    [ForeignKey("Value")]
    public int ValueId { get; set; }

    // Navigation property
    public Value Value { get; set; }
}