// File Path: ./Models/Alert.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Alert
{
    [Key]
    public int AlertId { get; set; }
    public int Total { get; set; }
    public string Color { get; set; }
    public string Severity { get; set; }
    public string AcknowledgementStatus { get; set; }
    public bool Ignored { get; set; }
    public bool Invisible { get; set; }
    public DateTime Time { get; set; }

    // Navigation properties
    public ICollection<Link> Links { get; set; }
    public ICollection<Value> Values { get; set; }
}