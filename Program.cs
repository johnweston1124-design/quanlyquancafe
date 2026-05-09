<<<<<<< HEAD
﻿using quanlyquancafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
=======
﻿using System;
>>>>>>> feature/product
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
<<<<<<< HEAD
            Application.Run(new frmLogin());
=======

            Application.Run(new frmCategory()); // form chính
>>>>>>> feature/product
        }
    }
}