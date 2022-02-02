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
                if(args.Contains("-h") || args.Contains("?") || args.Contains("help"))
                UI.HelpPanel();
                return;
            }
            try
            {
                string fileName;
                if (Clipboard.ContainsImage())
                {
                    Image image = Clipboard.GetImage();
                    fileName = SaveImage.save(args, image);
                }
                else if (Clipboard.ContainsText())
                {
                    string text = Clipboard.GetText();
                    fileName = SaveText.save(args, text);
                }
                else
                {
                    UI.NoCP();
                    return;
                }
                UI.SavedFileNotification(fileName);
            }
            catch (Exception ex)
            {
                UI.ErrorMsg(ex);
            }
        }
    }
}
