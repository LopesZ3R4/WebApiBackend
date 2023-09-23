// File Path: ./Controllers/AlertController.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AlertController : ControllerBase
{
    private readonly AlertRepository _alertRepository;

    public AlertController(AlertRepository alertRepository)
    {
        _alertRepository = alertRepository;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Post([FromBody] JsonElement jsonElement)
    {
        var jsonMapper = new JsonMapper();
        var alertData = jsonMapper.MapJsonToAlertData(jsonElement);

        foreach (var alert in alertData.Values)
        {
            var existingAlert = _alertRepository.Exists(alert.Id);

            if (existingAlert)
            {
                await _alertRepository.AddAlertAsync(alert);
            }
        }

        return Ok();
    }
    
    [HttpGet("GetAlerts")]
    public async Task<IActionResult> GetAlerts(string? type = null, string? color = null, string? severity = null, DateTime? date = null)
    {
        var alerts = await _alertRepository.GetAllAlertsAsync(type, color, severity, date);

        var response = new 
        {
            count = alerts.Count,
            alerts
        };

        var json = JsonSerializer.Serialize(response);

        return Ok(json);
    }
}