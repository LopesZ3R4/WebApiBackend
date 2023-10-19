// File Path: ./Repositories/EncaminhamentoRepository.cs
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class EncaminhamentoRepository
{
    private readonly ApplicationDbContext _context;

    public EncaminhamentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Insert(Encaminhamento encaminhamento, string loggedUser)
    {
        encaminhamento.IdUsuario = loggedUser;
        encaminhamento.DataInclusao = DateTime.Now;
        encaminhamento.DataAlteracao = DateTime.Now;
        encaminhamento.UsuarioInc = loggedUser;
        encaminhamento.UsuarioAlt = loggedUser;

        _context.Encaminhamento.Add(encaminhamento);
        _context.SaveChanges();
    }

    public void Update(Encaminhamento encaminhamento, string loggedUser)
    {
        var existingEncaminhamento = _context.Encaminhamento
            .Where(e => e.IdEncaminhamento == encaminhamento.IdEncaminhamento)
            .FirstOrDefault();

        if (existingEncaminhamento != null)
        {
            existingEncaminhamento.Alert = encaminhamento.Alert;
            existingEncaminhamento.IdUsuario = encaminhamento.IdUsuario;
            existingEncaminhamento.Motivo = encaminhamento.Motivo;
            existingEncaminhamento.IdEmpresa = encaminhamento.IdEmpresa;
            existingEncaminhamento.EncaminhamentoAtivo = encaminhamento.EncaminhamentoAtivo;
            existingEncaminhamento.DataAlteracao = DateTime.Now;
            existingEncaminhamento.UsuarioAlt = loggedUser;
            existingEncaminhamento.OrigemRetorno = encaminhamento.OrigemRetorno;

            _context.SaveChanges();
        }
    }
    public bool ExistsAlert(int AlertID)
        {
            return _context.Encaminhamento.Any(u => u.AlertId == AlertID);
        }
}