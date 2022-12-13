using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiosko.gSirem.clases
{
    public class Valores
    {
        public decimal MontoAPagar
        {
            get;
            set;
        }

        public int MontoCambio
        {
            get;
            set;
        }

        public decimal MontoPagado
        {
            get;
            set;
        }

        public decimal MontoPendiente
        {
            get;
            set;
        }

        public Valores(decimal monto)
        {
            this.MontoAPagar = monto;
            this.MontoPagado = decimal.Zero;
            this.MontoPendiente = monto;
            this.MontoCambio = 0;
        }
    }
}
