using Grpc.Core;
using static GrpcService.Algorithm.FractalGenerator;

namespace GrpcService.Services
{
    public class FractalService() : Fractal.FractalBase
    {
        private static readonly string? ServerID = Environment.GetEnvironmentVariable("SERVICE_NAME");

        public override Task<FractalReply> GenerateFractal(FractalRequest request, ServerCallContext context)
        {
            Console.WriteLine("Replied to request.");

            string fractal;
            int seed;

            if (request.Seed != -1)
            {
                (fractal, seed) = JuliaFractal.GenerateFractal(request.Size, request.Quality, request.Seed);
            }
            else
            {
                fractal = JuliaFractal.GenerateFractal(request.Size, request.Quality, out seed);
            }

            return Task.FromResult(new FractalReply
            {
                Fractal = fractal,
                Seed = seed,
            });
        }
    }
}
