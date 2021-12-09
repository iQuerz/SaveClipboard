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
                UI.help();
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
                    UI.NoCP();
                    return;
                }
            }
            catch (Exception ex)
            {
                UI.ErrorMsg(ex);
            }
        }
    }
}
