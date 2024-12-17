using Microsoft.AspNetCore.Mvc;

using Grpc.SDK;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Net;

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
        var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader != null && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();

            var stopwatch = Stopwatch.StartNew();

            var (fractal, generationSeed) = grpcService.GenerateFractal(size, inputSeed, quality, token, cancellationToken);

            stopwatch.Stop();
            Console.WriteLine($"Generated image using seed {generationSeed}");
            Console.WriteLine($"Time spent on request: {stopwatch.ElapsedMilliseconds} ms");

            return fractal;
        }

        return "Error: unauthorized request";
    }
}
