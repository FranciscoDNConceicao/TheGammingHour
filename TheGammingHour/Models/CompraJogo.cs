using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("CompraJogo")]
public partial class CompraJogo
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("UtilizadorID")]
    public long UtilizadorId { get; set; }

    [Column("JogoID")]
    public long JogoId { get; set; }

    [Column("PagamentoID")]
    public long PagamentoId { get; set; }

    [Column(TypeName = "date")]
    public DateTime Data { get; set; }

    public double Preco { get; set; }

    [ForeignKey("JogoId")]
    [InverseProperty("CompraJogos")]
    public virtual Jogo Jogo { get; set; } = null!;

    [ForeignKey("PagamentoId")]
    [InverseProperty("CompraJogos")]
    public virtual Pagamento Pagamento { get; set; } = null!;

    [ForeignKey("UtilizadorId")]
    [InverseProperty("CompraJogos")]
    public virtual Utilizador Utilizador { get; set; } = null!;
}
