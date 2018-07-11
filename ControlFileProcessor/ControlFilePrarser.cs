using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ControlFileProcessor
{
    public class ControlFileParser
    {
        private string controlFilePath;
        private int tableNameLinePosition = 1;
        private int columnNameLinePosition = 6;
        private List<string> controlFileContents = new List<string>();

        public ControlFileParser(string _controlFilePath)
        {
            controlFilePath = _controlFilePath;
            if (File.Exists(controlFilePath))
            {
                controlFileContents = File.ReadAllText(controlFilePath)
                    .Split('\n').ToList();
            }

        }

        // Process Table Name
        public string GetTableName()
        {
            if(controlFileContents.Count > 0)
            {
                var splittedTableLine = 
                       controlFileContents[tableNameLinePosition]
                       .Trim()
                       .Split(' ');
                if(splittedTableLine.Length == 3)
                {
                    return splittedTableLine[2].Replace("_BSTG", "");
                }
            }

            return string.Empty;
        }

        // Process Column Names
        public List<string> GetColumnNames()
        {
            var columnNames = new List<string>();
            var columnLines = controlFileContents
                    .Skip(columnNameLinePosition - 1) // Skip columns until we reach the column name line
                    .ToList();
            columnLines.RemoveAt(columnLines.Count - 1); // Remove the last line ')'
            foreach (var columnLine in columnLines)
            {
                var tempColumnLine = columnLine.Trim().Replace(",", "");
                var realColumnNames = tempColumnLine.Trim().Split(' ');
                columnNames.Add(realColumnNames[0]);
            }
            return columnNames;
        }
    }
}
