using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UTAD.LAB._2022.TheGammingHour.Models;

[Table("Pagamento")]
public partial class Pagamento
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("UtilizadorID")]
    public long UtilizadorId { get; set; }

    public int? Telemovel { get; set; }

    [Column("NIF")]
    [StringLength(9)]
    [Unicode(false)]
    public string Nif { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string Morada { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string CodigoPostal { get; set; } = null!;

    public int? Entidade { get; set; }

    public int? Referencia { get; set; }

    public bool? PayPal { get; set; }

    public bool? Guarda { get; set; }

    [InverseProperty("Pagamento")]
    public virtual ICollection<CompraJogo> CompraJogos { get; } = new List<CompraJogo>();

    [ForeignKey("UtilizadorId")]
    [InverseProperty("Pagamentos")]
    public virtual Utilizador Utilizador { get; set; } = null!;
}
