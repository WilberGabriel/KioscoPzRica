using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Configuration;
using System.Collections.Specialized;
using SIA.Kiosko.CCTalk;
using SIA.Kiosko.Envoy;
using SIA.Kiosko.Mpost;
using IronPdf;
using System.Text.RegularExpressions;



namespace wpfKiosko20
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        public MainWindow()
        {
            InitializeComponent();

            //SiaKioskoHub.ClienteConectado += new ClienteConexionEventHandler(this.SiaKioskoBridge_ClienteConectado);
            //SiaKioskoHub.ClienteDesconectado += new ClienteConexionEventHandler(this.SiaKioskoBridge_ClienteDesconectado);
            //SiaKioskoHub.ClienteListoParaRecibirDinero += new ClienteListoParaRecibirDineroEventHandler(this.SiaKioskoBridge_ClienteListoParaRecibirDinero);
            //SiaKioskoHub.ClienteListoParaRecibirDineroTarjeta += new ClienteListoParaRecibirDineroTarjetaEventHandler(this.SiaKioskoBridge_ClienteListoParaRecibirDineroTarjeta);
            //SiaKioskoHub.ClienteImprimirFacturas += new ClienteImprimirFacturasEventHandler(this.SiaKioskoBridge_ClienteImprimirFacturas);
            //SiaKioskoHub.ClienteCancelarPagar += new ClienteCancelarPagarEventHandler(this.SiaKioskoBridge_CancelarPagar);
            //SiaKioskoHub.ClienteImprimirFacturasYActa += new ClienteImprimirFacturasYActaEventHandler(this.SiaKioskoBridge_ClienteImprimirFacturasYActa);

            //ReceptorBilletes.OnConnected += new OnConnectedEventHandler(this.OnReceptorConnected);
            //ReceptorBilletes.OnEscrowed += new OnEscrowedEventHandler(this.OnReceptorEscrowed);
            //ReceptorBilletes.OnRejected += new OnRejectedEventHandler(this.OnReceptorRejected);
            //ReceptorBilletes.OnReturned += new OnReturnedEventHandler(this.OnReceptorReturned);
            //ReceptorBilletes.OnStacked += new OnStackedEventHandler(this.OnReceptorStacked);

         
            //ReceptorBilletes.OnMessage += new SIA.Kiosko.Mpost.OnMessageEventHandler(this.Log);
            //DispensadorBilletes.OnMessage += new SIA.Kiosko.Envoy.OnMessageEventHandler(this.Log);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            //lblLeyenda.Content = wpfKiosko20.Properties.Settings.Default.PuertoMonedas.ToString(); ;

            //this.HubStartup();
            //this.ConfiguraControles();
            //Control.CheckForIllegalCrossThreadCalls = false;
            txtMonto.Text = "40";
            //this.puertoMonedas = wpfKiosko20.Properties.Settings.Default.PuertoMonedas.ToString();
            //this.dispositivoReceptorMonedas = byte.Parse(wpfKiosko20.Properties.Settings.Default.DispositivoReceptorMonedas.ToString());
            //this.dispositivoDispensadorMonedas1Peso = byte.Parse(wpfKiosko20.Properties.Settings.Default.DispositivoDispensadorMonedas1Peso.ToString());
            //this.dispositivoDispensadorMonedas5Pesos = byte.Parse(wpfKiosko20.Properties.Settings.Default.DispositivoDispensadorMonedas5Pesos.ToString());
            //this.puertoReceptorBilletes = wpfKiosko20.Properties.Settings.Default.PuertoReceptorBilletes.ToString();
            //this.Log(string.Concat("[PuertoMonedas] Puerto : ", this.puertoMonedas));
            //this.Log(string.Format("[ReceptorMonedas] Dispositivo : {0}", this.dispositivoReceptorMonedas));
            //this.Log(string.Format("[DispensadorMonedas1Peso] Dispositivo : {0}", this.dispositivoDispensadorMonedas1Peso));
            //this.Log(string.Format("[DispensadorMonedas5Pesos] Dispositivo : {0}", this.dispositivoDispensadorMonedas5Pesos));
            //this.Log(string.Concat("[ReceptorBilletes] Puerto : ", this.puertoReceptorBilletes));
            //this.EjecutarReinicioMecanico();
            //this.monedero.MonedaAceptada += new EventHandler<CoinAcceptorCoinEventArgs>(this._coinAcceptor_CoinAccepted);
            //this.monedero.ErrorAceptandoMoneda += new EventHandler<CoinAcceptorErrorEventArgs>(this._coinAcceptor_ErrorMessageAccepted);
            //this.monedero.Conectar(this.puertoMonedas, this.dispositivoReceptorMonedas);
            //this.Log("Monedero Conectado");
            //this.receptorBilletes = new ReceptorBilletes();
            //this.dispensadorBilletes = new DispensadorBilletes();
            //if (!this.dispensadorBilletes.Inicializar())
            //{
            //    this.Log("No se pudo inicializar el Dispensador de Billetes");
            //    System.Windows.Application.Current.Shutdown();
            //}
            //else
            //{
            //    if (!this.receptorBilletes.Inicializar(this.puertoReceptorBilletes))
            //    {
            //        this.Log("No se pudo inicializar el Receptor de Billetes");
            //        this.dispensadorBilletes.Dispose();
            //        System.Windows.Application.Current.Shutdown();
            //        return;
            //    }
            //    //try
            //    //{
            //    //    this.InicializaPinPad();
            //    //}
            //    //catch (Exception exception)
            //    //{
            //    //    Exception ex = exception;
            //    //    this.Log(string.Concat("Error: ", ex.Message, "; StackTrace: ", ex.StackTrace));
            //    //}
            //}

            //ListadoImagenes();

        }
               
        #region Eventos de pantalla ....

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnHabilita_Click(object sender, RoutedEventArgs e)
        {
            int _monto = Convert.ToInt32(txtMonto.Text);
            ClienteListoParaRecibirDinero("01", _monto);
            lblLeyenda.Content = txtMonto.Text.ToString();
        }

        private void BtnCobrar_Click(object sender, RoutedEventArgs e)
        {
            int _monto = Convert.ToInt32(txtMonto.Text);
            string tipo = "B";
            int valor = _monto;
            //string text = button.Text;
            //switch (text)
            //{
            //    case "$20.00":
            //        {
            //            tipo = "B";
            //            valor = 20;
            //            break;
            //        }
            //    case "$50.00":
            //        {
            //            tipo = "B";
            //            valor = 50;
            //            break;
            //        }
            //    case "$100.00":
            //        {
            //            tipo = "B";
            //            valor = 100;
            //            break;
            //        }
            //    case "$200.00":
            //        {
            //            tipo = "B";
            //            valor = 200;
            //            break;
            //        }
            //    case "$500.00":
            //        {
            //            tipo = "B";
            //            valor = 500;
            //            break;
            //        }
            //    case "$1.00":
            //        {
            //            tipo = "M";
            //            valor = 1;
            //            break;
            //        }
            //    case "$5.00":
            //        {
            //            tipo = "M";
            //            valor = 5;
            //            break;
            //        }
            //    case "$10.00":
            //        {
            //            tipo = "M";
            //            valor = 10;
            //            break;
            //        }
            //    case "$1000.00":
            //        {
            //            tipo = "B";
            //            valor = 1000;
            //            break;
            //        }
            //}
            this.InformaDineroRecibido(tipo, valor);
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            CancelarPagar();
        }

        #endregion

        #region Métodos app

        private void ListadoImagenes()
        {
            String exePath = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string dir = System.IO.Path.GetDirectoryName(exePath);

            string path = @"J:\APLICACIONES\SIREM\POZARICA\GSirem2022_134\wpfKiosko20\Images\";

            List<Image> myImages = new List<Image>();
            Image myImage = new Image();
            myImage.Source = new BitmapImage(new Uri("/Images/imagen01.jpg", UriKind.RelativeOrAbsolute));
            myImage.Height = 200;
            myImage.Width = 200;
            myImages.Add(myImage);
            Image myImage1 = new Image();
            myImage1.Source = new BitmapImage(new Uri("/Images/imagen02.jpg", UriKind.RelativeOrAbsolute));
            myImage1.Height = 200;
            myImage1.Height = 200;
            myImages.Add(myImage1);
            Image myImage2 = new Image();
            myImage2.Source = new BitmapImage(new Uri("/Images/imagen03.jpg", UriKind.RelativeOrAbsolute));
            myImage2.Width = 200;
            myImage.Height = 200;
            myImages.Add(myImage2);

            Image myImage3 = new Image();
            myImage3.Source = new BitmapImage(new Uri("/Images/imagen04.jpg", UriKind.RelativeOrAbsolute));
            myImage3.Height = 200;
            myImage3.Width = 200;
            myImages.Add(myImage3);

            //Image myImage4 = new Image();
            //myImage4.Source = new BitmapImage(new Uri("/Images/nature26.jpg", UriKind.Relative));
            //myImage4.Height = 200;
            //myImage4.Width = 200;
            //myImages.Add(myImage4);

            //this.MyCarousel.ItemsSource = myImages;
        }

        #endregion

        #region Métodos del kiosko

        private void InformaDineroRecibido(string tipo, decimal valor)
        {
            this.valores = this.caja.AcumularDineroRecibido(tipo, valor);
            this.Log(string.Format("Pagado:{0}, Pendiente:{1}, cambio:{2}", this.valores.MontoPagado, this.valores.MontoPendiente, this.valores.MontoCambio));
            //IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<SiaKioskoHub>();
            //((dynamic)hubContext.Clients.All).dineroRecibido(this.valores);
            if (this.valores.MontoPendiente == decimal.Zero)
            {
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
                            System.Windows.Application.Current.Shutdown();
                        }
                        this.caja.AcumulaDineroEntregado("M", new decimal(5), denominaciones[2]);
                    }
                    if (denominaciones[3] > 0)
                    {
                        this.Log(string.Format("Monedas de $1 dispensadas: {0}", denominaciones[3]));
                        if (this.monedero.RetornarMonedas(this.dispositivoDispensadorMonedas1Peso, (byte)denominaciones[3]))
                        {
                            System.Windows.Application.Current.Shutdown();
                        }
                        this.caja.AcumulaDineroEntregado("M", decimal.One, denominaciones[3]);
                    }
                }
                string sdenominaciones = this.caja.GeneraCadenaDenominaciones();
                this.caja.LimpiaCaja();
                this.caja = null;
                this.valores = null;
                //((dynamic)hubContext.Clients.All).generaFolio(sdenominaciones);
                this.Log("[Transacción Completada]");
                this.Log("");
            }
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
            if (!CheckAccess())
            {
                this.Log(string.Format("Moneda recibida: {0}", e.CoinValue));
                this.InformaDineroRecibido("M", e.CoinValue);
                return;
            }
            Dispatcher.Invoke(new EventHandler<CoinAcceptorCoinEventArgs>(this._coinAcceptor_CoinAccepted), new object[] { sender, e });
        }

        private void _coinAcceptor_ErrorMessageAccepted(object sender, CoinAcceptorErrorEventArgs e)
        {
            if (!CheckAccess())
            {
                this.Log(string.Format("Coin acceptor error: {0} ({1}, {2:X2})", e.ErrorMessage, e.Error, (byte)e.Error));
                return;
            }
            Dispatcher.Invoke(new EventHandler<CoinAcceptorErrorEventArgs>(this._coinAcceptor_ErrorMessageAccepted), new object[] { sender, e });
        }

        private void Log(string msg)
        {
            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\";
            DateTime now = DateTime.Now;
            string fileName = string.Concat("Kiosko-", now.ToString("dd-MM-yyyy"), ".txt");
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




    }
}
