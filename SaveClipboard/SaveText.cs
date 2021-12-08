using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SaveClipboard
{
    public class SaveText
    {
        //stuff
        private static Dictionary<ArgumentType, string> _arguments;
        private static string _fileName;
        private static string _extension;

        public static void save(string[] args, string text)
        {
            if (args.Count() == 0)
            {
                _fileName = getDefaultFileName();
                _extension = "txt";
            }
            else
            {
                #region arguments Dictionary generation
                _arguments = new Dictionary<ArgumentType, string>();
                for (int i = 0; i < args.Count(); i++)
                {
                    if (args[i][0] == '-')
                    {
                        ArgumentType argument = (ArgumentType)Enum.Parse(typeof(ArgumentType), args[i].Substring(1).ToLower());
                        if (args.Count() == i + 1)
                            throw new Exception($"\'{argument}\' parameter expects data.");
                        string line = args[i++ + 1];
                        _arguments.Add(argument, line);
                    }
                    else
                    {
                        ArgumentType argument = ArgumentType.fileName;
                        string line = args[i];
                        _arguments.Add(argument, line);
                    }

                }
                #endregion

                #region fileName & extension generation
                // generate extension
                if (!_arguments.ContainsKey(ArgumentType.format))
                {
                    _extension = "txt";
                }
                else
                {
                    _extension = validateTextFormat(_arguments[ArgumentType.format]);
                }

                // generate file name
                if (!_arguments.ContainsKey(ArgumentType.fileName))
                {
                    _fileName = getDefaultFileName();
                }
                else
                {
                    _fileName = validateFileName(_arguments[ArgumentType.fileName]);
                }
                #endregion
            }

            // save the file
            File.WriteAllText($"{_fileName}.{_extension}", text);
        }

        #region Validations
        /// <summary>
        /// Validates text format of <paramref name="input"/> string.
        /// </summary>
        /// <param name="input">String to be checked.</param>
        /// <returns>lowercase version of <paramref name="input"/> if it passes the validation check.</returns>
        private static string validateTextFormat(string input)
        {
            input = input.ToLower();
            string[] validFormats =
            {
                // plain text:
                "txt", "rtf", "md", "readme", "log",
                
                // programming languages:
                "cs", "c", "cpp", "h", "py", "js", "asm", "java", "php",

                // non-programming languages:
                "html", "css",

                // scripting:
                "bat", "ahk", "dockerfile", "ps1", 

                // data/config:
                "json", "yaml", "xml", "xaml"
            };
            if (!validFormats.Contains(input))
                throw new Exception("invalid text format");
            return input;
        }

        /// <summary>
        /// Validates the file name inside <paramref name="input"/> string.
        /// </summary>
        /// <param name="input">string to be checked.</param>
        /// <returns>Trimmed version of <paramref name="input"/> string, without extension at the end.</returns>
        private static string validateFileName(string input)
        {
            if (!input.EndsWith($".{_extension}"))
            {
                int i = input.LastIndexOf('.');
                if (i == -1)
                    return input;
                input = input.Remove(i);
            }
            return input;
        }
        #endregion

        #region Other
        private static string getDefaultFileName()
        {
            int i = 0;
            while (File.Exists($"text{i++}.txt")) { }
            return $"text{i}";
        }
        #endregion
    }
}