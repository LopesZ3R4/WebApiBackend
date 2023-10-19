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
    private readonly AlertRepository _alertRepository;
    private readonly AuthenticationService _authService;

    public EncaminhamentoController(EncaminhamentoRepository repository, AuthenticationService authService, AlertRepository alertRepository)
    {
        _repository = repository;
        _authService = authService;
        _alertRepository = alertRepository;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] EncaminhamentoInputModel inputModel)
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

        bool alreadyExists = _repository.ExistsAlert(encaminhamento.AlertId);

        if (alreadyExists)
        {
            _repository.Insert(encaminhamento, username);
            await _alertRepository.UpdateAlertSentStatus(encaminhamento.AlertId);
        }
        return Ok();
    }
}