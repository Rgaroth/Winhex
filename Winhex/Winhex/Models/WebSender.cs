using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using Winhex.Interfaces;

namespace Winhex.Models
{
    public class WebSender : ILogSender
    {
        /// <summary>
        /// POST - запрос с сериализацией отсылаемого объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool SendToServer(object address, object data)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var url = (string) address;

                WebRequest request = WebRequest.Create(url); 
                request.Method = "POST";

                string jsonData = JsonConvert.SerializeObject(data);

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonData);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                Thread.Sleep(500);
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
    }
}