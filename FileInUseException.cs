using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicCsv
{
    public class FileInUseException : Exception
    {
        private string path;
        private System.IO.IOException ex;

        public FileInUseException(string path, System.IO.IOException ex)
        {
            this.path = path;
            this.ex = ex;
        }
    }
}
