using System;

namespace SaveClipboard
{
    public class UI
    {
        public static void HelpPanel()
        {
            Console.WriteLine();
            Console.WriteLine("SaveClipboard by iQuerz:");
            Console.WriteLine(" usage:");
            Console.WriteLine("     \"savecp {arguments}\"");
            Console.WriteLine("     text inside parenthesis can be omitted");
            Console.WriteLine("     more info on https://www.github.com/iQuerz/SaveClipboard");
            Console.WriteLine(" arguments:");
            Console.WriteLine("     (-name) <filename>      Specifies file name.");
            Console.WriteLine("     -format <extension>     Specifies the file extension.");
            Console.WriteLine("     -h, ?, help             Shows this text. Can be specified anywhere in the command.");
            Console.WriteLine(" examples of usage:");
            Console.WriteLine("     \"savecp app.jpg\"      Saves a picture named app.jpg inside working directory.");
            Console.WriteLine("     \"savecp -format png\"  Saves a picture named image.png inside working directory.");
            Console.WriteLine("     \"savecp C:\\app.gif\"  Saves a picture named app.gif inside the specified absolute path.");
            Console.WriteLine("     \"savecp pic\"          Saves a picture named pic.png inside the working directory.");
            Console.WriteLine("     \"savecp ?\"            Shows this text.");
            Console.WriteLine();
        }
        public static void ErrorMsg(Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"    Error: {ex.Message}");
            Console.WriteLine($"    for help, type \"savecp ?\".");
            Console.WriteLine();
        }
        public static void NoCP()
        {
            Console.WriteLine();
            Console.WriteLine(" Clipboard is empty or contains an invalid file format.");
            Console.WriteLine(" Send \"savecp ?\" or \"savecp help\" for help.");
            Console.WriteLine();
        }
        public static void SavedFileNotification(string file)
        {
            Console.WriteLine();
            Console.WriteLine($" File \"{file}\" has been saved.");
            Console.WriteLine();
        }
    }
}
