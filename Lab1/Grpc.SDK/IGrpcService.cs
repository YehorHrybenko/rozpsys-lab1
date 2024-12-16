using Grpc.Core;
using GrpcService;

namespace Grpc.SDK;

public interface IGrpcService
{
    (string, int) GenerateFractal(int size, int seed, int quality, CancellationToken cancellationToken);
}

public class GrpcService : IGrpcService
{
    private readonly Fractal.FractalClient fractalClient;

    public GrpcService(Fractal.FractalClient fractalClient)
    {
        this.fractalClient = fractalClient;
    }

    public (string, int) GenerateFractal(int size, int seed, int quality, CancellationToken cancellationToken)
    {
        try
        {
            var result = fractalClient.GenerateFractal(new FractalRequest { Size = size, Seed = seed, Quality = quality }, null, DateTime.UtcNow.AddSeconds(7), cancellationToken);
            return (result.Fractal, result.Seed);
        }
        catch (RpcException)
        {
            Console.WriteLine("Backend service unavailable.");
            throw;
        }
    }
}
