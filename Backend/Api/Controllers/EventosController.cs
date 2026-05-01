[ApiController]
[route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventoHandler _handler;

    public EventosController(IEventoHandler handler)
    {
        _handler = handler;
    }

    // GET: api/eventos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var evento = await _handler.HandleGetById(id);

        if (evento == null)
            return NotFound("Evento inexistente"); // 404

        return Ok(evento); // 200
    }

    // POST: api/eventos
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Evento entity)
    {
        if (entity == null) return BadRequest("Datos inválidos"); // 400

        var result = await _handler.HandleCreate(entity);
        
        // 201 Created
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}
