using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("/time2")]
[ApiController]
public class TimeController : ControllerBase {
    [HttpGet]
    public object Get() {
        return new { date = DateTime.UtcNow.ToString("d") };
    }
}