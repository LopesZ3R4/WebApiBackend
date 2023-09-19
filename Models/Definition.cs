// File Path: ./Models/Definition.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Definition
{
    [Key]
    public int DefinitionId { get; set; }
    public string Type { get; set; }
    public string SuspectParameterName { get; set; }
    public string FailureModeIndicator { get; set; }
    public string Bus { get; set; }
    public string SourceAddress { get; set; }
    public string ThreeLetterAcronym { get; set; }
    public string Id { get; set; }
    public string Description { get; set; }

    // Foreign key
    [ForeignKey("Value")]
    public int ValueId { get; set; }

    // Navigation properties
    public Value Value { get; set; }
    public ICollection<DefinitionLink> DefinitionLinks { get; set; }
}