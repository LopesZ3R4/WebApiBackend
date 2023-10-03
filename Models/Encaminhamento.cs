// File Path: ./Models/Encaminhamento.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Encaminhamento")]
public class Encaminhamento
{
    [Key]
    public int IdEncaminhamento { get; set; }
    public int AlertId { get; set; }
    [ForeignKey("AlertId")]
    public Alert Alert { get; set; }    
    public string? IdUsuario { get; set; }
    public string Motivo { get; set; }
    public int IdEmpresa { get; set; }
    public bool? EncaminhamentoAtivo { get; set; }
    public DateTime? DataInclusao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string? UsuarioInc { get; set; }
    public string? UsuarioAlt { get; set; }
    public int? OrigemRetorno { get; set; }
}