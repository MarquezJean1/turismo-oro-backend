namespace turismo_oro.Application.Dtos;

public class ArchivoDto
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public long TamanioBytes { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string TipoArchivo { get; set; } = string.Empty;
}
