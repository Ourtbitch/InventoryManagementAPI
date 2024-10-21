using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly IRandomService _randomServiceSingleton;
        private readonly IRandomService _randomServiceScoped;
        private readonly IRandomService _randomServiceTransient;
        private readonly IRandomService _randomService2Singleton;
        private readonly IRandomService _randomService2Scoped;
        private readonly IRandomService _randomService2Transient;

        public RandomController(
            [FromKeyedServices("randomSingleton")] IRandomService randomServiceSingleton,
            [FromKeyedServices("randomScoped")] IRandomService randomServiceScoped,
            [FromKeyedServices("randomTransient")] IRandomService randomServiceTransient,
            [FromKeyedServices("randomSingleton")] IRandomService random2ServiceSingleton,
            [FromKeyedServices("randomScoped")] IRandomService random2ServiceScoped,
            [FromKeyedServices("randomTransient")] IRandomService random2ServiceTransient)
        {
            _randomServiceSingleton = randomServiceSingleton;
            _randomServiceScoped = randomServiceScoped;
            _randomServiceTransient = randomServiceTransient;
            _randomService2Singleton = random2ServiceSingleton;
            _randomService2Scoped = random2ServiceScoped;
            _randomService2Transient = random2ServiceTransient;
        }
        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();

            result.Add("Singleton 1", _randomServiceSingleton.Value);
            result.Add("Scoped 1", _randomServiceScoped.Value);
            result.Add("Transient 1", _randomServiceTransient.Value);

            result.Add("Singleton 2", _randomService2Singleton.Value);
            result.Add("Scoped 2", _randomService2Scoped.Value);
            result.Add("Transient 2", _randomService2Transient.Value);


            return result;
        }
    }
}
