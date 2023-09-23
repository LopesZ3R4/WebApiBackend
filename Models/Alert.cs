// File Path: ./Models/Alert.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

[Table("Alerts")]
public class Alert
{   
    [Key]
    public int Id { get; set; }
    [JsonProperty(PropertyName = "@type")]
    public string? Type { get; set; }
    [JsonProperty(PropertyName = "@type")]
    public string? DurationType { get; set; }
    public double DurationValue { get; set; }
    public string? DurationUnit { get; set; }
    public string? Occurrences { get; set; }
    public string? EngineHoursType { get; set; }
    public double EngineHoursValue { get; set; }
    public string? EngineHoursUnit { get; set; }
    public int MachineLinearTime { get; set; }
    public int? Bus { get; set; }
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
    [JsonProperty(PropertyName = "@type")]
    public string? LinkType { get; set; }
    public string? LinkRel { get; set; }
    public string? LinkUri { get; set; }
    [JsonProperty(PropertyName = "@type")]
    public string? DefinitionLinkType { get; set; }
    public string? DefinitionLinkRel { get; set; }
    public string? DefinitionLinkUri { get; set; }
}
[Table("Definitions")]
public class Definition
{
    [Key]
    public int AlertId { get; set; }
    [JsonProperty(PropertyName = "@type")]
    public int? Id { get; set;}
    public string? Type { get; set; }
    public string? SuspectParameterName { get; set; }
    public string? FailureModeIndicator { get; set; }
    public int? Bus { get; set; }
    public string? SourceAddress { get; set; }
    public string? ThreeLetterAcronym { get; set; }
    public string? Description { get; set; }
    [ForeignKey("AlertId")]
    public Alert Alert { get; set; }
}