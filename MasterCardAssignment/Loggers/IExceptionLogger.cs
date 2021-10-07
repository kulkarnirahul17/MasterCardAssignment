using System;
namespace MasterCardAssignment.Loggers
{
    public interface IExceptionLogger
    {
        void LogException(Exception ex);      
    }
}
