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

    public MachineController(MachineRepository MachineRepository)
    {
        _MachineRepository = MachineRepository;
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
}