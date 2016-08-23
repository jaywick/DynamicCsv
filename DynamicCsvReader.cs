using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace DynamicCsv
{
    public class DynamicCsvReader : IDisposable
    {
        StreamReader _streamReader;
        CsvReader _csvReader;
        string _path;

        public DynamicCsvReader(string path)
        {
            _path = path;

            var config = new CsvConfiguration
            {
                HasHeaderRecord = true,
            };

            try
            {
                _streamReader = new StreamReader(path);
            }
            catch (IOException ex)
            {
                throw new FileInUseException(path, ex);
            }

            try
            {
                _csvReader = new CsvReader(_streamReader, config);
            }
            catch (Exception ex)
            {
                _streamReader.Dispose();
                throw new CsvFormatMismatchException(path, ex);
            }
        }

        public bool Read()
        {
            return _csvReader.Read();
        }

        public string[] FieldHeaders
        {
            get { return _csvReader.FieldHeaders; }
        }

        public Dictionary<string, object> ReadRecord()
        {
            return (_csvReader.GetRecord<dynamic>() as IDictionary<string, object>)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public int GetLineCount()
        {
            return _csvReader.GetRecords<dynamic>().Count();
        }

        public void Dispose()
        {
            _csvReader.Dispose();
            _streamReader.Dispose();
        }
    }
}
