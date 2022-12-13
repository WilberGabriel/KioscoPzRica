using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace kiosko.gSirem
{


    internal static class Program
    {
        
            private static Mutex mutex;

            /// <summary>
            /// Punto de entrada principal para la aplicación.
            /// </summary>
            [STAThread]
            private static void Main(string[] args)
            {
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new frmPrincipal());

                bool createdNew = false;
                Program.mutex = new Mutex(true, "kiosko.gSirem", out createdNew);
                if (!createdNew)
                {
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                frmPrincipal frm = new frmPrincipal();
                if (args.Length != 0 && args[0].ToUpper() == "RESIZE")
                {
                    frm.FormBorderStyle = FormBorderStyle.Sizable;
                }
                Application.Run(frm);
            }
      
    }
}
