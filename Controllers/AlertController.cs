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
        var alertData = jsonMapper.MapJsonToAlertData(jsonElement); // Assuming you have a method to map the entire JSON to AlertData

        foreach (var alert in alertData.Values)
        {
            await _alertRepository.AddAlertAsync(alert);
        }

        return Ok();
    }
}