// File Path: ./Services/JsonMapper.cs
using System.Text.Json;
using Utils;

public class JsonMapper
{
    public AlertData MapJsonToAlertData(JsonElement jsonElement)
    {
        var alertData = new AlertData
        {
            Values = new List<Alert>()
        };

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
        var definitionElement = valueElement.GetProperty("definition");
        var alert = new Alert
        {
            Type = valueElement.GetProperty("@type").GetString(),
            Occurrences = valueElement.GetProperty("occurrences").GetString(),
            EngineHoursType = valueElement.GetProperty("engineHours").GetProperty("@type").GetString(),
            EngineHoursUnit = valueElement.GetProperty("engineHours").GetProperty("reading").GetProperty("unit").GetString(),
            EngineHoursValue = valueElement.GetProperty("engineHours").GetProperty("reading").GetProperty("valueAsDouble").GetDouble(),
            MachineLinearTime = valueElement.GetProperty("machineLinearTime").GetInt32(),
            Bus = int.Parse(valueElement.GetProperty("bus").GetString()),
            Id = int.Parse(valueElement.GetProperty("id").GetString()),
            Time = DateTime.Parse(valueElement.GetProperty("time").GetString()),
            LocationType = valueElement.GetProperty("location").GetProperty("@type").GetString(),
            Lat = valueElement.GetProperty("location").GetProperty("lat").GetDouble(),
            Lon = valueElement.GetProperty("location").GetProperty("lon").GetDouble(),
            Color = valueElement.GetProperty("color").GetString(),
            Severity = valueElement.GetProperty("severity").GetString(),
            AcknowledgementStatus = valueElement.GetProperty("acknowledgementStatus").GetString(),
            Ignored = valueElement.GetProperty("ignored").GetBoolean(),
            Invisible = valueElement.GetProperty("invisible").GetBoolean(),
            DurationType = valueElement.GetProperty("duration").GetProperty("@type").GetString(),
            DurationValue = double.Parse(valueElement.GetProperty("duration").GetProperty("valueAsInteger").GetString()),
            DurationUnit = valueElement.GetProperty("duration").GetProperty("unit").GetString(),
            DefinitionType = definitionElement.GetProperty("@type").GetString(),
            DefinitionSuspectParameterName = definitionElement.GetProperty("suspectParameterName").GetString(),
            DefinitionFailureModeIndicator = definitionElement.GetProperty("failureModeIndicator").GetString(),
            DefinitionBus = int.Parse(definitionElement.GetProperty("bus").GetString()),
            DefinitionSourceAddress = definitionElement.GetProperty("sourceAddress").GetString(),
            DefinitionThreeLetterAcronym = definitionElement.GetProperty("threeLetterAcronym").GetString(),
            DefinitionId = int.Parse(definitionElement.GetProperty("id").GetString()),
            DefinitionDescription = definitionElement.GetProperty("description").GetString()
        };
        if (alert.EngineHoursUnit != "Minutes"){
            alert.EngineHoursValue = TimeConversion.ConvertTime(alert.EngineHoursValue, alert.EngineHoursUnit, "Minutes");
            alert.EngineHoursUnit = "Minutes";
        }
        if(alert.DurationUnit != "Minutes"){
            alert.DurationValue = TimeConversion.ConvertTime(alert.DurationValue, alert.DurationUnit, "Minutes");
            alert.DurationUnit = "Minutes";
        }
        
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
        return alert;
    }
}