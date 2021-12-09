using System;

namespace SaveClipboard
{
    public class UI
    {
        public static void help()
        {
            Console.WriteLine();
            Console.WriteLine("SaveClipboard by iQuerz:");
            Console.WriteLine(" usage:");
            Console.WriteLine("     \"savecp {arguments}\"");
            Console.WriteLine("     each argument expects a input after it.");
            Console.WriteLine("     parenthesis indicate an optional parameter.");
            Console.WriteLine("     more info on https://www.github.com/iQuerz/SaveClipboard");
            Console.WriteLine(" arguments:");
            Console.WriteLine("     (-name) <filename>      Specifies file name.");
            Console.WriteLine("     -format <extension>     Specifies the extension");
            Console.WriteLine("     -h                      Shows this text. Can be specified anywhere in the command.");
            Console.WriteLine();
        }
        public static void ErrorMsg(Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine(" for help, type \"-h\".");
            Console.WriteLine();
        }
        public static void NoCP()
        {
            Console.WriteLine();
            Console.WriteLine("Hmm... Clipboard seems to be empty.");
            Console.WriteLine();
        }
    }
}
