using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCsv
{
    public class CsvFormatMismatchException : Exception
    {
        private string path;
        private Exception ex;

        public CsvFormatMismatchException(string path, Exception ex)
        {
            this.path = path;
            this.ex = ex;
        }
    }
}
