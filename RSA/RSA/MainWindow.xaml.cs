using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace RSA
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Algorithm_RSA obj;
        public MainWindow()
        {
            InitializeComponent();
            obj = new Algorithm_RSA();
            obj.Init();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            TextBoxOriginalFilePath.Text = openFileDialog.FileName;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DateTime time1 = DateTime.Now;
            obj.EncryptFIle(TextBoxOriginalFilePath.Text, @"C:\Users\hp\Desktop\Безпека програм та даних\RSA\enc_try.txt");
            DateTime time2 = DateTime.Now;
            TimeSpan TimeToCrypted = time2.Subtract(time1);

            string answer = string.Format("{0:D3} мілісекунд", TimeToCrypted.Milliseconds);
            MessageBox.Show("Файл зашифровано за " + answer);

            using (var sr = new StreamReader(@"C:\Users\hp\Desktop\Безпека програм та даних\RSA\enc_try.txt"))
            {
                var str = sr.ReadToEnd();
                textBox.Text = Convert.ToString(str);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DateTime time1 = DateTime.Now;
            obj.DecryptFile(@"C:\Users\hp\Desktop\Безпека програм та даних\RSA\enc_try.txt", @"C:\Users\hp\Desktop\Безпека програм та даних\RSA\decr_try.txt");
            DateTime time2 = DateTime.Now;
            TimeSpan TimeToDecrypted = time2.Subtract(time1);

            string answer = string.Format("{0:D3} мілісекунд", TimeToDecrypted.Milliseconds);
            MessageBox.Show("Файл розшифровано за " + answer);
            using (var reader = new StreamReader(@"C:\Users\hp\Desktop\Безпека програм та даних\RSA\decr_try.txt"))
            {
                var str = reader.ReadLine();
                textBox.Text = str.ToString();
            }
        }
    }
}
