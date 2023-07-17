using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Utilizador")]
public partial class Utilizador
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    public int Tipo { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Nome { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(14)]
    public string? Telefone { get; set; }

    public bool Verificado { get; set; }

    public bool Newsletter { get; set; }

    [Column("GrupoID")]
    public long GrupoId { get; set; }

    [InverseProperty("Utilizador")]
    public virtual ICollection<CategoriaFavoritum> CategoriaFavorita { get; } = new List<CategoriaFavoritum>();

    [InverseProperty("Utilizador")]
    public virtual ICollection<CompraJogo> CompraJogos { get; } = new List<CompraJogo>();

    [ForeignKey("GrupoId")]
    [InverseProperty("Utilizadors")]
    public virtual Grupo Grupo { get; set; } = null!;

    [InverseProperty("Utilizador")]
    public virtual ICollection<Pagamento> Pagamentos { get; } = new List<Pagamento>();

    [InverseProperty("Utilizador")]
    public virtual ICollection<UtilizadorGrupo> UtilizadorGrupos { get; } = new List<UtilizadorGrupo>();
}
