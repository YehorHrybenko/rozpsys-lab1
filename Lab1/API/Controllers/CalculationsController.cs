using Microsoft.AspNetCore.Mvc;

using Grpc.SDK;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationsController : ControllerBase
    {
        private static readonly string? ServerID = Environment.GetEnvironmentVariable("SERVICE_NAME");

        [HttpGet(Name = "Calculations")]
        public Task<string> Get([FromServices] IGrpcService grpcService, CancellationToken cancellationToken)
        {
            var result = grpcService.SayHelloAsync($"YEGOR through api {ServerID}", cancellationToken);

            return result;
        }
    }
}
