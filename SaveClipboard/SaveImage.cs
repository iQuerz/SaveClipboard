using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Drawing;
using System.Drawing.Imaging;

namespace SaveClipboard
{
    class SaveImage
    {
        //stuff
        private static Dictionary<ArgumentType, string> _arguments;
        private static string _fileName;
        private static string _extension;
        private static ImageFormat _format;


        /// <summary>
        /// Generates filename from arguments and saves the image.
        /// </summary>
        /// <param name="args">Arguments from the executable call.</param>
        /// <param name="image">Image from clipboard to be saved.</param>
        /// <returns>Saved filename.</returns>
        public static string save(string[] args, Image image)
        {
            if (args.Count() == 0)
            {
                _fileName = getDefaultFileName();
                _extension = "png";
                _format = ImageFormat.Png;
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
                        ArgumentType argument = ArgumentType.name;
                        string line = args[i];
                        _arguments.Add(argument, line);
                    }
                }
                #endregion

                #region fileName, extension & format generation
                // generate extension and format
                if (!_arguments.ContainsKey(ArgumentType.format))
                {
                    _extension = "png";
                    _format = ImageFormat.Png;

                    // this allows the user to call "savecp img.gif"
                    if (_arguments.ContainsKey(ArgumentType.name) && _arguments[ArgumentType.name].Contains("."))
                    {
                        _extension = getExtensionFromName(_arguments[ArgumentType.name]);
                        _format = validatePictureFormat(_extension);
                    }
                }
                else
                {
                    _extension = _arguments[ArgumentType.format].ToLower();
                    _format = validatePictureFormat(_extension);
                }

                // generate file name
                if (!_arguments.ContainsKey(ArgumentType.name))
                {
                    _fileName = getDefaultFileName();
                }
                else
                {
                    _fileName = validateFileName(_arguments[ArgumentType.name]);
                }
                #endregion
            }

            // save the file and notify the console.
            image.Save($"{_fileName}.{_extension}", _format);
            return $"{_fileName}.{_extension}";
        }


        #region Validations

        /// <summary>
        /// Validates text format of <paramref name="input"/> string.
        /// </summary>
        /// <param name="input">String to be checked.</param>
        /// <returns>lowercase version of <paramref name="input"/> if it passes the validation check.</returns>
        private static ImageFormat validatePictureFormat(string input)
        {
            ImageFormat format;
            input = input.ToLower();
            switch (input)
            {
                case "jpg":
                    format = ImageFormat.Jpeg;
                    break;
                case "jpeg":
                    format = ImageFormat.Jpeg;
                    break;
                case "png":
                    format = ImageFormat.Png;
                    break;
                case "gif":
                    format = ImageFormat.Gif;
                    break;
                case "tiff":
                    format = ImageFormat.Tiff;
                    break;
                case "bmp":
                    format = ImageFormat.Bmp;
                    break;
                case "ico":
                    format = ImageFormat.Icon;
                    break;
                case "wmf":
                    format = ImageFormat.Wmf;
                    break;
                case "emf":
                    format = ImageFormat.Emf;
                    break;
                case "exif":
                    format = ImageFormat.Exif;
                    break;
                default:
                    throw new Exception("Invalid image format.");
            }
            return format;
        }


        /// <summary>
        /// Validates the file name inside <paramref name="input"/> string.
        /// </summary>
        /// <param name="input">string to be checked.</param>
        /// <returns>Trimmed version of <paramref name="input"/> string, without extension at the end.</returns>
        private static string validateFileName(string input)
        {
            int i = input.LastIndexOf('.');
            if (i == -1)
                return input;
            input = input.Remove(i);
            return input;
        }

        #endregion

        #region Other

        private static string getDefaultFileName()
        {
            int i = 0;
            while (File.Exists($"image{i++}.png")) { }
            return $"image{i}";
        }

        private static string getExtensionFromName(string name)
        {
            string res = "";
            for (int i = name.LastIndexOf('.') + 1; i < name.Length; i++)
            {
                res += name[i];
            }
            return res;
        }

        #endregion
    }
}
