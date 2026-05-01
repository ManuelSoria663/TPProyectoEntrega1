[ApiController]
[route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminHandler _handler;

    [HttpGet("reportes")]
    public async Task<IActionResult> GetReportes()
    {
        
        if (!User.Identity.IsAuthenticated)
            return Unauthorized(); // 401

        if (!User.IsInRole("Admin"))
            return Forbid(); // 403

        var data = await _handler.HandleGetReportes();
        return Ok(data);
    }
}
