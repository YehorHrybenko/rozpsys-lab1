using Grpc.Core;
using GrpcService;

namespace Grpc.SDK;

public interface IGrpcService
{
    Task<string> SayHelloAsync(string name, CancellationToken cancellationToken);
}

public class GrpcService : IGrpcService
{
    private readonly Greeter.GreeterClient greeterClient;

    public GrpcService(Greeter.GreeterClient greeterClient)
    {
        this.greeterClient = greeterClient;
    }

    public async Task<string> SayHelloAsync(string name, CancellationToken cancellationToken)
    {
        try
        {
            var result = await greeterClient.SayHelloAsync(new HelloRequest { Name = name }, null, null, cancellationToken);
            return result.Message;
        }
        catch (RpcException)
        {
            throw;
            //throw new ApplicationException("Backend service unavailable.");
        }
    }
}
