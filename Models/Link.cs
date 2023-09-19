// File Path: ./Models/Link.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Link
{
    [Key]
    public int LinkId { get; set; }
    public string Rel { get; set; }
    public string Uri { get; set; }

    // Foreign key
    [ForeignKey("Alert")]
    public int AlertId { get; set; }

    // Navigation property
    public Alert Alert { get; set; }
}