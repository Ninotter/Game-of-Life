using System.Drawing;
namespace ServerChat
{
    internal static class Log
    {
        internal static Color defaultColor = Color.White;
        internal static void Write(LogType type, string message)
        {
            switch (type)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            Console.WriteLine($"{DateTime.Now} - {type} - {message}");
        }
    }

    internal enum LogType
    {
        Info,
        Warning,
        Error
    }
}
