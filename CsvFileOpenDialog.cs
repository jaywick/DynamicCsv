using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicCsv
{
    public class CsvFileOpenDialog
    {
        public delegate void FileSelectedEventHandler(string path);
        public event FileSelectedEventHandler FileSelected;

        private OpenFileDialog _actualDialog;

        public CsvFileOpenDialog()
        {
            _actualDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                AutoUpgradeEnabled = true,
                Filter = "Comma Seperated Value file (*.csv)|*.csv",
                DereferenceLinks = true,
                RestoreDirectory = true,
                DefaultExt = "*.csv",
            };
        }

        public void ShowDialog(string title)
        {
            _actualDialog.Title = title;
            _actualDialog.FileOk += _actualDialog_FileOk;
            _actualDialog.ShowDialog();
        }

        void _actualDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (FileSelected != null)
                FileSelected.Invoke(_actualDialog.FileName);
        }
    }
}
