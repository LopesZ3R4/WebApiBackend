// File Path: ./Models/Alert.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

[Table("Alerts")]
public class Alert
{   
    [Key]
    public int Id { get; set; }
    public int MachineId { get; set; }
    [ForeignKey("MachineId")]
    public Machine? Machine { get; set; }
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
    public string? DefinitionType { get; set; }
    public string? DefinitionSuspectParameterName { get; set; }
    public string? DefinitionFailureModeIndicator { get; set; }
    public int DefinitionBus { get; set; }
    public string? DefinitionSourceAddress { get; set; }
    public string? DefinitionThreeLetterAcronym { get; set; }
    public int DefinitionId { get; set; }
    public string? DefinitionDescription { get; set; }
    public bool Sent { get; set; }
}