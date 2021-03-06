﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Data.Components;

namespace Service.Rest.Tests
{
    public class Config
    {
        public static string GetConnectionString() => 
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build()
                .GetConnectionString("Test");
        
        public static DbContextOptions<Context> GetContextOptions() => 
            new DbContextOptionsBuilder<Context>()
                   .UseSqlServer(Config.GetConnectionString())
                   .Options;

        public static Context CreateContext() => 
            new(Config.GetContextOptions());
    }
}
