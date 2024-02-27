using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Thread.Sleep(1000);
            Console.WriteLine("Conexion a la base de datos terminada");

            Thread.Sleep(1000);
            Console.WriteLine("Envio de mail terminado");
            Console.WriteLine("Toda ha terminado");
            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            var task = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexion a la base de datos terminada");
                return 8;
            });
            var task2 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Envio de mail terminado");
                return 2;
            });
            task.Start();
            task2.Start();
            Console.WriteLine("Hago otra cosas");

            var result = await task;
            var result2 = await task2;
            Console.WriteLine("Toda ha terminado");
            stopwatch.Stop();
            return Ok(result + " " + result2+ " " + stopwatch.Elapsed);
        }
    }
}
