using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Machines")]
public class Machine
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("ClientId")]
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public string? VisualizationCategory { get; set; }
    public string? MachineCategories { get; set; }
    public string? Category { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? DetailMachineCode { get; set; }
    public string? Type { get; set; }
    public int? ProductKey { get; set; }
    public string? EngineSerialNumber { get; set; }
    public string? TelematicsState { get; set; }
    public string? Capabilities { get; set; }
    public string? Terminals { get; set; }
    public string? Displays { get; set; }
    public string? Guid { get; set; }
    public int? ModelYear { get; set; }
    public string? Vin { get; set; }
    public int? ExternalId { get; set; }
    public string? Name { get; set; }
    public string? Display { get; set; }
}