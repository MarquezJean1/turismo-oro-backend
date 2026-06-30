using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turismo_oro.Domain.Entities;

[Table(nameof(ComentarioTurismo), Schema = "TUR")]
public class ComentarioTurismo : Audit
{
    public Guid Id { get; set; }

    public Guid TurismoLugarId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Autor { get; set; } = string.Empty;

    [Required]
    public int Calificacion { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Texto { get; set; } = string.Empty;

    [ForeignKey(nameof(TurismoLugarId))]
    public TurismoLugar TurismoLugar { get; set; } = null!;
}
