using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace ControlFileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var exeFilePath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            var controlFolderPath = string.Format("{0}\\{1}",
                exeFilePath, "ControlFiles");

            DirectoryInfo d = new DirectoryInfo(controlFolderPath);
            FileInfo[] Files = d.GetFiles("*.txt");

            foreach (FileInfo file in Files)
            {
                var controlFileParser = new ControlFileParser(file.FullName);
                var tableName = controlFileParser.GetTableName();
                var columnNames = controlFileParser.GetColumnNames();

                PrintTableName(tableName);
                Console.WriteLine("\n");
                PrintColumnNames(columnNames);
            }

            Console.ReadLine();
        }

        private static void PrintTableName(string tableName)
        {
            Console.WriteLine("Table Name: "+ tableName);
        }

        private static void PrintColumnNames(List<string> columnNames)
        {
            Console.WriteLine("Column Names\n");
            foreach (var columnName in columnNames)
            {
                Console.WriteLine(columnName);
            }
        }
    }
}
