using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Grupo")]
public partial class Grupo
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [InverseProperty("Grupo")]
    public virtual ICollection<GrupoMenu> GrupoMenus { get; } = new List<GrupoMenu>();

    [InverseProperty("Grupo")]
    public virtual ICollection<UtilizadorGrupo> UtilizadorGrupos { get; } = new List<UtilizadorGrupo>();

    [InverseProperty("Grupo")]
    public virtual ICollection<Utilizador> Utilizadors { get; } = new List<Utilizador>();
}
