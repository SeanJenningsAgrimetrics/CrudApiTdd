﻿using Microsoft.Extensions.Configuration;

namespace Integration.Db;

public static class Settings
{
    private static readonly IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

    public static class Database
    {
        public static string Server => configuration["Database:Server"]!;
        public static string Connection => configuration["ConnectionString"]!;
    }
}