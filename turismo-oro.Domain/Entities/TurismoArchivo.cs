using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace turismo_oro.Domain.Entities;

[Table(nameof(TurismoArchivo), Schema = "FILE")]
[PrimaryKey(nameof(TurismoLugarId), nameof(ArchivoId))]
public class TurismoArchivo : Audit
{
    public Guid TurismoLugarId { get; set; }

    public Guid ArchivoId { get; set; }

    [Required]
    public int Orden { get; set; }

    [ForeignKey(nameof(TurismoLugarId))]
    public TurismoLugar TurismoLugar { get; set; } = null!;

    [ForeignKey(nameof(ArchivoId))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Archivo Archivo { get; set; } = null!;
}
