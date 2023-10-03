// File Path: ./Controllers/EncaminhamentoController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

[Route("[controller]")]
[ApiController]
[Authorize]
public class EncaminhamentoController : ControllerBase
{
    private readonly EncaminhamentoRepository _repository;
    private readonly AuthenticationService _authService;

    public EncaminhamentoController(EncaminhamentoRepository repository, AuthenticationService authService)
    {
        _repository = repository;
        _authService = authService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] EncaminhamentoInputModel inputModel)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var username = _authService.GetUsernameFromToken(token);

        Console.WriteLine($"Usuario: {username}");
        
        var encaminhamento = new Encaminhamento
        {
            AlertId = inputModel.AlertId,
            Motivo = inputModel.Motivo,
            IdEmpresa = inputModel.IdEmpresa,
            EncaminhamentoAtivo = true,
            OrigemRetorno = 0,
        };

        _repository.Insert(encaminhamento,username);
        return Ok();
    }
}