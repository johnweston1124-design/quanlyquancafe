using System;
using System.Windows.Forms;

namespace quanlyquancafe.GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new frmCategory()); // form chính
        }
    }
}