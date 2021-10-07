using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.FileReaders
{
    public class OrderInputReader : IInputReader
    {
        public IEnumerable<OrderInfo> ReadInput(string filePath, char delimiter)
        {
            List<OrderInfo> orderInfos = new();

            try
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
                string line;                
                bool columnHeadersRead = false;

                using (StreamReader file = new StreamReader(fullPath))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        //Other ways of reading are through TextFieldParser where a delimiter can be set
                        //But for this solution, we can skip the headers line
                        if (!columnHeadersRead)
                        {
                            columnHeadersRead = true;
                            continue;
                        }
                        string[] words = line.Split(delimiter);

                        DateTime.TryParse(words[0], out DateTime orderDate);
                        string model = words[1];
                        //Ignore first character because it contains $ symbol
                        decimal.TryParse(words[2][1..], out decimal price);                        
                        int.TryParse(words[3], out int quantity);
                        orderInfos.Add(new OrderInfo(orderDate, model, price, quantity));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read csv file: {filePath} with the following error {ex.Message}");
            }

            return orderInfos;
        }
    }
}
