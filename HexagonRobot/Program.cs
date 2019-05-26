using System;

namespace HexagonRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var _logger = new Logger();

            _logger.Log(Logger.LogLevel.IMPORTANT, "Application STARTED.");

            var runRobot = new RunRobot(_logger);
            runRobot.Loop();

            _logger.Log(Logger.LogLevel.IMPORTANT, "Application FINISHED.");
            Console.WriteLine("\nYou have chosen to Exit. Good Bye...\n");
        }
    }
}
