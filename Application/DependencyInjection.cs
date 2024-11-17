﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.Abstractions.Behaviors;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assembly);
                cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

            return services;
        }
    }
}
