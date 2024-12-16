using Microsoft.AspNetCore.Mvc;

using Grpc.SDK;
using Microsoft.AspNetCore.Authorization;

namespace Lab1.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FractalGenerationController : ControllerBase
{
    private static readonly string? ServerID = Environment.GetEnvironmentVariable("SERVICE_NAME");

    [HttpGet(Name = "generate")]
    public string Get([FromServices] IGrpcService grpcService, CancellationToken cancellationToken, int size = 256, int quality = 80, int inputSeed = -1)
    {
        var (fractal, generationSeed) = grpcService.GenerateFractal(size, inputSeed, quality, cancellationToken);

        Console.WriteLine($"Generated image using seed {generationSeed}");

        return fractal;
    }
}
