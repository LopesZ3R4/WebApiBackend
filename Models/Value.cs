// File Path: ./Models/Value.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Value
{
    [Key]
    public int ValueId { get; set; }
    public string Type { get; set; }
    public int Occurrences { get; set; }
    public string MachineLinearTime { get; set; }
    public string Bus { get; set; }
    public string Id { get; set; }

    // Foreign key
    [ForeignKey("Alert")]
    public int AlertId { get; set; }

    // Navigation properties
    public Alert Alert { get; set; }
    public Duration Duration { get; set; }
    public EngineHours EngineHours { get; set; }
    public Location Location { get; set; }
    public Definition Definition { get; set; }
    public ICollection<AlertLink> AlertLinks { get; set; }
}