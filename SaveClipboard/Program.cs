using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace SaveClipboard
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Count() > 0 && args.Contains("-h"))
            {
                help();
                return;
            }
            try
            {
                if (Clipboard.ContainsImage())
                {
                    Image image = Clipboard.GetImage();
                    SaveImage.save(args, image);
                }
                else if (Clipboard.ContainsText())
                {
                    string text = Clipboard.GetText();
                    SaveText.save(args, text);
                }
                else
                {
                    Console.WriteLine("Hmm... Clipboard seems to be empty.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(" for help, type \"-h\".");
            }
        }

        static void help()
        {
            Console.WriteLine("SaveClipboard by iQuerz:");
            Console.WriteLine(" list of available commands:");
            Console.WriteLine(" TBD!");
        }
    }
}
