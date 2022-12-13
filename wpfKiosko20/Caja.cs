using System;
using System.Collections.Generic;
using System.Text;

namespace wpfKiosko20
{
  public  class Caja
    {
        private Valores valores;

        private Dictionary<string, Denominacion> denominaciones;

        public Caja(decimal monto)
        {
            this.valores = new Valores(monto);
            this.denominaciones = new Dictionary<string, Denominacion>();
        }

        private void AcumulaDenominaciones(string tipo, string eos, decimal valor, int cantidad)
        {
            Denominacion denominacion;
            string key = string.Format("{0}-{1}-{2}", eos, tipo, valor);
            if (this.denominaciones.TryGetValue(key, out denominacion))
            {
                Denominacion denominacion1 = denominacion;
                denominacion1.Cantidad = denominacion1.Cantidad + cantidad;
                return;
            }
            denominacion = new Denominacion()
            {
                Tipo = tipo,
                EoS = eos,
                Valor = valor,
                Cantidad = cantidad
            };
            this.denominaciones.Add(key, denominacion);
        }

        public void AcumulaDineroEntregado(string tipo, decimal valor, int cantidad)
        {
            this.AcumulaDenominaciones(tipo, "S", valor, cantidad);
        }

        public Valores AcumularDineroRecibido(string tipo, decimal valor)
        {
            this.AcumulaDenominaciones(tipo, "E", valor, 1);
            Valores montoPagado = this.valores;
            montoPagado.MontoPagado = montoPagado.MontoPagado + valor;
            if (this.valores.MontoPagado > this.valores.MontoAPagar)
            {
                this.valores.MontoCambio = Helpers.Redondea(this.valores.MontoPagado - this.valores.MontoAPagar);
                this.valores.MontoPendiente = decimal.Zero;
            }
            else if (this.valores.MontoPagado >= this.valores.MontoAPagar)
            {
                this.valores.MontoPendiente = decimal.Zero;
                this.valores.MontoCambio = 0;
            }
            else
            {
                this.valores.MontoCambio = 0;
                this.valores.MontoPendiente = this.valores.MontoAPagar - this.valores.MontoPagado;
            }
            return this.valores;
        }

        public string GeneraCadenaDenominaciones()
        {
            string cadena = "";
            foreach (KeyValuePair<string, Denominacion> denominacion in this.denominaciones)
            {
                cadena = string.Concat(cadena, string.Format("{0}-{1}-{2}-{3}|", new object[] { denominacion.Value.EoS, denominacion.Value.Cantidad, denominacion.Value.Tipo, denominacion.Value.Valor }));
            }
            if (cadena.Length <= 0)
            {
                return string.Empty;
            }
            return cadena.Substring(0, cadena.Length - 1);
        }

        public void LimpiaCaja()
        {
            this.valores = null;
            this.denominaciones.Clear();
            this.denominaciones = null;
        }
    }
}
