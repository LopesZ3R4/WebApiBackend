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
public class ClientsController : ControllerBase
{
    private readonly ClientRepository _ClienteRepository;

    public ClientsController(ClientRepository ClienteRepository)
    {
        _ClienteRepository = ClienteRepository;
    }

    [HttpGet("GetClients")]
    public IActionResult GetClients()
    {
        var Clients = _ClienteRepository.GetAll();

        var json = JsonSerializer.Serialize(Clients);

        return Ok(json);
    }
}