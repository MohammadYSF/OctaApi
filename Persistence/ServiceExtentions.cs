﻿using Application.Repositories.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OctaApi.Application.Repositories;
using OctaApi.Persistence.Contexts;
using OctaApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Persistence;
public static class ServiceExtentions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("OAS");

        services.AddDbContext<WriteDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<ICommandUnitOfWork, UnitOfWork>();


    }
}
