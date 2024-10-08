﻿using Business.Events;
using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ioc
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string? defaultConnection = configuration.GetConnectionString("DefaultConnection");

            if(string.IsNullOrEmpty(defaultConnection))
                throw new ArgumentNullException(nameof(defaultConnection));

            services.AddDbContext<VendasDbContext>(options => options.UseSqlite(defaultConnection, b => b.MigrationsAssembly("Data")));

            services.AddScoped<IVendaService, VendaService>();
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IVendaRepository, VendaRepository>();

            return services;
        }
    }
}
