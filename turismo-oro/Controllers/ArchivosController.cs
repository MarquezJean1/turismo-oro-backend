using Microsoft.AspNetCore.Mvc;
using turismo_oro.Application.Interfaces;

namespace turismo_oro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchivosController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;

    public ArchivosController(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    /// <summary>Sube una imagen al servidor. Devuelve los metadatos del archivo guardado.</summary>
    [HttpPost("imagenes")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubirImagen(IFormFile archivo, CancellationToken cancellationToken)
    {
        if (archivo is null || archivo.Length == 0)
            return BadRequest(new { mensaje = "Debe enviar un archivo de imagen." });

        try
        {
            await using var stream = archivo.OpenReadStream();
            var resultado = await _fileStorageService.GuardarImagenAsync(stream, archivo.FileName, cancellationToken);
            return Created(resultado.Url, resultado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
