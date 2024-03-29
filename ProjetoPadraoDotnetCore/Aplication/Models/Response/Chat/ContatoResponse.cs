﻿using Infraestrutura.Enum;

namespace Aplication.Models.Response.Chat;

public class ContatoResponse 
{
    public string? Nome { get; set; }
    public string? Foto { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Sobre { get; set; }
    public string? DataNascimento { get; set; }
    public StatusContato StatusContato { get; set; }
    public int IdUsuarioContato { get; set; }
    public int? IdContatoChat { get; set; }
}