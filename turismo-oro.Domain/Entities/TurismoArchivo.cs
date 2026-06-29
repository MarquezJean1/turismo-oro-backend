namespace turismo_oro.Domain.Entities;

public class TurismoArchivo
{
    public Guid TurismoLugarId { get; set; }
    public Guid ArchivoId { get; set; }
    public int Orden { get; set; }

    public TurismoLugar TurismoLugar { get; set; } = null!;
    public Archivo Archivo { get; set; } = null!;
}
