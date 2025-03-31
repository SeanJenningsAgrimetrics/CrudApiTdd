﻿using System.Diagnostics;
using NUnit.Framework;

namespace Integration.Db;

[SetUpFixture]
public class Startup
{
    private Process database_process = null!;
    
    [OneTimeSetUp]
    public void BeforeAllTests()
    {
        var server = Settings.Database.Server;
        database_process = new Process();
        var startInfo = new ProcessStartInfo
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "cmd.exe",
            Arguments = $"/C sqllocaldb.exe Start {server}"
        };
        database_process.StartInfo = startInfo;
        database_process.Start();
    }
    
    [OneTimeTearDown]
    public void AfterAllTests()
    {
        database_process.Kill();
    }
}