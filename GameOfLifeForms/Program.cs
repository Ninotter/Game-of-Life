using GameOfLifeForms.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Runtime;

namespace GameOfLifeForms
{
    internal static class Program
    {
        public static IConfiguration Configuration;
        public static string SqliteDbPath { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).Build();
            var builder = new ConfigurationBuilder()
            .AddJsonFile("config.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            SqliteDbPath = Program.Configuration.GetSection("SQLITE_DB_PATH").Get<string>();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new GameOfLifeForm());
            Environment.Exit(0);
        }
    }
}