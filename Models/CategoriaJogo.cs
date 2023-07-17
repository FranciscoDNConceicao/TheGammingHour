using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Categoria_Jogo")]
public partial class CategoriaJogo
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CategoriaID")]
    public long CategoriaId { get; set; }

    [Column("JogoID")]
    public long JogoId { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("CategoriaJogos")]
    public virtual Categoria Categoria { get; set; } = null!;

    [ForeignKey("JogoId")]
    [InverseProperty("CategoriaJogos")]
    public virtual Jogo Jogo { get; set; } = null!;
}
