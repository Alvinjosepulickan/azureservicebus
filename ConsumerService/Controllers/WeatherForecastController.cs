using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;

namespace ConsumerService.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ConsumerService _consumerService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IConfiguration configuration) {
            _logger = logger;
            _consumerService = new ConsumerService(new SubscriptionClient(configuration.GetValue<string>("ServiceBus:ConnectionString"),
                                configuration.GetValue<string>("ServiceBus:TopicName"),
                                configuration.GetValue<string>("ServiceBus:Subscription")));
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get() {
            _consumerService.ExecuteAsync();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}