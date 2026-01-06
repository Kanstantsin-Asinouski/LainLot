using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class WeatherForecastAuthController(ILogger<WeatherForecastAuthController> logger) : ControllerBase
    {
        private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

        private readonly ILogger<WeatherForecastAuthController> _logger = logger;

        [HttpGet]
        public IEnumerable<WeatherForecastAuth> Get()
        {
            return [.. Enumerable.Range(1, 5).Select(index => new WeatherForecastAuth
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })];
        }
    }
}

public class WeatherForecastAuth
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}