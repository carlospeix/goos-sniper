using System;
using System.Windows.Forms;

namespace GoosSniper.Sniper
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm("localhost", "sniper", "sniper", "item-54321"));
        }
    }
}
