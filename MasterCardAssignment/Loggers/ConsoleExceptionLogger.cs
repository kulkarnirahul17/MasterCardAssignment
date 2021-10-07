using System;
namespace MasterCardAssignment.Loggers
{
    public class ConsoleExceptionLogger : IExceptionLogger
    {        
        public void LogException(Exception ex)
        {
            Console.WriteLine("Exception Message: " + ex.Message);
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
        }
    }
}
