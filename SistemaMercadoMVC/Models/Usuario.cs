using System;
using System.Collections.Generic;

namespace SistemaMercadoMVC.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[]? Senha { get; set; }

    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();
}
