using turismo_oro.Application.Dtos;
using turismo_oro.Domain.Entities;

namespace turismo_oro.Application.Mappings;

public static class TurismoMapper
{
    public static TurismoLugarListDto ToListDto(TurismoLugar lugar)
    {
        var comentarios = lugar.Comentarios;
        var fotoPrincipal = lugar.TurismoArchivos
            .OrderBy(ta => ta.Orden)
            .Select(ta => ta.Archivo.Url)
            .FirstOrDefault();

        return new TurismoLugarListDto
        {
            Id = lugar.Id,
            Nombre = lugar.Nombre,
            Categoria = lugar.Categoria,
            Latitud = lugar.Latitud,
            Longitud = lugar.Longitud,
            EtiquetaPrecio = lugar.EtiquetaPrecio,
            FotoPrincipal = fotoPrincipal,
            CalificacionPromedio = CalcularPromedio(comentarios),
            TotalComentarios = comentarios.Count
        };
    }

    public static TurismoLugarDto ToDto(TurismoLugar lugar)
    {
        var comentarios = lugar.Comentarios
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new ComentarioDto
            {
                Id = c.Id,
                Autor = c.Autor,
                Calificacion = c.Calificacion,
                Texto = c.Texto,
                FechaCreacion = c.CreatedAt
            })
            .ToList();

        return new TurismoLugarDto
        {
            Id = lugar.Id,
            Nombre = lugar.Nombre,
            Categoria = lugar.Categoria,
            Latitud = lugar.Latitud,
            Longitud = lugar.Longitud,
            Direccion = lugar.Direccion,
            EtiquetaPrecio = lugar.EtiquetaPrecio,
            Descripcion = lugar.Descripcion,
            Destacados = ParseDestacados(lugar.Destacados),
            Fotos = lugar.TurismoArchivos
                .OrderBy(ta => ta.Orden)
                .Select(ta => ta.Archivo.Url)
                .ToList(),
            CalificacionPromedio = CalcularPromedio(lugar.Comentarios),
            TotalComentarios = lugar.Comentarios.Count,
            Comentarios = comentarios,
            FechaCreacion = lugar.CreatedAt
        };
    }

    public static string SerializarDestacados(IReadOnlyList<string> destacados) =>
        string.Join('|', destacados.Where(d => !string.IsNullOrWhiteSpace(d)).Select(d => d.Trim()));

    public static IReadOnlyList<string> ParseDestacados(string? destacados) =>
        string.IsNullOrWhiteSpace(destacados)
            ? []
            : destacados.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    private static double CalcularPromedio(ICollection<ComentarioTurismo> comentarios) =>
        comentarios.Count == 0
            ? 0
            : Math.Round(comentarios.Average(c => c.Calificacion), 2);
}
