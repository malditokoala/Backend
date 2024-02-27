
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomService _randomServiceSingleton;
        private IRandomService _randomServiceScoped;
        private IRandomService _randomServiceTransient;

        private IRandomService _random2ServiceSingleton;
        private IRandomService _random2ServiceScoped;
        private IRandomService _random2ServiceTransient;

        public RandomController(
            [FromKeyedServices("randomSingleton")] IRandomService randomServiceSingleton,
            [FromKeyedServices("randomScoped")] IRandomService randomServiceScoped,
            [FromKeyedServices("randomTransient")] IRandomService randomServiceTransient,
             [FromKeyedServices("randomSingleton")] IRandomService random2ServiceSingleton,
            [FromKeyedServices("randomScoped")] IRandomService random2ServiceScoped,
            [FromKeyedServices("randomTransient")] IRandomService random2ServiceTransient
            )
        {
            _randomServiceSingleton = randomServiceSingleton;
            _randomServiceScoped = randomServiceScoped;
            _randomServiceTransient = randomServiceTransient;

            _random2ServiceSingleton = random2ServiceSingleton;
            _random2ServiceScoped = random2ServiceScoped;
            _random2ServiceTransient = random2ServiceTransient;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();
            result.Add("Singleton 1:", _randomServiceSingleton.Value);
            result.Add("Scoped 1:", _randomServiceScoped.Value);
            result.Add("Transient 1:", _randomServiceTransient.Value);

            result.Add("Singleton 2:", _random2ServiceSingleton.Value);
            result.Add("Scoped 2:", _random2ServiceScoped.Value);
            result.Add("Transient 2:", _random2ServiceTransient.Value);

            return result;

        }
    }
}
