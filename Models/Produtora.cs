using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Produtora")]
public partial class Produtora
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [InverseProperty("Produtora")]
    public virtual ICollection<ProdutoraJogo> ProdutoraJogos { get; } = new List<ProdutoraJogo>();
}
