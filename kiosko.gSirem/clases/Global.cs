using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiosko.gSirem
{
    static class Global
    {
        private static String _sqlCad = "";

        public static string SqlCad { get => _sqlCad; set => _sqlCad = value; }

        private static Multipago multipago = null;
        public static Multipago Multipago
        {
            get { return Global.multipago; }
            set { Global.multipago = value; }
        }

        private static ActaNacimientoBE acta = null;
        public static ActaNacimientoBE Acta
        {
            get { return Global.acta; }
            set { Global.acta = value; }
        }
    }
}
