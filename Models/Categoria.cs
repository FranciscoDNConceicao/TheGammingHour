using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

public partial class Categoria
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [InverseProperty("Categoria")]
    public virtual ICollection<CategoriaFavoritum> CategoriaFavorita { get; } = new List<CategoriaFavoritum>();

    [InverseProperty("Categoria")]
    public virtual ICollection<CategoriaJogo> CategoriaJogos { get; } = new List<CategoriaJogo>();
}
