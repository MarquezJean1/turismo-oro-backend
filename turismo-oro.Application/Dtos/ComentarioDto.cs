using System.Text.Json.Serialization;

namespace turismo_oro.Application.Dtos;

public class ComentarioDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("author")]
    public string Autor { get; set; } = string.Empty;

    [JsonPropertyName("rating")]
    public int Calificacion { get; set; }

    [JsonPropertyName("comment")]
    public string Texto { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string FechaFormateada => FechaCreacion.ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-EC"));

    [JsonIgnore]
    public DateTime FechaCreacion { get; set; }
}
