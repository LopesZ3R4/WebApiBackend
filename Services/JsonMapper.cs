// File Path: ./Services/JsonMapper.cs
using System.Text.Json;

public class JsonMapper
{
    // File Path: ./Services/JsonMapper.cs
    public AlertData MapJsonToAlertData(JsonElement jsonElement)
    {
        var alertData = new AlertData();
        alertData.Values = new List<Alert>();

        var valuesElement = jsonElement.GetProperty("values");
        foreach (var valueElement in valuesElement.EnumerateArray())
        {
            var alert = MapJsonToAlert(valueElement); // Assuming you have a method to map a single alert
            alertData.Values.Add(alert);
        }

        return alertData;
    }
    // File Path: ./Services/JsonMapper.cs
    public Alert MapJsonToAlert(JsonElement valueElement)
    {
        var alert = new Alert();

        alert.Type = valueElement.GetProperty("@type").GetString();
        alert.Occurrences = valueElement.GetProperty("occurrences").GetString();
        alert.EngineHoursType = valueElement.GetProperty("engineHours").GetProperty("@type").GetString();
        alert.ValueAsDouble = valueElement.GetProperty("engineHours").GetProperty("reading").GetProperty("valueAsDouble").GetDouble();
        alert.MachineLinearTime = valueElement.GetProperty("machineLinearTime").GetInt32();
        alert.Bus = valueElement.GetProperty("bus").GetString();
        alert.Id = valueElement.GetProperty("id").GetString();
        alert.Time = DateTime.Parse(valueElement.GetProperty("time").GetString());
        alert.LocationType = valueElement.GetProperty("location").GetProperty("@type").GetString();
        alert.Lat = valueElement.GetProperty("location").GetProperty("lat").GetDouble();
        alert.Lon = valueElement.GetProperty("location").GetProperty("lon").GetDouble();
        alert.Color = valueElement.GetProperty("color").GetString();
        alert.Severity = valueElement.GetProperty("severity").GetString();
        alert.AcknowledgementStatus = valueElement.GetProperty("acknowledgementStatus").GetString();
        alert.Ignored = valueElement.GetProperty("ignored").GetBoolean();
        alert.Invisible = valueElement.GetProperty("invisible").GetBoolean();

        var durationElement = valueElement.GetProperty("duration");
        alert.Duration = new Duration
        {
            AlertId = valueElement.GetProperty("id").GetString(),
            Type = durationElement.GetProperty("@type").GetString(),
            ValueAsInteger = durationElement.GetProperty("valueAsInteger").GetString(),
            Unit = durationElement.GetProperty("unit").GetString()
        };

        var definitionElement = valueElement.GetProperty("definition");
        alert.Definition = new Definition
        {
            AlertId = valueElement.GetProperty("id").GetString(),
            Type = definitionElement.GetProperty("@type").GetString(),
            SuspectParameterName = definitionElement.GetProperty("suspectParameterName").GetString(),
            FailureModeIndicator = definitionElement.GetProperty("failureModeIndicator").GetString(),
            Bus = definitionElement.GetProperty("bus").GetString(),
            SourceAddress = definitionElement.GetProperty("sourceAddress").GetString(),
            ThreeLetterAcronym = definitionElement.GetProperty("threeLetterAcronym").GetString(),
            Id = definitionElement.GetProperty("id").GetString(),
            Description = definitionElement.GetProperty("description").GetString()
        };
        
        if (valueElement.TryGetProperty("links", out var alertLinksElement))
        {
            alert.Links = new List<Link>();
            foreach (var linkElement in alertLinksElement.EnumerateArray())
            {
                var link = new Link
                {   
                    AlertId = valueElement.GetProperty("id").GetString(),
                    Type = linkElement.GetProperty("@type").GetString(),
                    Rel = linkElement.GetProperty("rel").GetString(),
                    Uri = linkElement.GetProperty("uri").GetString()
                };
                alert.Links.Add(link);
            }
        }


        if (valueElement.TryGetProperty("links", out _))
        {
            alert.Links = new List<Link>();
            foreach (var linkElement in alertLinksElement.EnumerateArray())
            {
                var link = new Link
                {   
                    AlertId = valueElement.GetProperty("id").GetString(),
                    Type = linkElement.GetProperty("@type").GetString(),
                    Rel = linkElement.GetProperty("rel").GetString(),
                    Uri = linkElement.GetProperty("uri").GetString()
                };
                alert.Links.Add(link);
            }
        }

        return alert;
    }
}