using GameOfLifeForms.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Runtime;

namespace GameOfLifeForms
{
    /// <summary>
    /// WinForm program for the Game of Life application
    /// </summary>
    internal static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        /// The path to the SQLite database
        /// </summary>
        public static string SqliteDbPath { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).Build();
            try
            {
                //Retrieving the configuration from the config.json file
                var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);
                Configuration = builder.Build();
                //Setting the SQLite database path
                SqliteDbPath = Program.Configuration.GetSection("SQLITE_DB_PATH").Get<string>();
            }
            catch
            {
                MessageBox.Show("Error reading config.json file. Please make sure it exists and is properly formatted.");
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new GameOfLifeForm());
            Environment.Exit(0);
        }
    }
}