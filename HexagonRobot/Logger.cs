using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HexagonRobot
{
    /// <summary>
    /// A Class that logs details of each stage of the application to a Log File.
    /// </summary>
    public class Logger
    {
        public enum LogLevel
        {
            CREATE = 1,
            INFO = 2,
            START = 3,
            END = 4,
            WARNING = 5,
            ERROR = 6,
            IMPORTANT = 7
        }

        private const string FILE_EXT = ".log";
        private string folderPath = Directory.GetCurrentDirectory();
        private readonly string datetimeFormat;
        private readonly string logFilename;
        private readonly string filePath;

        /// <summary>
        /// Initializing certain details of the Logger class.
        /// </summary>
        public Logger()
        {
            datetimeFormat = "yyyy-MM-dd HH:mm:ss";
            logFilename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " Log - " + DateTime.Now.ToString("yyyy-MM-dd") + FILE_EXT;
            filePath = folderPath + "\\Log\\" + logFilename;
            if (!Directory.Exists(folderPath + "\\Log"))
            {
                var newFile = Directory.CreateDirectory(folderPath + "\\Log");
                Log(LogLevel.IMPORTANT, "Creating new Log File.");
            }
        }

        /// <summary>
        /// Writes the logging details to the Log File.
        /// </summary>
        /// <param name="level">The level type of each individual log</param>
        /// <param name="text">Notes about each log</param>
        public void Log(LogLevel level, string text)
        {
            using (var stream = new StreamWriter(filePath, true))
            {
                stream.WriteLine(FormatLog(level, text));
                stream.Close();
            }
        }

        /// <summary>
        /// Prepends standard information for each log.
        /// </summary>
        /// <param name="level">The level type of each individual log</param>
        /// <param name="text">Notes about each log</param>
        /// <returns></returns>
        public string FormatLog(LogLevel level, string text)
        {
            switch (level)
            {
                case LogLevel.CREATE:
                    return DateTime.Now.ToString(datetimeFormat) + " [CREATE] | " + text;
                case LogLevel.INFO:
                    return DateTime.Now.ToString(datetimeFormat) + " [INFO] | " + text;
                case LogLevel.START:
                    return DateTime.Now.ToString(datetimeFormat) + " [START] | " + text;
                case LogLevel.END:
                    return DateTime.Now.ToString(datetimeFormat) + " [END] | " + text;
                case LogLevel.WARNING:
                    return DateTime.Now.ToString(datetimeFormat) + " [WARNING] | " + text;
                case LogLevel.ERROR:
                    return DateTime.Now.ToString(datetimeFormat) + " [ERROR] | " + text;
                case LogLevel.IMPORTANT:
                    return DateTime.Now.ToString(datetimeFormat) + " [IMPORTANT] | " + text;
                default:
                    return "";
            }
        }
    }
}
