// c:\WebApiBackend\Models\Client.cs
using System.ComponentModel.DataAnnotations.Schema;

[Table("Clients")]
public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
}