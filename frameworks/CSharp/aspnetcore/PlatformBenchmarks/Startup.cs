// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Data.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace PlatformBenchmarks
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnv)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnv.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostingEnv.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(Program.Args)
                ;

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);

            // Common DB services
            services.AddSingleton<IRandom, DefaultRandom>();
            //services.AddEntityFrameworkSqlServer();

            var appSettings = Configuration.Get<AppSettings>();
            Console.WriteLine($"Database: {appSettings.Database}");

            services.AddSingleton<DbProviderFactory>(NpgsqlFactory.Instance);

            // Is Singleton valid?
            services.AddSingleton<RawDb>();
        }


        public void Configure(IApplicationBuilder app)
        {
            
        }
    }
}
