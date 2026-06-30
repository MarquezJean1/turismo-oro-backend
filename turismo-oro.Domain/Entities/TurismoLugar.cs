using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace turismo_oro.Domain.Entities;

[Table(nameof(TurismoLugar), Schema = "TUR")]
public class TurismoLugar : Audit
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Categoria { get; set; } = string.Empty;

    [Required]
    [Precision(10, 7)]
    public decimal Latitud { get; set; }

    [Required]
    [Precision(10, 7)]
    public decimal Longitud { get; set; }

    [Required]
    [MaxLength(500)]
    public string Direccion { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string EtiquetaPrecio { get; set; } = "Gratis";

    [Required]
    [MaxLength(4000)]
    public string Descripcion { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Destacados { get; set; } = string.Empty;

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public ICollection<TurismoArchivo> TurismoArchivos { get; set; } = [];

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public ICollection<ComentarioTurismo> Comentarios { get; set; } = [];
}
