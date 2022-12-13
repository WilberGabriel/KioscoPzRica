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
    }
}
