using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSA
{
    public class Algorithm_RSA
    {
        private RSACryptoServiceProvider RSAService;
        public string pub_key { get; set; }
        public string pr_key { get; set; }
        public void Init()
        {
            RSAService = new RSACryptoServiceProvider(1024);

            byte[] key = RSAService.ExportCspBlob(false);
            File.WriteAllBytes(@"C:\Users\hp\Desktop\Безпека програм та даних\RSA\private_key.xml", key);

            key = RSAService.ExportCspBlob(true);
            File.WriteAllBytes(@"C:\Users\hp\Desktop\Безпека програм та даних\RSA\public_key.xml", key);

            var writer = new StreamWriter(@"C:\Users\hp\Desktop\Безпека програм та даних\RSA\private_key.xml");
            pr_key = RSAService.ToXmlString(true);
            writer.Write(pr_key);
            writer.Close();

            writer = new StreamWriter(@"C:\Users\hp\Desktop\Безпека програм та даних\RSA\public_key.xml");
            pub_key = RSAService.ToXmlString(false);
            writer.Write(pub_key);
            writer.Close();
        }

        public string Encrypt(string data)
        {
            RSAService.FromXmlString(pub_key);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var encryptedData = RSAService.Encrypt(dataBytes, false);
            return Convert.ToBase64String(encryptedData);
        }

        public byte[] Encrypt(byte[] data)
        {
            return RSAService.Encrypt(data, false);
        }

        public string Decrypt(string data)
        {
            var dataBytes = Convert.FromBase64String(data);
            RSAService.FromXmlString(pr_key);
            var result = RSAService.Decrypt(dataBytes, false);
            return Encoding.UTF8.GetString(result);
        }
        public void EncryptFIle(string file, string outFile)
        {
            RSAService.FromXmlString(pub_key);
            var f = new FileStream(file, FileMode.Open);
            var buffer = new byte[78];

            var fw = new FileStream(outFile, FileMode.Create);

            while (f.Read(buffer, 0, buffer.Length) != 0)
            {
                var a = Encrypt(buffer);
                fw.Write(a, 0, a.Length);
            }

            f.Close();
            fw.Close();
        }

        public void DecryptFile(string inputFile, string outputFile)
        {
            RSAService.FromXmlString(pr_key);

            var f = new FileStream(inputFile, FileMode.Open);

            var buffer = new byte[128];

            var fw = new FileStream(outputFile, FileMode.Create);

            while (f.Read(buffer, 0, buffer.Length) != 0)
            {
                var a = RSAService.Decrypt(buffer, false);
                fw.Write(a, 0, a.Length);
            }

            f.Close();
            fw.Close();
        }
    }
}