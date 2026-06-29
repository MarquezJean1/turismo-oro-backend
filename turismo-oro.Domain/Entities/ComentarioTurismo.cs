namespace turismo_oro.Domain.Entities;

public class ComentarioTurismo
{
    public Guid Id { get; set; }
    public Guid TurismoLugarId { get; set; }
    public string Autor { get; set; } = string.Empty;
    public int Calificacion { get; set; }
    public string Texto { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }

    public TurismoLugar TurismoLugar { get; set; } = null!;
}
