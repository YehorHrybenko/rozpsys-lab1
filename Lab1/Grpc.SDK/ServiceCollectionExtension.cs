﻿
using GrpcService;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.SDK
{
    public static class ServiceCollectionExtension
    {
        public static void AddGrpcSdk(this IServiceCollection services)
        {
            services.AddGrpcClient<Greeter.GreeterClient>(client =>
            {
                client.Address = new Uri("http://provider:8080");
            });


            services.AddScoped<IGrpcService, GrpcService>();
        }
    }
}