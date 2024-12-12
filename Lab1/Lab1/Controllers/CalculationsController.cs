using Microsoft.AspNetCore.Mvc;

using Grpc.SDK;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationsController : ControllerBase
    {
        [HttpGet(Name = "Calculations")]
        public Task<string> Get([FromServices] IGrpcService grpcService, CancellationToken cancellationToken)
        {
            var result = grpcService.SayHelloAsync("GRPC", cancellationToken);

            return result;
        }
    }
}
