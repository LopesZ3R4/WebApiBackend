// File Path: ./Models/Alert.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

[Table("Alerts")]
public class Alert
{   
    [Key]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "@type")]
    public string? Type { get; set; }
    public Duration? Duration { get; set; }
    public string? Occurrences { get; set; }
    public string? EngineHoursType { get; set; }
    public double ValueAsDouble { get; set; }
    public string? Unit { get; set; }
    public int MachineLinearTime { get; set; }
    public string? Bus { get; set; }
    public Definition? Definition { get; set; }
    public DateTime? Time { get; set; }
    public string? LocationType { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string? Color { get; set; }
    public string? Severity { get; set; }
    public string? AcknowledgementStatus { get; set; }
    public bool Ignored { get; set; }
    public bool Invisible { get; set; }
    public List<Link>? Links { get; set; }
}
[Table("Durations")]
public class Duration
{
    [Key]
    public string AlertId { get; set; }
    [JsonProperty(PropertyName = "@type")]
    public string? Type { get; set; }
    public string? ValueAsInteger { get; set; }
    public string? Unit { get; set; }
    [ForeignKey("AlertId")]
    public Alert Alert { get; set; }
}
[Table("Definitions")]
public class Definition
{
    [Key]
    public string AlertId { get; set; }
    [JsonProperty(PropertyName = "@type")]
    public string? Id { get; set;}
    public string? Type { get; set; }
    public string? SuspectParameterName { get; set; }
    public string? FailureModeIndicator { get; set; }
    public string? Bus { get; set; }
    public string? SourceAddress { get; set; }
    public string? ThreeLetterAcronym { get; set; }
    public string? Description { get; set; }
    //public List<Link> Links { get; set; }
    [ForeignKey("AlertId")]
    public Alert Alert { get; set; }
}
[Table("Links")]
public class Link
{
    [Key]
    public int Id { get; set; }
    public string? AlertId { get; set; }
    public string? DefinitionId { get; set; }
    [JsonProperty(PropertyName = "@type")]
    public string? Type { get; set; }
    public string? Rel { get; set; }
    public string? Uri { get; set; }
    [ForeignKey("AlertId")]
    public Alert? Alert { get; set; }
    [ForeignKey("DefinitionId")]
    public Definition? Definition { get; set; }
}