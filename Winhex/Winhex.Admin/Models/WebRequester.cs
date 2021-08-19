using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Winhex.Admin.Models
{
    public class WebRequester
    {
        public static string Get(string Url, string Data)
        {
            try
            {
                WebRequest req = WebRequest.Create(Url + "?" + Data);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string Out = sr.ReadToEnd();
                sr.Close();
                return Out;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }
        public static bool Post(object obj, string url)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                WebRequest
                    request = WebRequest.Create(url); //"http://www.ihih.somee.com/upload");//"https://localhost:5001/upload"); "https://localhost:44373/upload"

                request.Method = "POST"; // для отправки используется метод Post

                string data = JsonConvert.SerializeObject(obj);

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }

                response.Close();
                Console.WriteLine("Запрос выполнен...");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetHash(string str)
        {
            byte[] hash = Encoding.ASCII.GetBytes(str);
            byte[] hashenc = new MD5CryptoServiceProvider().ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;
        }
    }
}