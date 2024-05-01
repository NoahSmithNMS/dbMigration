using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;


//adapted from https://github.com/lecaillon/Evolve
//Author: Noah Smith


namespace AncientCiv_Migrate
{
    public class Program
    {
        private static readonly string EnvironmentName;
        private static readonly IConfiguration Configuration;

        static Program()
        {
            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static int Main(string[] args)
        {
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            MigrateDatabase();

            return Host.CreateDefaultBuilder(args)
                       .ConfigureAppConfiguration((hostingContext, config) =>
                       {
                           config.AddConfiguration(Configuration);
                       })
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                       })
                       .UseSerilog();
        }

        private static void MigrateDatabase()
        {
            // exclude db/datasets from production and staging environments/ NOT YET IMPLEMENTED
            string location = EnvironmentName == Environments.Production || EnvironmentName == Environments.Staging
                ? "db/migrations"
                : "db";



            try
            {
                //Edit connection string un appsettings.json
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                //this is where you should set you connection string. Datasource will be the name of your
                //sql server. probably the name of you computer followed by MSSQLSERVER
                    DataSource = "DESKTOP-KQH9R42\\MSSQLSERVER01",
                    IntegratedSecurity = true,
                    //if necessary for your machine
                    //UserID = "sa",
                    //Password = "password",
                    InitialCatalog = "ancientciv",
                    TrustServerCertificate = true
                };

                //msg should Log somewhere, not write to console. Should be adjusted in future
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    var evolve = new Evolve.Evolve(connection, msg => Console.WriteLine(msg))
                    {
                        Locations = new[] { location },
                        IsEraseDisabled = true,
                        Placeholders = new Dictionary<string, string>
                        {
                            ["${database}"] = "ancientciv"
                        }
                    };


                    //Executes migrate action. this will run each script in order based on file name. see db folder
                    evolve.Migrate();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed.", ex);
                throw;
            }
        }
    }
}