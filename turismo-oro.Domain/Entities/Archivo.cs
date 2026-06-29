using turismo_oro.Domain.Enums;

namespace turismo_oro.Domain.Entities;

public class Archivo
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public long TamanioBytes { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public TipoArchivo TipoArchivo { get; set; }
    public DateTime FechaCreacion { get; set; }

    public ICollection<TurismoArchivo> TurismoArchivos { get; set; } = [];
}
