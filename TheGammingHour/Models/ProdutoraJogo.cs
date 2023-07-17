using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Produtora_Jogo")]
public partial class ProdutoraJogo
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("ProdutoraID")]
    public long ProdutoraId { get; set; }

    [Column("JogoID")]
    public long JogoId { get; set; }

    [ForeignKey("JogoId")]
    [InverseProperty("ProdutoraJogos")]
    public virtual Jogo Jogo { get; set; } = null!;

    [ForeignKey("ProdutoraId")]
    [InverseProperty("ProdutoraJogos")]
    public virtual Produtora Produtora { get; set; } = null!;
}
