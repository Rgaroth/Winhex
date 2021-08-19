using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winhex.Models;

namespace Winhex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private static void PostRequestAsync()
        {
            List<UserAction> actions = new List<UserAction>();
            actions.Add(new UserAction() { ActionDateTime = DateTime.Now, AppTitle = "Chrome", TextLog = "heelo!" });
            var log = new UserLog() { CompName = "MyComp", SendingDateTime = DateTime.Now };
            log.Logs.Add(actions[0]);



            WebRequest request = WebRequest.Create("https://localhost:44373/upload");
            request.Method = "POST"; // для отправки используется метод Post
            // данные для отправки
            string data = JsonConvert.SerializeObject(log);
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/json";
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;

            //записываем данные в поток запроса
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PostRequestAsync();
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message);
            }
        }
    }
}
