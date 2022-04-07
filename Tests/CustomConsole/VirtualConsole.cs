using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomConsole
{
    public delegate object StringConverter(string str);

    public static class VirtualConsole
    {
        private static readonly List<string> _history = new List<string>(256);
        private static readonly List<string> _lines = new List<string>(256);
        private static readonly List<Function> _functions = new List<Function>(256);

        public static string Directory { get; private set; } = Environment.CurrentDirectory;

        public static void EnterText(string text)
        {
            text = text.Trim();

            // No value
            if (text.Length < 1) { return; }

            // Clear command
            if (text.Length >= 5 && text[0..5] == "clear")
            {
                string info = text[5..].Trim();

                if (info.Length == 0)
                {
                    _history.Clear();
                    _lines.Clear();
                    return;
                }

                if (info == "-c")
                {
                    _lines.Clear();

                    _history.Add(text);
                    return;
                }

                if (info == "-h")
                {
                    _history.Clear();
                    return;
                }

                if (info.Length < 4)
                {
                    Log("Invalid command parameters");
                    return;
                }

                if ((info[0] == '-' &&
                    info[^2] == '-') &&

                    (info[1] == 'c' &&
                    info[^1] == 'h') ||

                    (info[1] == 'h' &&
                    info[^1] == 'c'))
                {
                    if ((info.Length > 4) && (info[2..^3].Trim() != ""))
                    {
                        Log("Invalid command parameters");
                        return;
                    }

                    _history.Clear();
                    _lines.Clear();
                    return;
                }

                Log("Invalid command parameters");
                return;
            }
            // Close command
            if (text == "close")
            {
                Environment.Exit(0);
                return;
            }
            // History command
            if (text == "history")
            {
                if (_history.Count == 0)
                {
                    Log("No command history");
                    return;
                }

                for (int i = 0; i < _history.Count; i++)
                {
                    Log($"{i + 1}: {_history[i]}");
                }

                return;
            }
            // Directory command
            if (text == "directory")
            {
                Log(Directory);

                _history.Add(text);
                return;
            }
            // Log command
            if (text.Length >= 3 && text[0..3] == "log")
            {
                string info = text[3..].Trim();

                if (info.Length == 0)
                {
                    Log("Invalid parameter");
                    return;
                }

                Log(info);

                _history.Add(text);
                return;
            }
            // cd command
            if (text.Length >= 2 && text[0] == 'c' && text[1] == 'd')
            {
                string path = text[2..].Trim();

                if (path.Length == 0)
                {
                    Log("No path was provided");
                    return;
                }

                string pathFull = Path.Combine(Directory, path);

                if (System.IO.Directory.Exists(pathFull))
                {
                    Directory = pathFull;
                    return;
                }

                if (System.IO.Directory.Exists(path))
                {
                    Directory = path;
                    return;
                }

                Log("Path could not be found");
                return;
            }

            Function f = FindFunction(text, out int paramIndex);

            if (f == null)
            {
                Log("Unknown function");
                return;
            }

            object[] paramData = f.GetParams(text[paramIndex..], out int endIndex);

            if (paramData == null) { return; }

            f.Callback(paramData, text[(endIndex + paramIndex)..].Trim());

            _history.Add(text);
        }

        public static void AddFunction(string name, StringConverter[] paramConverters, FunctionPasser callcack)
        {
            _functions.Add(new Function(name, paramConverters, callcack));
        }
        public static void Log(string value)
        {
            _lines.Add(value);
        }
        public static void NewLine()
        {
            _lines.Add("");
        }

        public static List<string> Output => _lines;

        private static Function FindFunction(string text, out int paramIndex)
        {
            StringBuilder name = new StringBuilder(32);

            paramIndex = text.Length - 1;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (c == '\n') { return null; }

                if (c == ' ' || c == '(')
                {
                    paramIndex = i;
                    break;
                }

                name.Append(c);
            }

            string nameStr = name.ToString();

            return _functions.Find(f => f.Name == nameStr);
        }

        public static object IntParam(string value)
        {
            if (!int.TryParse(value, out int i))
            {
                Log("Invalid integer parameter");
                return null;
            }

            return i;
        }
        public static object FloatParam(string value)
        {
            if (!float.TryParse(value, out float f))
            {
                Log("Invalid float parameter");
                return null;
            }

            return f;
        }
        public static object DoubleParam(string value)
        {
            if (!double.TryParse(value, out double d))
            {
                Log("Invalid double parameter");
                return null;
            }

            return d;
        }
        public static object StringParam(string value)
        {
            if (value[0] != '\"' || value[^1] != '\"')
            {
                Log("Invalid string parameter");
                return null;
            }

            return value[1..^1];
        }
    }
}
