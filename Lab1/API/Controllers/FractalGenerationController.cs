using Microsoft.AspNetCore.Mvc;

using Grpc.SDK;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FractalGenerationController : ControllerBase
{
    private static readonly string? ServerID = Environment.GetEnvironmentVariable("SERVICE_NAME");

    [HttpGet(Name = "generate")]
    public string Get([FromServices] IGrpcService grpcService, CancellationToken cancellationToken, int size = 256, int quality = 80, int inputSeed = -1)
    {
        var stopwatch = Stopwatch.StartNew();

        var (fractal, generationSeed) = grpcService.GenerateFractal(size, inputSeed, quality, cancellationToken);

        stopwatch.Stop();
        Console.WriteLine($"Generated image using seed {generationSeed}");
        Console.WriteLine($"Time spent on request: {stopwatch.ElapsedMilliseconds} ms");

        return fractal;
    }
}
