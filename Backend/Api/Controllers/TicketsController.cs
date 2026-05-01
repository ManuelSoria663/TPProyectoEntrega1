[ApiController]
[route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketHandler _handler;

    public TicketsController(ITicketHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("reservar")]
    public async Task<IActionResult> Reservar([FromBody] ReservaRequest request)
    {
        try 
        {
            var ticket = await _handler.HandleReserva(request);
            return Ok(ticket); // 200 OK
        }
        catch (ButacaOcupadaException ex) // Excepción lanzada por tu Handler
        {
            // 409 Conflict - Clave para tu TP de Ticketing
            return Conflict("La butaca ya fue reservada por otro usuario."); 
        }
        catch (ArgumentException)
        {
            return BadRequest("ID de butaca o evento inválido."); // 400
        }
        catch (Exception)
        {
            return StatusCode(500, "Error inesperado en el servidor."); // 500
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var exito = await _handler.HandleDelete(id);

        if (!exito)
            return NotFound("Ticket inexistente"); // 404

        return NoContent(); // 204 No Content
    }
}
