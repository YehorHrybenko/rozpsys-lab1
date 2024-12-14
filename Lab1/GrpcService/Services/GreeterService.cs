using Grpc.Core;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private static readonly string? ServerID = Environment.GetEnvironmentVariable("SERVICE_NAME");

        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine("Replied to request.");
            return Task.FromResult(new HelloReply
            {
                Message = $"Hello {request.Name} from server {ServerID}!"
            });
        }
    }
}
