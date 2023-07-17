using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Grupo_Menu")]
public partial class GrupoMenu
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("GrupoID")]
    public long GrupoId { get; set; }

    [Column("MenuID")]
    public long MenuId { get; set; }

    [ForeignKey("GrupoId")]
    [InverseProperty("GrupoMenus")]
    public virtual Grupo Grupo { get; set; } = null!;

    [ForeignKey("MenuId")]
    [InverseProperty("GrupoMenus")]
    public virtual Menu Menu { get; set; } = null!;
}
