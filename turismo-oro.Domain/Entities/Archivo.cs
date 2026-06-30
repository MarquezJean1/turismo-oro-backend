using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using turismo_oro.Domain.Enums;

namespace turismo_oro.Domain.Entities;

[Table(nameof(Archivo), Schema = "FILE")]
public class Archivo : Audit
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;

    public long TamanioBytes { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Extension { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public TipoArchivo TipoArchivo { get; set; }

    public ICollection<TurismoArchivo> TurismoArchivos { get; set; } = [];
}
