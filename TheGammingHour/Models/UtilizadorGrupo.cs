using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Utilizador_Grupo")]
public partial class UtilizadorGrupo
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("UtilizadorID")]
    public long UtilizadorId { get; set; }

    [Column("GrupoID")]
    public long GrupoId { get; set; }

    [ForeignKey("GrupoId")]
    [InverseProperty("UtilizadorGrupos")]
    public virtual Grupo Grupo { get; set; } = null!;

    [ForeignKey("UtilizadorId")]
    [InverseProperty("UtilizadorGrupos")]
    public virtual Utilizador Utilizador { get; set; } = null!;
}
