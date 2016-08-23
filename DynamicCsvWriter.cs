using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper.Configuration;

namespace DynamicCsv
{
    public class DynamicCsvWriter : IDisposable
    {
        StreamWriter _streamWriter;
        CsvWriter _csvWriter;
        int line = 0;

        public DynamicCsvWriter(string path)
        {
            var config = new CsvConfiguration
            {
                HasHeaderRecord = true,
                QuoteAllFields = true,
            };

            try
            {
                _streamWriter = new StreamWriter(path);
            }
            catch (IOException ex)
            {
                throw new FileInUseException(path, ex);
            }

            try
            {
                _csvWriter = new CsvWriter(_streamWriter, config);
            }
            catch (Exception ex)
            {
                _streamWriter.Dispose();
                throw new CsvFormatMismatchException(path, ex);
            }
        }

        public void WriteRecord(Dictionary<string, object> record)
        {
            if (line == 0)
                WriteRawRecord(record.Keys); // write header

            WriteRawRecord(record.Values);

            ++line;
        }

        public void WriteRecord<T>(T item)
        {
            var properties = item.GetType().GetProperties();

            if (line == 0)
                WriteRawRecord(properties.Select(x => x.Name)); // write header
            
            WriteRawRecord(properties.Select(x => x.GetValue(item)));

            ++line;
        }

        private void WriteRawRecord(IEnumerable<object> fields)
        {
            foreach (var field in fields)
                _csvWriter.WriteField(field ?? "");

            _csvWriter.NextRecord();
        }

        public void Dispose()
        {
            _csvWriter.Dispose();
            _streamWriter.Dispose();
        }
    }
}
