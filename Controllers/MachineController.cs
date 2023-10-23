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
public class MachineController : ControllerBase
{
    private readonly MachineRepository _MachineRepository;
    private readonly AlertRepository _alertRepository;

    public MachineController(MachineRepository MachineRepository, AlertRepository alertRepository)
    {
        _MachineRepository = MachineRepository;
        _alertRepository = alertRepository;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Post([FromBody] JsonElement jsonElement)
    {
        var jsonMapper = new JsonMapper();
        var MachineData = jsonMapper.MapJsonToMachineData(jsonElement);

        foreach (var Machine in MachineData.Values)
        {
            var existingMachine = _MachineRepository.Exists(Machine.Id);
            Console.WriteLine($"Buscando pela existencia da Maquina: {Machine.Id}, resultado: {existingMachine}");

            if (!existingMachine)
            {
                await _MachineRepository.AddMachineAsync(Machine);
            }
        }

        return Ok();
    }

    [HttpGet("GetMachines")]
    public async Task<IActionResult> GetMachines(int pageNumber = 1, int pageSize = 10)
    {
        var (Machines, hasMore) = await _MachineRepository.GetAllMachinesAsync(pageNumber, pageSize);

        var response = new
        {
            count = Machines.Count,
            hasMore,
            Machines
        };

        var json = JsonSerializer.Serialize(response);

        return Ok(json);
    }
    [HttpGet("GetMachines/Types")]
    public async Task<IActionResult> GetMachinesTypes()
    {
        var MachinesTypes = await _MachineRepository.GetDistinctMachineTypes();
        var json = JsonSerializer.Serialize(MachinesTypes);
        return Ok(json);
    }
    public async Task<IActionResult> GetMachineAlerts()
    {
        var machines = await _MachineRepository.GetAllMachinesAsync();
        var httpClient = new HttpClient();
        var machineAlerts = new Dictionary<int, List<Alert>>();
        var jsonMapper = new JsonMapper();

        foreach (var machine in machines)
        {
            var response = await httpClient.GetAsync($"https://sandboxapi.deere.com/platform/machines/{machine.Id}/alerts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonElement = JsonDocument.Parse(content).RootElement;
                var alertData = jsonMapper.MapJsonToAlertData(jsonElement);

                foreach (var alert in alertData.Values)
                {
                    var existingAlert = _alertRepository.Exists(alert.Id);
                    Console.WriteLine($"Buscando pela existencia do alerta: {alert.Id}, resultado: {existingAlert}");

                    if (!existingAlert)
                    {
                        await _alertRepository.AddAlertAsync(alert);
                    }
                }
            }
        }

        return Ok(machineAlerts);
    }
}