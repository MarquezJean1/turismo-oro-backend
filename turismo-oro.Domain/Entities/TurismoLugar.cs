namespace turismo_oro.Domain.Entities;

public class TurismoLugar
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string EtiquetaPrecio { get; set; } = "Gratis";
    public string Descripcion { get; set; } = string.Empty;
    public string Destacados { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }

    public ICollection<TurismoArchivo> TurismoArchivos { get; set; } = [];
    public ICollection<ComentarioTurismo> Comentarios { get; set; } = [];
}
