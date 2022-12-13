using IronPdf;
using kiosko.gSirem.clases;
using SIA.Kiosko.CCTalk;
using SIA.Kiosko.Envoy;
using SIA.Kiosko.Mpost;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace kiosko.gSirem
{
    public partial class KioskoHome : Form
    {
        private Caja caja;
        private DispensadorBilletes dispensadorBilletes;
        private ReceptorBilletes receptorBilletes;
        private Valores valores;
        private Monedero monedero = new Monedero();

        private string puertoMonedas;
        private byte dispositivoReceptorMonedas;
        private byte dispositivoDispensadorMonedas1Peso;
        private byte dispositivoDispensadorMonedas5Pesos;
        private string puertoReceptorBilletes;

        private string login = "AdminKiosk";
        private string domain = "DESKTOP-UQ0F7IF";
        private string password = "Sollertia";

        private bool conectadoMonedas;


        private decimal montoPago;
        private string transaccion = "";
        private string archivoImagenRecibo = "";

        public KioskoHome()
        {
            InitializeComponent();

            //SiaKioskoHub.ClienteConectado += new ClienteConexionEventHandler(this.SiaKioskoBridge_ClienteConectado);
            //SiaKioskoHub.ClienteDesconectado += new ClienteConexionEventHandler(this.SiaKioskoBridge_ClienteDesconectado);
            //SiaKioskoHub.ClienteListoParaRecibirDinero += new ClienteListoParaRecibirDineroEventHandler(this.SiaKioskoBridge_ClienteListoParaRecibirDinero);
            //SiaKioskoHub.ClienteListoParaRecibirDineroTarjeta += new ClienteListoParaRecibirDineroTarjetaEventHandler(this.SiaKioskoBridge_ClienteListoParaRecibirDineroTarjeta);
            //SiaKioskoHub.ClienteImprimirFacturas += new ClienteImprimirFacturasEventHandler(this.SiaKioskoBridge_ClienteImprimirFacturas);
            //SiaKioskoHub.ClienteCancelarPagar += new ClienteCancelarPagarEventHandler(this.SiaKioskoBridge_CancelarPagar);
            //SiaKioskoHub.ClienteImprimirFacturasYActa += new ClienteImprimirFacturasYActaEventHandler(this.SiaKioskoBridge_ClienteImprimirFacturasYActa);

            ReceptorBilletes.OnConnected += new OnConnectedEventHandler(this.OnReceptorConnected);
            ReceptorBilletes.OnEscrowed += new OnEscrowedEventHandler(this.OnReceptorEscrowed);
            ReceptorBilletes.OnRejected += new OnRejectedEventHandler(this.OnReceptorRejected);
            ReceptorBilletes.OnReturned += new OnReturnedEventHandler(this.OnReceptorReturned);
            ReceptorBilletes.OnStacked += new OnStackedEventHandler(this.OnReceptorStacked);

            ReceptorBilletes.OnMessage += new SIA.Kiosko.Mpost.OnMessageEventHandler(this.Log);
            DispensadorBilletes.OnMessage += new SIA.Kiosko.Envoy.OnMessageEventHandler(this.Log);
        }

        private void KioskoHome_Load(object sender, EventArgs e)
        {
            //lblLeyenda.Content = wpfKiosko20.Properties.Settings.Default.PuertoMonedas.ToString(); ;

            //this.HubStartup();
            //this.ConfiguraControles();
            Control.CheckForIllegalCrossThreadCalls = false;
            txtMonto.Text = "15";
           
            this.puertoMonedas =  ConfigurationManager.AppSettings["Puerto-Monedas"];
            this.dispositivoReceptorMonedas = byte.Parse(ConfigurationManager.AppSettings["Dispositivo-ReceptorMonedas"]);
            this.dispositivoDispensadorMonedas1Peso = byte.Parse(ConfigurationManager.AppSettings["Dispositivo-DispensadorMonedas1Peso"]);
            this.dispositivoDispensadorMonedas5Pesos = byte.Parse(ConfigurationManager.AppSettings["Dispositivo-DispensadorMonedas5Pesos"]);
            this.puertoReceptorBilletes = ConfigurationManager.AppSettings["Puerto-ReceptorBilletes"];

            this.Log(string.Concat("[PuertoMonedas] Puerto : ", this.puertoMonedas));
            this.Log(string.Format("[ReceptorMonedas] Dispositivo : {0}", this.dispositivoReceptorMonedas));
            this.Log(string.Format("[DispensadorMonedas1Peso] Dispositivo : {0}", this.dispositivoDispensadorMonedas1Peso));
            this.Log(string.Format("[DispensadorMonedas5Pesos] Dispositivo : {0}", this.dispositivoDispensadorMonedas5Pesos));
            this.Log(string.Concat("[ReceptorBilletes] Puerto : ", this.puertoReceptorBilletes));
            this.EjecutarReinicioMecanico();
            this.monedero.MonedaAceptada += new EventHandler<CoinAcceptorCoinEventArgs>(this._coinAcceptor_CoinAccepted);
            this.monedero.ErrorAceptandoMoneda += new EventHandler<CoinAcceptorErrorEventArgs>(this._coinAcceptor_ErrorMessageAccepted);
            this.monedero.Conectar(this.puertoMonedas, this.dispositivoReceptorMonedas);
            this.Log("Monedero Conectado");
            this.receptorBilletes = new ReceptorBilletes();
            this.dispensadorBilletes = new DispensadorBilletes();
            if (!this.dispensadorBilletes.Inicializar())
            {
                this.Log("No se pudo inicializar el Dispensador de Billetes");
                Application.Exit();
            }
            else
            {
                if (!this.receptorBilletes.Inicializar(this.puertoReceptorBilletes))
                {
                    this.Log("No se pudo inicializar el Receptor de Billetes");
                    this.dispensadorBilletes.Dispose();
                    Application.Exit();
                    return;
                }
                //try
                //{
                //    this.InicializaPinPad();
                //}
                //catch (Exception exception)
                //{
                //    Exception ex = exception;
                //    this.Log(string.Concat("Error: ", ex.Message, "; StackTrace: ", ex.StackTrace));
                //}
            }

        }

        #region Eventos de pantalla ....

      

        //private void BtnHabilita_Click(object sender, RoutedEventArgs e)
        //{
        //    int _monto = Convert.ToInt32(txtMonto.Text);
        //    ClienteListoParaRecibirDinero("01", _monto);
        //    lblLeyenda.Content = txtMonto.Text.ToString();
        //}

        //private void BtnCobrar_Click(object sender, RoutedEventArgs e)
        //{
        //    int _monto = Convert.ToInt32(txtMonto.Text);
        //    string tipo = "B";
        //    int valor = _monto;
        //    //string text = button.Text;
        //    //switch (text)
        //    //{
        //    //    case "$20.00":
        //    //        {
        //    //            tipo = "B";
        //    //            valor = 20;
        //    //            break;
        //    //        }
        //    //    case "$50.00":
        //    //        {
        //    //            tipo = "B";
        //    //            valor = 50;
        //    //            break;
        //    //        }
        //    //    case "$100.00":
        //    //        {
        //    //            tipo = "B";
        //    //            valor = 100;
        //    //            break;
        //    //        }
        //    //    case "$200.00":
        //    //        {
        //    //            tipo = "B";
        //    //            valor = 200;
        //    //            break;
        //    //        }
        //    //    case "$500.00":
        //    //        {
        //    //            tipo = "B";
        //    //            valor = 500;
        //    //            break;
        //    //        }
        //    //    case "$1.00":
        //    //        {
        //    //            tipo = "M";
        //    //            valor = 1;
        //    //            break;
        //    //        }
        //    //    case "$5.00":
        //    //        {
        //    //            tipo = "M";
        //    //            valor = 5;
        //    //            break;
        //    //        }
        //    //    case "$10.00":
        //    //        {
        //    //            tipo = "M";
        //    //            valor = 10;
        //    //            break;
        //    //        }
        //    //    case "$1000.00":
        //    //        {
        //    //            tipo = "B";
        //    //            valor = 1000;
        //    //            break;
        //    //        }
        //    //}
        //    this.InformaDineroRecibido(tipo, valor);
        //}

        //private void RadButton_Click(object sender, RoutedEventArgs e)
        //{
        //    CancelarPagar();
        //}

        #endregion

        #region Métodos app

       

        #endregion

        #region Métodos del kiosko

        private void InformaDineroRecibido(string tipo, decimal valor)
        {
            this.valores = this.caja.AcumularDineroRecibido(tipo, valor);
            this.Log(string.Format("Pagado:{0}, Pendiente:{1}, cambio:{2}", this.valores.MontoPagado, this.valores.MontoPendiente, this.valores.MontoCambio));
            //IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<SiaKioskoHub>();
            //((dynamic)hubContext.Clients.All).dineroRecibido(this.valores);

            if (valores.MontoPendiente>0)
                lblLeyenda.Text = $"Aun le faltan $ {valores.MontoPendiente} para completar la transacción";

            if (this.valores.MontoPendiente == decimal.Zero)
            {
                lblLeyenda.Text = $"Pagado {this.valores.MontoPagado} - Pendiente {this.valores.MontoPendiente} - Cambio {this.valores.MontoCambio}";
                //this.CambiaColorControles(Color.Red, Color.White);
                this.Log("Inabilitando Receptor de Billetes");
                this.receptorBilletes.DeshabilitarReceptor();
                this.Log("Inabilitando Receptor de Monedas");
                this.monedero.EndPoll();
                Thread.Sleep(500);
                this.conectadoMonedas = false;
                Thread.Sleep(1000);
                if (this.valores.MontoCambio > 0)
                {
                    this.Log("Retornando cambio");
                    int[] denominaciones = Helpers.ObtenerDenominaciones(this.valores.MontoCambio);
                    if (denominaciones[0] > 0 || denominaciones[1] > 0)
                    {
                        this.dispensadorBilletes.Dispensar(denominaciones[1], denominaciones[0]);
                        if (denominaciones[0] > 0)
                        {
                            this.caja.AcumulaDineroEntregado("B", new decimal(50), denominaciones[0]);
                        }
                        if (denominaciones[1] > 0)
                        {
                            this.caja.AcumulaDineroEntregado("B", new decimal(20), denominaciones[1]);
                        }
                    }
                    Thread.Sleep(1000);
                    if (denominaciones[2] > 0)
                    {
                        this.Log(string.Format("Monedas de $5 dispensadas: {0}", denominaciones[2]));
                        if (this.monedero.RetornarMonedas(this.dispositivoDispensadorMonedas5Pesos, (byte)denominaciones[2]))
                        {
                            Application.Exit();
                        }
                        this.caja.AcumulaDineroEntregado("M", new decimal(5), denominaciones[2]);
                    }
                    if (denominaciones[3] > 0)
                    {
                        this.Log(string.Format("Monedas de $1 dispensadas: {0}", denominaciones[3]));
                        if (this.monedero.RetornarMonedas(this.dispositivoDispensadorMonedas1Peso, (byte)denominaciones[3]))
                        {
                            Application.Exit();
                        }
                        this.caja.AcumulaDineroEntregado("M", decimal.One, denominaciones[3]);
                    }
                }
                string sdenominaciones = this.caja.GeneraCadenaDenominaciones();
                ImprimirTicket(this.valores);
                this.caja.LimpiaCaja();
                this.caja = null;
                this.valores = null;
                //((dynamic)hubContext.Clients.All).generaFolio(sdenominaciones);
                this.Log($"[Denominaciones] {sdenominaciones.ToString()}");
                this.Log("[Transacción Completada]");
                this.Log("");
            }
        }

        private void ImprimirTicket(Valores val)
        {
            Guid guid = Guid.NewGuid();
            string recibo = "";
            recibo = File.ReadAllText(string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "\\recibo.html"));
            recibo = recibo.Replace("{MONTO_TOTAL}", $"$ {val.MontoAPagar:#,#0.00}");
            recibo = recibo.Replace("{MONTO_CAMBIO}", $"$ {val.MontoCambio:#,#0.00}");
            recibo = recibo.Replace("{FOLIO}", guid.ToString());
            recibo = recibo.Replace("{IMPORTE}", $"$ {val.MontoAPagar:#,#0.00}");
            recibo = recibo.Replace("{SERVICIO}", "SERVICIO DE PRUEBA....");
           
            recibo = recibo.Replace("{FECHA}", $"{DateTime.Now:dd/MM/yyyy}");
            recibo = recibo.Replace("{HORA}", $"{DateTime.Now:hh:mm:ss}");

            //this.Log(string.Concat("Recibo ", recibo.ToString()));

            try
            {

          

            PdfDocument pdfDocument = (new HtmlToPdf()).RenderHtmlAsPdf(recibo, (Uri)null, null);
            string rutaPDFs = ConfigurationManager.AppSettings["RutaPDFs"];
          
            this.archivoImagenRecibo = string.Concat(rutaPDFs, guid.ToString(), ".jpg");
            this.archivoImagenRecibo = pdfDocument.ToJpegImages(this.archivoImagenRecibo)[0];
            this.Log(string.Concat("Imprimiendo recibo ", this.archivoImagenRecibo));
            string impresoraRecibos = ConfigurationManager.AppSettings["ImpresoraParaRecibos"];
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = impresoraRecibos;
            printDocument.DefaultPageSettings.Landscape = false;
            printDocument.PrintPage += new PrintPageEventHandler(this.PrintPageRecibo);
            printDocument.Print();
            }
            catch (Exception exception)
            {
                Exception ex = exception;
                this.Log(string.Concat("Error: ", ex.Message, "; StackTrace: ", ex.StackTrace));

            }
        }


        private void PrintPageRecibo(object o, PrintPageEventArgs e)
        {
            Image img = Image.FromFile(this.archivoImagenRecibo);
            e.Graphics.DrawImage(img, 10, 10, 500, 550);
        }


        private void CancelarPagar()
        {
            bool montoPagado;
            string sdenominaciones = "";
            this.Log("[Cancelando Transacción]");
            //this.CambiaColorControles(Color.Red, Color.White);
            this.Log("Inabilitando Receptor de Billetes");
            this.receptorBilletes.DeshabilitarReceptor();
            this.Log("Inabilitando Receptor de Monedas");
            this.monedero.EndPoll();
            this.conectadoMonedas = false;
            Thread.Sleep(1000);
            Valores valore = this.valores;
            if (valore != null)
            {
                montoPagado = valore.MontoPagado > new decimal();
            }
            else
            {
                montoPagado = false;
            }
            if (montoPagado)
            {
                int[] denominaciones = Helpers.ObtenerDenominaciones(Helpers.Redondea(this.valores.MontoPagado));
                if (denominaciones[0] > 0 || denominaciones[1] > 0)
                {
                    this.dispensadorBilletes.Dispensar(denominaciones[1], denominaciones[0]);
                    if (denominaciones[0] > 0)
                    {
                        this.caja.AcumulaDineroEntregado("B", new decimal(50), denominaciones[0]);
                    }
                    if (denominaciones[1] > 0)
                    {
                        this.caja.AcumulaDineroEntregado("B", new decimal(20), denominaciones[1]);
                    }
                }
                Thread.Sleep(1000);
                if (denominaciones[2] > 0)
                {
                    this.Log(string.Format("Monedas de $5 dispensadas: {0}", denominaciones[2]));
                    this.monedero.RetornarMonedas(this.dispositivoDispensadorMonedas5Pesos, (byte)denominaciones[2]);
                    this.caja.AcumulaDineroEntregado("M", new decimal(5), denominaciones[2]);
                }
                Thread.Sleep(1000);
                if (denominaciones[3] > 0)
                {
                    this.Log(string.Format("Monedas de $1 dispensadas: {0}", denominaciones[3]));
                    this.monedero.RetornarMonedas(this.dispositivoDispensadorMonedas1Peso, (byte)denominaciones[3]);
                    this.caja.AcumulaDineroEntregado("M", decimal.One, denominaciones[3]);
                }
                this.valores.MontoPagado = decimal.Zero;
            }
            sdenominaciones = this.caja.GeneraCadenaDenominaciones();
            this.caja.LimpiaCaja();
            this.caja = null;
            this.valores = null;
            //((dynamic)GlobalHost.ConnectionManager.GetHubContext<SiaKioskoHub>().Clients.All).confirmarCancelarPagar(sdenominaciones);
            this.Log("[Transacción Cancelada]");
            this.Log("");
        }

        private void ClienteListoParaRecibirDinero(string operacion, int monto)
        {
            this.transaccion = operacion;
            if (this.conectadoMonedas)
            {
                this.Log("Inabilitando Receptor de Billetes");
                this.receptorBilletes.DeshabilitarReceptor();
                this.Log("Inabilitando Receptor de Monedas");
                this.monedero.EndPoll();
            }
            this.conectadoMonedas = false;
            this.valores = null;
            this.Log("");
            this.Log("[Transacción Iniciada]");
            this.caja = new Caja(monto);
            //this.CambiaColorControles(Color.Green, Color.White);
            this.Log("Habilitando Receptor de Billetes");
            this.receptorBilletes.HabilitarReceptor();
            this.Log("Habilitando Receptor de Monedas");
            this.monedero.StartPoll();
        }

        private void EjecutarReinicioMecanico()
        {
            this.dispensadorBilletes = new DispensadorBilletes();
            if (this.dispensadorBilletes.Inicializar())
            {
                this.dispensadorBilletes.EjecutaReinicioMecanico();
            }

        }

        private void OnReceptorConnected()
        {
            this.Log("OnReceptorConnected");
            this.Log("Inabilitando Receptor de Billetes");
            this.receptorBilletes.DeshabilitarReceptor();
        }

        private void OnReceptorEscrowed()
        {
            this.Log("OnReceptorEscrowed");
        }

        private void OnReceptorRejected()
        {
            this.Log("OnReceptorRejected");
        }

        private void OnReceptorReturned()
        {
            this.Log("OnReceptorReturned");
        }

        private void OnReceptorStacked(int value)
        {
            this.Log(string.Format("Billete recibido: {0}", value));
            this.InformaDineroRecibido("B", value);
        }

        private void _coinAcceptor_CoinAccepted(object sender, CoinAcceptorCoinEventArgs e)
        {
            if (!base.InvokeRequired)
            {
                this.Log(string.Format("Moneda recibida: {0}", e.CoinValue));
                this.InformaDineroRecibido("M", e.CoinValue);
                return;
            }
            base.Invoke(new EventHandler<CoinAcceptorCoinEventArgs>(this._coinAcceptor_CoinAccepted), new object[] { sender, e });
        }

        private void _coinAcceptor_ErrorMessageAccepted(object sender, CoinAcceptorErrorEventArgs e)
        {
            if (!base.InvokeRequired)
            {
                this.Log(string.Format("Coin acceptor error: {0} ({1}, {2:X2})", e.ErrorMessage, e.Error, (byte)e.Error));
                return;
            }
            base.Invoke(new EventHandler<CoinAcceptorErrorEventArgs>(this._coinAcceptor_ErrorMessageAccepted), new object[] { sender, e });
        }

        private void Log(string msg)
        {
            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\";
            DateTime now = DateTime.Now;
            string fileName = string.Concat("gKiosko-", now.ToString("dd-MM-yyyy"), ".txt");
            string[] str = new string[] { "[Sirem] ", null, null, null, null, null };
            now = DateTime.Now;
            str[1] = now.ToString("HH:mm:ss.fff");
            str[2] = " ";
            str[3] = msg;
            str[4] = " ";
            str[5] = Environment.NewLine;
            string msg2 = string.Concat(str);
            File.AppendAllText(string.Concat(path, fileName), msg2);
        }

        #endregion

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            int _monto = Convert.ToInt32(txtMonto.Text);
            ClienteListoParaRecibirDinero("01", _monto);
            lblLeyenda.Text = txtMonto.Text.ToString();
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
