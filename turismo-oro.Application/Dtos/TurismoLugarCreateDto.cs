namespace turismo_oro.Application.Dtos;

public class TurismoLugarCreateDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string EtiquetaPrecio { get; set; } = "Gratis";
    public string Descripcion { get; set; } = string.Empty;
    public IReadOnlyList<string> Destacados { get; set; } = [];
}
