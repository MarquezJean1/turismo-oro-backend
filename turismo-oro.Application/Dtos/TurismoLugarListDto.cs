using System.Text.Json.Serialization;

namespace turismo_oro.Application.Dtos;

public class TurismoLugarListDto
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

    [JsonPropertyName("priceLabel")]
    public string EtiquetaPrecio { get; set; } = string.Empty;

    [JsonPropertyName("photo")]
    public string? FotoPrincipal { get; set; }

    [JsonPropertyName("rating")]
    public double CalificacionPromedio { get; set; }

    [JsonPropertyName("reviewCount")]
    public int TotalComentarios { get; set; }
}
