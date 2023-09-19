// File Path: ./Models/DefinitionLink.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DefinitionLink
{
    [Key]
    public int DefinitionLinkId { get; set; }
    public string Type { get; set; }
    public string Rel { get; set; }
    public string Uri { get; set; }

    // Foreign key
    [ForeignKey("Definition")]
    public int DefinitionId { get; set; }

    // Navigation property
    public Definition Definition { get; set; }
}