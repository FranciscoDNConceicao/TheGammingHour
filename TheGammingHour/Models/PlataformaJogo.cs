using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Plataforma_Jogo")]
public partial class PlataformaJogo
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("PlataformaID")]
    public long PlataformaId { get; set; }

    [Column("JogoID")]
    public long JogoId { get; set; }

    [ForeignKey("JogoId")]
    [InverseProperty("PlataformaJogos")]
    public virtual Jogo Jogo { get; set; } = null!;

    [ForeignKey("PlataformaId")]
    [InverseProperty("PlataformaJogos")]
    public virtual Plataforma Plataforma { get; set; } = null!;
}
