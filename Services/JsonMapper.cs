// File Path: ./Services/JsonMapper.cs
using System.Text.Json;

public class JsonMapper
{
    public AlertData MapJsonToAlertData(JsonElement jsonElement)
    {
        var alertData = new AlertData();
        alertData.Values = new List<Alert>();

        var valuesElement = jsonElement.GetProperty("values");
        foreach (var valueElement in valuesElement.EnumerateArray())
        {
            var alert = MapJsonToAlert(valueElement);
            alertData.Values.Add(alert);
        }

        return alertData;
    }
    public Alert MapJsonToAlert(JsonElement valueElement)
    {
        var alert = new Alert();

        alert.Type = valueElement.GetProperty("@type").GetString();
        alert.Occurrences = valueElement.GetProperty("occurrences").GetString();
        alert.EngineHoursType = valueElement.GetProperty("engineHours").GetProperty("@type").GetString();
        alert.EngineHoursValue = valueElement.GetProperty("engineHours").GetProperty("reading").GetProperty("valueAsDouble").GetDouble();
        alert.MachineLinearTime = valueElement.GetProperty("machineLinearTime").GetInt32();
        alert.Bus = int.Parse(valueElement.GetProperty("bus").GetString());
        alert.Id = int.Parse(valueElement.GetProperty("id").GetString());
        alert.Time = DateTime.Parse(valueElement.GetProperty("time").GetString());
        alert.LocationType = valueElement.GetProperty("location").GetProperty("@type").GetString();
        alert.Lat = valueElement.GetProperty("location").GetProperty("lat").GetDouble();
        alert.Lon = valueElement.GetProperty("location").GetProperty("lon").GetDouble();
        alert.Color = valueElement.GetProperty("color").GetString();
        alert.Severity = valueElement.GetProperty("severity").GetString();
        alert.AcknowledgementStatus = valueElement.GetProperty("acknowledgementStatus").GetString();
        alert.Ignored = valueElement.GetProperty("ignored").GetBoolean();
        alert.Invisible = valueElement.GetProperty("invisible").GetBoolean();
        alert.DurationType = valueElement.GetProperty("duration").GetProperty("@type").GetString();
        alert.DurationValue = double.Parse(valueElement.GetProperty("duration").GetProperty("valueAsInteger").GetString());
        alert.DurationUnit = valueElement.GetProperty("duration").GetProperty("unit").GetString();
        
        var linksArray = valueElement.GetProperty("definition").GetProperty("links").EnumerateArray();
        if (linksArray.Any())
        {
            var firstLinkElement = linksArray.First();
            alert.DefinitionLinkType = firstLinkElement.GetProperty("@type").GetString();
            alert.DefinitionLinkRel = firstLinkElement.GetProperty("rel").GetString();
            alert.DefinitionLinkUri = firstLinkElement.GetProperty("uri").GetString();
        }

        var alertLinksArray = valueElement.GetProperty("links").EnumerateArray();
        if (alertLinksArray.Any())
        {
            var firstAlertLinkElement = alertLinksArray.First();
            alert.LinkType = firstAlertLinkElement.GetProperty("@type").GetString();
            alert.LinkRel = firstAlertLinkElement.GetProperty("rel").GetString();
            alert.LinkUri = firstAlertLinkElement.GetProperty("uri").GetString();
        }

        var definitionElement = valueElement.GetProperty("definition");
        alert.Definition = new Definition
        {
            AlertId = int.Parse(valueElement.GetProperty("id").GetString()),
            Type = definitionElement.GetProperty("@type").GetString(),
            SuspectParameterName = definitionElement.GetProperty("suspectParameterName").GetString(),
            FailureModeIndicator = definitionElement.GetProperty("failureModeIndicator").GetString(),
            Bus = int.Parse(definitionElement.GetProperty("bus").GetString()),
            SourceAddress = definitionElement.GetProperty("sourceAddress").GetString(),
            ThreeLetterAcronym = definitionElement.GetProperty("threeLetterAcronym").GetString(),
            Id = int.Parse(definitionElement.GetProperty("id").GetString()),
            Description = definitionElement.GetProperty("description").GetString()
        };

        return alert;
    }
}