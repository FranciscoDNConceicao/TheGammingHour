using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Menu")]
public partial class Menu
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Caption { get; set; } = null!;

    public bool Create { get; set; }

    public bool Edit { get; set; }

    public bool Update { get; set; }

    public bool View { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Tooltip { get; set; } = null!;

    public long Key { get; set; }

    [InverseProperty("Menu")]
    public virtual ICollection<GrupoMenu> GrupoMenus { get; } = new List<GrupoMenu>();
}
