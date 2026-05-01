[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly IReservaHandler _handler; // Referencia al Handler

    public ReservasController(IReservaHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReservaRequest request)
    {
        // --- INTEGRACIÓN 400 Bad Request ---
        if (request == null || request.ButacaId <= 0)
            return BadRequest("Datos inválidos: Se requiere ID de evento y butaca.");

        try 
        {
            var resultado = await _handler.Handle(request);
            
            // --- INTEGRACIÓN 201 Created ---
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }
        catch (ButacaOcupadaException ex)
        {
            // --- INTEGRACIÓN 409 Conflict (Concurrencia) ---
            return Conflict(ex.Message);
        }
        catch (Exception)
        {
            // --- INTEGRACIÓN 500 Internal Server Error ---
            return StatusCode(500, "Error no controlado en el servidor.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reserva = await _handler.ObtenerPorId(id);
        
        // --- INTEGRACIÓN 404 Not Found ---
        if (reserva == null) return NotFound("La reserva no existe.");

        // --- INTEGRACIÓN 200 OK ---
        return Ok(reserva);
    }
}
