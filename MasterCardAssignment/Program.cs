using System;
using MasterCardAssignment.Business;
using MasterCardAssignment.FileReaders;
using MasterCardAssignment.Loggers;

namespace MasterCardAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Not using any DI containers for this solution, but we can simulate constructor injection.
            IExceptionLogger exceptionLogger = new ConsoleExceptionLogger();
            IInputReader inputReader = new OrderInputReader(exceptionLogger);
            IReaderCoordinator readerCoordinator = new ReaderCoordinator(inputReader, exceptionLogger);
            IOutputLogger logger = new FileOutputLogger(exceptionLogger);
            IOrderResultsBusiness business = new OrderResultsBusiness();

            IOrderMerger orderMerger = new OrderMerger(readerCoordinator, logger, business);
            

            orderMerger.ReadAndMergeOrders();

            Console.WriteLine("Successfully written the output to output.txt");

        }

       
    }
}
