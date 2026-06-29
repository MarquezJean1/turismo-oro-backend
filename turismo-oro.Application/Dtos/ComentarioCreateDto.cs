using System.Text.Json.Serialization;

namespace turismo_oro.Application.Dtos;

public class ComentarioCreateDto
{
    [JsonPropertyName("author")]
    public string Autor { get; set; } = string.Empty;

    [JsonPropertyName("rating")]
    public int Calificacion { get; set; }

    [JsonPropertyName("comment")]
    public string Texto { get; set; } = string.Empty;
}
