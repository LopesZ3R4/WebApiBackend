// File Path: ./Controllers/AlertController.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AlertController : ControllerBase
{
    private readonly AlertRepository _alertRepository;
    private readonly LinkRepository _linkRepository;
    private readonly ValueRepository _valueRepository;
    private readonly DurationRepository _durationRepository;
    private readonly EngineHoursRepository _engineHoursRepository;
    private readonly LocationRepository _locationRepository;
    private readonly DefinitionRepository _definitionRepository;
    private readonly DefinitionLinkRepository _definitionLinkRepository;
    private readonly AlertLinkRepository _alertLinkRepository;

    public AlertController(AlertRepository alertRepository, LinkRepository linkRepository, ValueRepository valueRepository, DurationRepository durationRepository, EngineHoursRepository engineHoursRepository, LocationRepository locationRepository, DefinitionRepository definitionRepository, DefinitionLinkRepository definitionLinkRepository, AlertLinkRepository alertLinkRepository)
    {
        _alertRepository = alertRepository;
        _linkRepository = linkRepository;
        _valueRepository = valueRepository;
        _durationRepository = durationRepository;
        _engineHoursRepository = engineHoursRepository;
        _locationRepository = locationRepository;
        _definitionRepository = definitionRepository;
        _definitionLinkRepository = definitionLinkRepository;
        _alertLinkRepository = alertLinkRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Alert>> Get()
    {
        return await _alertRepository.GetAllAlertsAsync();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Post([FromBody] Alert alert)
    {
        // Save the alert
        await _alertRepository.AddAlertAsync(alert);

        // Save the links
        foreach (var link in alert.Links)
        {
            link.AlertId = alert.AlertId;
            await _linkRepository.AddLinkAsync(link);
        }

        // Save the values
        foreach (var value in alert.Values)
        {
            value.AlertId = alert.AlertId;
            await _valueRepository.AddValueAsync(value);

            // Save the duration
            if (value.Duration != null)
            {
                value.Duration.ValueId = value.ValueId;
                await _durationRepository.AddDurationAsync(value.Duration);
            }

            // Save the engine hours
            if (value.EngineHours != null)
            {
                value.EngineHours.ValueId = value.ValueId;
                await _engineHoursRepository.AddEngineHoursAsync(value.EngineHours);
            }

            // Save the location
            if (value.Location != null)
            {
                value.Location.ValueId = value.ValueId;
                await _locationRepository.AddLocationAsync(value.Location);
            }

            // Save the definition
            if (value.Definition != null)
            {
                value.Definition.ValueId = value.ValueId;
                await _definitionRepository.AddDefinitionAsync(value.Definition);

                // Save the definition links
                foreach (var definitionLink in value.Definition.DefinitionLinks)
                {
                    definitionLink.DefinitionId = value.Definition.DefinitionId;
                    await _definitionLinkRepository.AddDefinitionLinkAsync(definitionLink);
                }
            }

            // Save the alert links
            foreach (var alertLink in value.AlertLinks)
            {
                alertLink.ValueId = value.ValueId;
                await _alertLinkRepository.AddAlertLinkAsync(alertLink);
            }
        }
        return Ok();
    }
}