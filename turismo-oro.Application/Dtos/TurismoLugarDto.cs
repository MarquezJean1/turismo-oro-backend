using System.Text.Json.Serialization;

namespace turismo_oro.Application.Dtos;

public class TurismoLugarDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Categoria { get; set; } = string.Empty;

    [JsonPropertyName("lat")]
    public decimal Latitud { get; set; }

    [JsonPropertyName("lng")]
    public decimal Longitud { get; set; }

    [JsonPropertyName("direccion")]
    public string Direccion { get; set; } = string.Empty;

    [JsonPropertyName("priceLabel")]
    public string EtiquetaPrecio { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Descripcion { get; set; } = string.Empty;

    [JsonPropertyName("highlights")]
    public IReadOnlyList<string> Destacados { get; set; } = [];

    [JsonPropertyName("photos")]
    public IReadOnlyList<string> Fotos { get; set; } = [];

    [JsonPropertyName("rating")]
    public double CalificacionPromedio { get; set; }

    [JsonPropertyName("reviewCount")]
    public int TotalComentarios { get; set; }

    [JsonPropertyName("reviews")]
    public IReadOnlyList<ComentarioDto> Comentarios { get; set; } = [];

    [JsonIgnore]
    public DateTime FechaCreacion { get; set; }
}
