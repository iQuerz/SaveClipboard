using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace SaveClipboard
{
    class SaveImage
    {
        //stuff
        private static Dictionary<ArgumentType, string> _arguments;
        private static string _fileName;
        private static string _extension;
        private static ImageFormat _format;

        public static void save(string[] args, Image image)
        {
            if (args.Count() == 0)
            {
                _fileName = getDefaultFileName();
                _format = ImageFormat.Png;
                _extension = "png";
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

                #region fileName, extension & format generation
                // generate extension and format
                if (!_arguments.ContainsKey(ArgumentType.format))
                {
                    _format = ImageFormat.Png;
                    _extension = "png";
                }
                else
                {
                    _format = validatePictureFormat(_arguments[ArgumentType.format]);
                    _extension = _arguments[ArgumentType.format].ToLower();
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
            image.Save($"{_fileName}.{_extension}", _format);
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
            while (File.Exists($"image{i++}.png")) { }
            return $"image{i}";
        }
        #endregion
    }
}
