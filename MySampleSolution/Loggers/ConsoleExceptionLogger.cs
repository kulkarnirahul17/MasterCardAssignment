using System;
namespace MySampleSolution.Loggers
{
    /// <summary>
    /// Logger class that logs the exceptions to console.
    /// </summary>
    public class ConsoleExceptionLogger : IExceptionLogger
    {
        /// <summary>
        /// Logs the exceptions to console and stack trace.
        /// </summary>
        /// <param name="ex">The exception that needs to be logged</param>
        public void LogException(Exception ex)
        {
            Console.WriteLine("Exception Message: " + ex.Message);
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
        }
    }
}
