using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using static GrpcService.Algorithm.FractalGenerator;

namespace GrpcService.Services
{
    [Authorize]
    public class FractalService() : Fractal.FractalBase
    {
        private static readonly string? ServerID = Environment.GetEnvironmentVariable("SERVICE_NAME");

        public override Task<FractalReply> GenerateFractal(FractalRequest request, ServerCallContext context)
        {
            Console.WriteLine("Replied to request.");
            
            int seed = request.Seed != -1 ? request.Seed : new Random().Next();

            var stopwatch = Stopwatch.StartNew();

            var fractal = JuliaFractal.GenerateFractal(request.Size, request.Quality, request.Seed);

            stopwatch.Stop();

            Console.WriteLine($"Time spent on calculations: {stopwatch.ElapsedMilliseconds} ms");

            return Task.FromResult(new FractalReply
            {
                Fractal = fractal,
                Seed = seed,
            });
        }
    }
}
