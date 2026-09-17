using System;
using System.Collections.Generic;

namespace SistemaMercadoMVC.Models;

public partial class Produto
{
    public int ProdutoId { get; set; }

    public string NomeProduto { get; set; } = null!;

    public decimal? Preco { get; set; }

    public string Descricao { get; set; } = null!;

    public int? UsuarioId { get; set; }

    public byte[]? Imagem { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
