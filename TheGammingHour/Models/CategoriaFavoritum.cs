using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

public partial class CategoriaFavoritum
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("UtilizadorID")]
    public long UtilizadorId { get; set; }

    [Column("CategoriaID")]
    public long CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("CategoriaFavorita")]
    public virtual Categoria Categoria { get; set; } = null!;

    [ForeignKey("UtilizadorId")]
    [InverseProperty("CategoriaFavorita")]
    public virtual Utilizador Utilizador { get; set; } = null!;
}
