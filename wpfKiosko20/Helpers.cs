using System;
using System.Collections.Generic;
using System.Text;

namespace wpfKiosko20
{
   public static class Helpers
    {
        public static int[] ObtenerDenominaciones(int monto)
        {
            List<int> valores = new List<int>();
            int[] denominaciones = new int[] { 50, 20, 5, 1 };
            int resto = monto;
            for (int i = 0; i < (int)denominaciones.Length; i++)
            {
                int valor = resto / denominaciones[i];
                resto = resto - valor * denominaciones[i];
                valores.Add(valor);
            }
            return valores.ToArray();
        }

        public static int Redondea(decimal monto)
        {
            decimal num = Math.Truncate(monto);
            return int.Parse(num.ToString());
        }
    }
}
