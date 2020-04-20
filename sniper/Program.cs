using System;
using System.Windows.Forms;

namespace GoosSniper.Sniper
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args[0], args[1], args[2], args[3]));
        }
    }
}