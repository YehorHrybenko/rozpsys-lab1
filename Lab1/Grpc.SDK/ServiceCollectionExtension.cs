
using GrpcService;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.SDK
{
    public static class ServiceCollectionExtension
    {
        private static readonly string? clientAddress = Environment.GetEnvironmentVariable("CLIENT_ADDRESS");

        public static void AddGrpcSdk(this IServiceCollection services)
        {
            services.AddGrpcClient<Greeter.GreeterClient>(client =>
            {
                if (clientAddress == null) throw new ApplicationException("Client address not specified.");
                client.Address = new Uri(clientAddress);
            });

            services.AddScoped<IGrpcService, GrpcService>();
        }
    }
}
