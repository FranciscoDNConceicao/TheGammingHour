using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Jogo")]
public partial class Jogo
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    public DateTime Data { get; set; }

    public double Preco { get; set; }

    public int Pegi { get; set; }

    [Column(TypeName = "text")]
    public string Descricao { get; set; } = null!;

    [Column(TypeName = "text")]
    public string Imagem { get; set; } = null!;

    public bool Desconto { get; set; }

    [Column("Percentagem_Desconto")]
    public double? PercentagemDesconto { get; set; }

    public double Avaliacao { get; set; }

    [InverseProperty("Jogo")]
    public virtual ICollection<CategoriaJogo> CategoriaJogos { get; } = new List<CategoriaJogo>();

    [InverseProperty("Jogo")]
    public virtual ICollection<CompraJogo> CompraJogos { get; } = new List<CompraJogo>();

    [InverseProperty("Jogo")]
    public virtual ICollection<PlataformaJogo> PlataformaJogos { get; } = new List<PlataformaJogo>();

    [InverseProperty("Jogo")]
    public virtual ICollection<ProdutoraJogo> ProdutoraJogos { get; } = new List<ProdutoraJogo>();

    
}
