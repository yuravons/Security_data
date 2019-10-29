using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace RC5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private byte keyLength = 16;
        private string _inputFilepath;
        private string _outputFilepath;

        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void ButtonInputFile_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Title = "Choose file",
                Multiselect = false
            };
            if (dialog.ShowDialog(this) != true) return;
            _inputFilepath = dialog.FileName;
            TextBoxInputFile.Text = _inputFilepath;
        }

        private void ButtonOutputFile_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = true,
                Title = "Choose file for saving"
            };
            if (dialog.ShowDialog(this) != true) return;
            _outputFilepath = dialog.FileName;
            TextBoxOutputFile.Text = _outputFilepath;
        }

        private void PasswordBoxKey_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(PasswordBoxKey.Text);
            writer.Flush();
            stream.Position = 0;
        }

        private void cleareContent()
        {
            TextBoxInputFile.Clear();
            TextBoxOutputFile.Clear();
            PasswordBoxKey.Clear();   
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                #region parameters parsing

                Digest digest = new Digest();

                Stream stream = null;
             
                if (!string.IsNullOrWhiteSpace(PasswordBoxKey.Text))
                    stream = new MemoryStream(Encoding.Unicode.GetBytes(PasswordBoxKey.Text));
                if (stream != null)
                {
                    digest = Md5.CalculateMd5Value(stream);
                }
                var key = new byte[keyLength];

                Buffer.BlockCopy(BitConverter.GetBytes(digest.A), 0, key, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(digest.B), 0, key, 4, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(digest.C), 0, key, 8, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(digest.D), 0, key, 12, 4);

                #endregion

                var algorithm = new RC5();
                var md5Algoritm = new Md5();
                MessageBox.Show((new Md5().Calculate(stream)).ToString());
                var rawInputStream = new FileStream(_inputFilepath, FileMode.Open, FileAccess.Read);
                var rawOutputStream = new FileStream(_outputFilepath, FileMode.Create, FileAccess.ReadWrite);

                var inputStream = new BufferedStream(rawInputStream);
                var outputStream = new BufferedStream(rawOutputStream);
                
                int operation = ComboBoxOperation.SelectedIndex;

                var hash = md5Algoritm.Calculate(inputStream);

                if (operation == 0)
                    algorithm.Encrypt(key, inputStream, outputStream);
                else
                    algorithm.Decrypt(key, inputStream, outputStream);

                inputStream.Close();
                outputStream.Close();

                rawInputStream.Close();
                rawOutputStream.Close();

                cleareContent();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception.StackTrace);
            }
        }
    }
}
