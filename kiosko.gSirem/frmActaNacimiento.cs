using IronPdf;
using kiosko.gSirem.clases;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ZXing;
using PdfiumViewer;

using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace kiosko.gSirem
{
    public partial class frmActaNacimiento : Telerik.WinControls.UI.RadForm
    {
        private string archivoImagenRecibo = "";
        private System.Windows.Forms.Timer aTimer;
        private int counter = 60;
        private BusquedaContribuyente frmBusqueda;

        public frmActaNacimiento()
        {
            InitializeComponent();  
        }

        #region METODOS DE PANTALLA

        private void ImprimirTicketR2()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-MX");
            DateTime dt = DateTime.Now;

            Guid guid = Guid.NewGuid();
            string acta = "";
            ActaNacimientoBE actaBE = Global.Acta;
            //StringBuilder builder = new StringBuilder();

            //for (int i = 0; i < 5; i++)
            //{
                acta = File.ReadAllText($"{path}\\formato\\Acta.html");
                acta = acta.Replace("{identicadorElectronico}", "30131000120220108510");
                acta = acta.Replace("{curp}", actaBE.CURP);
                acta = acta.Replace("{ncn}", "--------------");
                acta = acta.Replace("{entidad}", actaBE.CDES_EFR.ToString().Trim().ToUpper());
                acta = acta.Replace("{municipioRegistro}", actaBE.CMUNNACREG.ToString().Trim().ToUpper());

                acta = acta.Replace("{comparecio}", actaBE.CCOMPARECE.ToString().Trim().ToUpper());
                acta = acta.Replace("{oficialia}", actaBE.NTOMO.ToString().Trim().ToUpper());
                acta = acta.Replace("{fechaRegistro}", actaBE.DFECHA_REG.ToString().Trim().ToUpper());
                acta = acta.Replace("{libro}", actaBE.NLIBRO.ToString().Trim().ToUpper());
                acta = acta.Replace("{numeroActa}", actaBE.CACTA.ToString().Trim().ToUpper());

                acta = acta.Replace("{nombres}", actaBE.CNOMBREREG.ToString().Trim().ToUpper());
                acta = acta.Replace("{primerApellido}", actaBE.CAPE1REG.ToString().Trim().ToUpper());
                acta = acta.Replace("{segundoApellido}", actaBE.CAPE2REG.ToString().Trim().ToUpper());

                acta = acta.Replace("{sexo}", actaBE.SEXO.ToString().Trim().ToUpper());
                acta = acta.Replace("{fechaNacimiento}", actaBE.SEXO.ToString().Trim().ToUpper());
                acta = acta.Replace("{lugarNacimiento}", actaBE.CLOCNACREG.ToString().Trim().ToUpper());

                acta = acta.Replace("{nombresDF}", actaBE.CNOMPADRE.ToString().Trim().ToUpper());
                acta = acta.Replace("{primerApellidoDF}", actaBE.CAPE1PADRE.ToString().Trim().ToUpper());
                acta = acta.Replace("{segundoApellidoDF}", actaBE.CAPE2PADRE.ToString().Trim().ToUpper());
                acta = acta.Replace("{lugarNacimientoDF}", actaBE.DES_NACPA.ToString().Trim().ToUpper());
                acta = acta.Replace("{curpDF}", "-----------");

                acta = acta.Replace("{nombresDF2}", actaBE.CNOMMADRE.ToString().Trim().ToUpper());
                acta = acta.Replace("{primerApellidoDF2}", actaBE.CAPE1MADRE.ToString().Trim().ToUpper());
                acta = acta.Replace("{segundoApellidoDF2}", actaBE.CAPE2MADRE.ToString().Trim().ToUpper());
                acta = acta.Replace("{lugarNacimientoDF2}", actaBE.DES_NACMADRE.ToString().Trim().ToUpper());
                acta = acta.Replace("{curpDF2}", "-----------");

                acta = acta.Replace("{anotacionesMargianles}", actaBE.ANOTMARG.ToString().Trim().ToUpper());
                acta = acta.Replace("{fecha}", $"A LOS {dt.Day} DÍAS DEL MES DE {dt.ToString("MMMM", cultureinfo).ToUpper()} DE {dt:yyyy}. DOY FE.");
                acta = acta.Replace("{firmaElectronica}", "QU 9N QT gy MT Ix Mk hP Q0 5U Qj Ew fE pP U0 Ug QU JS SE FO fE FO VE 9O SU 98 TU FU SU FT fD Ey MD E0 Mz Aw MD Ex OT gy MD Ay Mz Yw fE 18 MT Ig ZG Ug ZG lj aW Vt Yn Jl IG Rl ID E5 OD J8 T0 FY QU NB fG 51 bG x8 bn Vs bA ==");

                acta = acta.Replace("{codIEImage}", GetCodeBarra("30131000120220108510", 10, 25));
                acta = acta.Replace("{curpImage}", GetCodeBarra("VIVD050130MVZLLCA4", 10, 25));
                acta = acta.Replace("{nombreORC}", "LIC. ROSALBA RIVERA MONCAYO");

                acta = acta.Replace("{QrImage}", GetQR("MIAGCSqGSIb3DQEHAqCAMIACAQExCzAJBgUrDgMCGgUAMIAGCSqGSIb3DQEHAaCAJIAEgboxfDMwfDEzMXwxfDIwMDV8OTI2fDB8fDF8NHx8MTAwODM1MzR8MDgvMDMvMjAwNXxWfERBQ0lBIFBBUklTfFZJTExBfFZJTExBfEZ8MzAvMDEvMjAwNXwzMHwxMzF8fDIyM3xWSVZEMDUwMTMwTVZaTExDQTR8RkVMSVBFIERFIEpFU1VTfFZJTExBfFJFWkF8MzN8MjIzfHxHVUFEQUxVUEV8VklMTEF8RVNQSU5PU0F8MzN8MjIzfHwAAAAAAAAxggKCMIICfgIBATCCAXswggFhMQ8wDQYDVQQHEwZDb2xpbWExDzANBgNVBAgTBkNvbGltYTELMAkGA1UEBhMCTVgxDjAMBgNVBBETBTI4MDEwMUIwQAYDVQQJEzlUZXJjZXIgQW5pbGxvIFBlcmlmZXJpY28gZXNxdWluYSBjb24gQXYgRWplcmNpdG8gTWV4aWNhbm8xQzBBBgNVBAMTOkF1dG9yaWRhZCBDZXJ0aWZpY2Fkb3JhIGRlbCBHb2JpZXJubyBkZWwgRXN0YWRvIGRlIENvbGltYS4xQzBBBgNVBAsTOkRpcmVjY2lvbiBHZW5lcmFsIFBhcmEgbGEgSW5ub3ZhY2lvbiBkZSBsYSBHZXN0aW9uIFB1YmxpY2ExJjAkBgNVBAoTHUdvYmllcm5vIGRlbCBFc3RhZG8gZGUgQ29saW1hMSowKAYJKoZIhvcNAQkBFhtmaXJtYWVsZWN0cm9uaWNhQGNvbC5nb2IubXgCFDAwMDAwMTAwMDAwMTAwMDAwNTM0MAkGBSsOAwIaBQCgXTAYBgkqhkiG9w0BCQMxCwYJKoZIhvcNAQcBMBwGCSqGSIb3DQEJBTEPFw0yMjA5MTQxNzE1MjJaMCMGCSqGSIb3DQEJBDEWBBSfntuHVO2sWRSJ", 280, 280));
                acta = acta.Replace("{codigoQrImage}", GetQR("QU 9N QT gy MT Ix Mk hP Q0 5U Qj Ew fE pP U0 Ug QU JS SE FO fE FO VE 9O SU 98 TU FU SU FT fD Ey MD E0 Mz Aw MD Ex OT gy MD Ay Mz Yw fE 18 MT Ig ZG Ug ZG lj aW Vt Yn Jl IG Rl ID E5 OD J8 T0 FY QU NB fG 51 bG x8 bn Vs bA ==", 130, 130));

                acta = acta.Replace("{codVerImage}", ImageToB64($"{path}\\formato\\escudoCodVer.jpg"));
                acta = acta.Replace("{FirmaImage}", ImageToB64($"{path}\\formato\\firmaORC.jpg"));

                acta = acta.Replace("��", " ");
                acta = acta.Replace("� ", "");
                acta = acta.Replace("�", "");

                acta = acta.Replace("á", "&aacute;");
                acta = acta.Replace("é", "&eacute;");
                acta = acta.Replace("í", "&iacute;");
                acta = acta.Replace("ó", "&oacute;");
                acta = acta.Replace("ú", "&uacute;");
                acta = acta.Replace("Á", "&Aacute;");
                acta = acta.Replace("É", "&Eacute;");
                acta = acta.Replace("Í", "&Iacute;");
                acta = acta.Replace("Ó", "&Oacute;");
                acta = acta.Replace("Ú", "&Uacute;");
                acta = acta.Replace("ñ", "&ntilde;");
            //    acta = acta.Replace("{newPagina}", "<div style = \"page-break-after: always; \"></div>");

            //    builder.Append(acta);
            //    builder.AppendLine();
            //}


                string rutaPDFs = ConfigurationManager.AppSettings["RutaPDFs"];
                string pathHTMLPlantilla = $"{path}\\formato\\Acta.html";
                string destinationFile = $"{rutaPDFs}\\{guid}.PDF"; //HttpContext.Current.Server.MapPath($"~/GsCaja/formato/A{datos.NoCajera}.PDF");
                string pathHTMLTemp = $"{path}\\formato\\temporal.html";//temporal
                try
                {
                    if (File.Exists(pathHTMLPlantilla))
                    {
                        //string sHtml = GetStringOfFile(pathHTMLPlantilla);//leemos la plantilla
                        //sustituimos los datos
                        //string resultHtml = acta;
                    string resultHtml = acta.ToString();
                    File.WriteAllText(pathHTMLTemp, resultHtml);//guardamos el archivo modificado como temporal
                                                                    //convertimos a PDF-------------
                                                                    //string pathWKHTMLTOPDF = AppDomain.CurrentDomain.BaseDirectory + "GsCaja\\formato\\wkhtmltopdf.exe";
                                                                    //string s = AppDomain.CurrentDomain.BaseDirectory + "GsCaja\\formato\\temporal.html";
                        string pathWKHTMLTOPDF = $"{path}\\formato\\wkhtmltopdf.exe";
                        string s = $"{path}\\formato\\temporal.html";
                        ProcessStartInfo oProcessStartInfo = new ProcessStartInfo();
                        oProcessStartInfo.UseShellExecute = false;
                        oProcessStartInfo.FileName = pathWKHTMLTOPDF;
                        String mipag = "file:///" + $"{path}\\formato\\temporal.html";

                        //string switches = "";
                        //switches += "--print-media-type ";
                        //switches += "--margin-top 10mm --margin-bottom 10mm --margin-right 10mm --margin-left 10mm ";
                        //switches += "--page-width 216mm --page-height 165mm";//page-height: 'auto'

                        //oProcessStartInfo.Arguments = switches + " " + mipag + " " + destinationFile;
                        oProcessStartInfo.Arguments = " --page-width 22cm --page-height 27.94cm --margin-top 10mm --margin-bottom 7mm --margin-right 10mm --margin-left 13mm " + mipag + " " + destinationFile;
                        using (Process oProcess = Process.Start(oProcessStartInfo))
                        {
                            oProcess.WaitForExit();
                        }
                        //convertimos a PDF-----------

                    }

                    //string pdfSave = $"{path}\\formato\\temp{guid}.pdf";

                    if (File.Exists(destinationFile))
                    {
                        this.archivoImagenRecibo = destinationFile;
                        this.Log(string.Concat("Imprimiendo recibo ", this.archivoImagenRecibo));
                        PrintRecibo(archivoImagenRecibo, guid.ToString());

                        //string impresoraRecibos = ConfigurationManager.AppSettings["ImpresoraPdf"];
                        //PrintDocument printDocument = new PrintDocument();
                        //printDocument.PrinterSettings.PrinterName = impresoraRecibos;
                        //printDocument.DefaultPageSettings.Landscape = false;

                        //printDocument.PrintPage += new PrintPageEventHandler(this.PrintPageRecibo);
                        //printDocument.Print();
                    }
                }
                catch (Exception exception)
                {
                    Exception ex = exception;
                    this.Log(string.Concat("Error: ", ex.Message, "; StackTrace: ", ex.StackTrace));

                }
            
        }

        private void ImprimirTicket()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-MX");
            DateTime dt = DateTime.Now;

            Guid guid = Guid.NewGuid();
            string acta = "";
            acta = File.ReadAllText(string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "\\Acta.html"));
            acta = acta.Replace("{identicadorElectronico}", "30131000120220108510");
            acta = acta.Replace("{curp}", "VIVD050130MVZLLCA4");
            acta = acta.Replace("{ncn}", "--------------");
            acta = acta.Replace("{entidad}", "VERACRUZ");
            acta = acta.Replace("{municipioRegistro}", "POZA RICA DE HIDALGO");

            acta = acta.Replace("{comparecio}", "AMBOS PROGENITORES");
            acta = acta.Replace("{oficialia}", "0001");
            acta = acta.Replace("{fechaRegistro}", "08/03/2005");
            acta = acta.Replace("{libro}", "4");
            acta = acta.Replace("{numeroActa}", "926");

            acta = acta.Replace("{nombres}", "DACIA PARIS");
            acta = acta.Replace("{primerApellido}", "VILLA");
            acta = acta.Replace("{segundoApellido}", "VILLA");

            acta = acta.Replace("{sexo}", "MUJER");
            acta = acta.Replace("{fechaNacimiento}", "30/01/2005");
            acta = acta.Replace("{lugarNacimiento}", "POZA RICA DE HIDALGO VERACRUZ");

            acta = acta.Replace("{nombresDF}", "FELIPE DE JESUS");
            acta = acta.Replace("{primerApellidoDF}", "VILLA");
            acta = acta.Replace("{segundoApellidoDF}", "REZA");
            acta = acta.Replace("{lugarNacimientoDF}", "MEXICANA");
            acta = acta.Replace("{curpDF}", "-----------");

            acta = acta.Replace("{nombresDF2}", "GUADALUPE");
            acta = acta.Replace("{primerApellidoDF2}", "VILLA");
            acta = acta.Replace("{segundoApellidoDF2}", "ESPINOSA");
            acta = acta.Replace("{lugarNacimientoDF2}", "MEXICANA");
            acta = acta.Replace("{curpDF2}", "-----------");

            acta = acta.Replace("{anotacionesMargianles}", "Sin anotaciones marginales");
            acta = acta.Replace("{fecha}", $"A LOS {dt.Day} DÍAS DEL MES DE {dt.ToString("MMMM", cultureinfo).ToUpper()} DE {dt:yyyy}. DOY FE.");
            acta = acta.Replace("{firmaElectronica}", "QU 9N QT gy MT Ix Mk hP Q0 5U Qj Ew fE pP U0 Ug QU JS SE FO fE FO VE 9O SU 98 TU FU SU FT fD Ey MD E0 Mz Aw MD Ex OT gy MD Ay Mz Yw fE 18 MT Ig ZG Ug ZG lj aW Vt Yn Jl IG Rl ID E5 OD J8 T0 FY QU NB fG 51 bG x8 bn Vs bA ==");

            acta = acta.Replace("{codIEImage}", GetCodeBarra("30131000120220108510", 10, 25));
            acta = acta.Replace("{curpImage}", GetCodeBarra("VIVD050130MVZLLCA4", 10, 25));
            acta = acta.Replace("{nombreORC}", "LIC. ROSALBA RIVERA MONCAYO");

            acta = acta.Replace("{QrImage}", GetQR("MIAGCSqGSIb3DQEHAqCAMIACAQExCzAJBgUrDgMCGgUAMIAGCSqGSIb3DQEHAaCAJIAEgboxfDMwfDEzMXwxfDIwMDV8OTI2fDB8fDF8NHx8MTAwODM1MzR8MDgvMDMvMjAwNXxWfERBQ0lBIFBBUklTfFZJTExBfFZJTExBfEZ8MzAvMDEvMjAwNXwzMHwxMzF8fDIyM3xWSVZEMDUwMTMwTVZaTExDQTR8RkVMSVBFIERFIEpFU1VTfFZJTExBfFJFWkF8MzN8MjIzfHxHVUFEQUxVUEV8VklMTEF8RVNQSU5PU0F8MzN8MjIzfHwAAAAAAAAxggKCMIICfgIBATCCAXswggFhMQ8wDQYDVQQHEwZDb2xpbWExDzANBgNVBAgTBkNvbGltYTELMAkGA1UEBhMCTVgxDjAMBgNVBBETBTI4MDEwMUIwQAYDVQQJEzlUZXJjZXIgQW5pbGxvIFBlcmlmZXJpY28gZXNxdWluYSBjb24gQXYgRWplcmNpdG8gTWV4aWNhbm8xQzBBBgNVBAMTOkF1dG9yaWRhZCBDZXJ0aWZpY2Fkb3JhIGRlbCBHb2JpZXJubyBkZWwgRXN0YWRvIGRlIENvbGltYS4xQzBBBgNVBAsTOkRpcmVjY2lvbiBHZW5lcmFsIFBhcmEgbGEgSW5ub3ZhY2lvbiBkZSBsYSBHZXN0aW9uIFB1YmxpY2ExJjAkBgNVBAoTHUdvYmllcm5vIGRlbCBFc3RhZG8gZGUgQ29saW1hMSowKAYJKoZIhvcNAQkBFhtmaXJtYWVsZWN0cm9uaWNhQGNvbC5nb2IubXgCFDAwMDAwMTAwMDAwMTAwMDAwNTM0MAkGBSsOAwIaBQCgXTAYBgkqhkiG9w0BCQMxCwYJKoZIhvcNAQcBMBwGCSqGSIb3DQEJBTEPFw0yMjA5MTQxNzE1MjJaMCMGCSqGSIb3DQEJBDEWBBSfntuHVO2sWRSJ", 250, 250));
            acta = acta.Replace("{codigoQrImage}", GetQR("QU 9N QT gy MT Ix Mk hP Q0 5U Qj Ew fE pP U0 Ug QU JS SE FO fE FO VE 9O SU 98 TU FU SU FT fD Ey MD E0 Mz Aw MD Ex OT gy MD Ay Mz Yw fE 18 MT Ig ZG Ug ZG lj aW Vt Yn Jl IG Rl ID E5 OD J8 T0 FY QU NB fG 51 bG x8 bn Vs bA ==", 145, 145));



            try
            {


                IronPdf.PdfDocument pdfDocument = (new HtmlToPdf()).RenderHtmlAsPdf(acta, (Uri)null, null);
                string rutaPDFs = ConfigurationManager.AppSettings["RutaPDFs"];


                this.archivoImagenRecibo = string.Concat(rutaPDFs, guid.ToString(), ".jpg");
                this.archivoImagenRecibo = pdfDocument.ToJpegImages(this.archivoImagenRecibo)[0];
                this.Log(string.Concat("Imprimiendo recibo ", this.archivoImagenRecibo));
                string impresoraRecibos = ConfigurationManager.AppSettings["ImpresoraPdf"];
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

        private void PrintRecibo(string path, string guid)
        {
            string impresoraRecibos = ConfigurationManager.AppSettings["ImpresoraPdf"];

            using (var document = PdfiumViewer.PdfDocument.Load(path))
            {
                using (var printDocument = document.CreatePrintDocument())
                {
                    printDocument.PrinterSettings.PrintFileName = $"{guid}.pdf";
                    printDocument.PrinterSettings.PrinterName = impresoraRecibos;
                    printDocument.DocumentName = $"{guid}.pdf";
                    printDocument.PrinterSettings.PrintFileName = $"{guid}.pdf";
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.Print();
                }
            }
        }

        private void PrintPageRecibo(object o, PrintPageEventArgs e)
        {
            Image img = Image.FromFile(this.archivoImagenRecibo);
            e.Graphics.DrawImage(img, 2, 2, 899, 1100);
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

        private static string GetCodeBarra(string _url, int w, int h)
        {
            #region QR
            string code = _url;

            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.CODE_128;

            QCwriter.Options = new ZXing.Common.EncodingOptions
            {
                Width = w,
                Height = h,
                PureBarcode = true,
                GS1Format = true
            };
            var result = QCwriter.Write(code);
            var barcodeBitmap = new Bitmap(result);

            string imagen = null;
            using (MemoryStream ms = new MemoryStream())
            {
                barcodeBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();

                imagen = Convert.ToBase64String(byteImage);
            }
            #endregion

            return imagen;
        }

        private static string GetQR(string _url, int w, int h)
        {
            #region QR
            string code = _url;

            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            QCwriter.Options = new ZXing.Common.EncodingOptions
            {
                Width = w,
                Height = h
            };
            var result = QCwriter.Write(code);
            var barcodeBitmap = new Bitmap(result);

            string imagen = null;
            using (MemoryStream ms = new MemoryStream())
            {
                barcodeBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();

                imagen = Convert.ToBase64String(byteImage);
            }
            #endregion

            return imagen;
        }

        public static string QR(string baseQR)
        {
            string sQR = "";
            sQR = System.String.Format("data:image/gif;base64,{0}", baseQR);
            return sQR;

        }

        string ImageToB64(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, ImageFormat.Jpeg);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);

                    // In my case I didn't find the part "data:image/png;base64,", so I added.
                    return base64String;// $"data:image/png;base64,{base64String}";
                }
            }
        }

        private static string GetStringOfFile(string pathFile)
        {
            string contenido = File.ReadAllText(pathFile);
            return contenido;
        }

        private bool WithErrorsCURP()
        {
            errorProviderCurp.Clear();
            if (string.IsNullOrWhiteSpace(txtCurpBusqueda.Text))
            {
                txtCurpBusqueda.Focus();
                errorProviderCurp.SetError(txtCurpBusqueda, "Es requerida la CURP para iniciar la busqueda!");
                return false;
            }
            return true;
        }

        private bool WithErrorsCURP_Padres()
        {
            errorProviderDatPadres.Clear();
            if (string.IsNullOrWhiteSpace(txtNombresCurp.Text))
            {
                txtNombresCurp.Focus();
                errorProviderDatPadres.SetError(txtNombresCurp, "El nombre del padre o la madre es requerido!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtPrimerApellidoCurp.Text))
            {
                txtPrimerApellidoCurp.Focus();
                errorProviderDatPadres.SetError(txtPrimerApellidoCurp, "El primer apellido del padre o la madre es requerido!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtSegundoApellidoCurp.Text))
            {
                txtSegundoApellidoCurp.Focus();
                errorProviderDatPadres.SetError(txtSegundoApellidoCurp, "El segundo apellido del padre o la madre es requerido!");
                return false;
            }
            return true;
        }

        private bool WithErrorsDatosPersonales()
        {
            errorProviderDatPersonales.Clear();
            if (string.IsNullOrWhiteSpace(txtNombreDP.Text))
            {
                txtNombreDP.Focus();
                errorProviderDatPersonales.SetError(txtNombreDP, "Nombre es requerido!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtPrimerApellidoDP.Text))
            {
                txtPrimerApellidoDP.Focus();
                errorProviderDatPersonales.SetError(txtPrimerApellidoDP, "El primer apellido es requerido!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtSegundoApellidoDP.Text))
            {
                txtSegundoApellidoDP.Focus();
                errorProviderDatPersonales.SetError(txtSegundoApellidoDP, "El segundo apellido es requerido!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(ddlDiaNacimientoDP.Text))
            {
                ddlDiaNacimientoDP.Focus();
                errorProviderDatPersonales.SetError(ddlDiaNacimientoDP, "Día de nacimiento es requerido!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(ddlMesNacimientoDP.Text))
            {
                ddlMesNacimientoDP.Focus();
                errorProviderDatPersonales.SetError(ddlMesNacimientoDP, "Mes de nacimiento es requerido!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAnioNacimientoDG.Text))
            {
                txtAnioNacimientoDG.Focus();
                errorProviderDatPersonales.SetError(txtAnioNacimientoDG, "Año apellido es requerido!");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(ddlSexoDP.Text))
            {
                ddlSexoDP.Focus();
                errorProviderDatPersonales.SetError(ddlSexoDP, "Sexo apellido es requerido!");
                return false;
            }

            return true;
        }

        private void CargarDias()
        {
            for (int i = 1; i <= 31; i++)
            {
                ddlDiaNacimientoDP.Items.Add(i.ToString());
            }
        }

        private void CargarSexo()
        {
            ddlSexoDP.Items.Add("HOMBRE".ToString());
            ddlSexoDP.Items.Add("MUJER".ToString());
        }

        private void CargarMes()
        {
            ddlMesNacimientoDP.DataSource = null;

            List<string> nombreMes = DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToList();
            var listaMesesSeleccionados = nombreMes.Select(m => new
            {
                Id = nombreMes.IndexOf(m) + 1,
                Name = m.ToString().ToUpper()
            });

            ddlMesNacimientoDP.DataSource = listaMesesSeleccionados;
            ddlMesNacimientoDP.DisplayMember = "Name";
            ddlMesNacimientoDP.ValueMember = "Id";
        }

        void resetBusqueda(bool activo)
        {
            txtLblCmmtCrup.Visible = activo;
            lblEstatusCurp.Visible = activo;
            btnCurpContinuar.Visible = activo;
            txtNombresCurp.Visible = activo;
            txtPrimerApellidoCurp.Visible = activo;
            txtSegundoApellidoCurp.Visible = activo;
            lblNombreCurp.Visible = activo;
            lblPrimerApellido.Visible = activo;
            lblSegundoApellido.Visible = activo;
        }

        void limpiarCampos()
        {
            radPageViewBusqueda.SelectedPage = pageViewCurp;
            pvVistaPrevia.SelectedPage = pvpDatActaNac;

            /*CURP*/
            txtCurpBusqueda.Text = "";
            txtNombresCurp.Text = "";
            txtPrimerApellidoCurp.Text = "";
            txtSegundoApellidoCurp.Text = "";
            lblEstatusCurp.Text = "";
            //-----------------------------------
            resetBusqueda(false);
            //-----------------------------------
            txtNombreDP.Text = "";
            txtPrimerApellidoDP.Text = "";
            txtSegundoApellidoDP.Text = "";
            ddlDiaNacimientoDP.Text = "";
            ddlMesNacimientoDP.Text = "";
            txtAnioNacimientoDG.Text = "";
            ddlSexoDP.Text = "";
            lblEstatusDP.Text = "";
            //-----------------------------------
            lblCurpAnDVP.Text = "";
            lblEntidadRegAnDVP.Text = "";
            lblMunRegAnDVP.Text = "";
            lblFechaRegAnDVP.Text = "";
            lblLibroAnDVP.Text = "";
            lblNumActaAnDVP.Text = "";
            lblNombrePrDVP.Text = "";
            lblPrimerApellidoPrDVP.Text = "";
            lblSegApellidoPrDVP.Text = "";
            lblFechaNacPrDVP.Text = "";
            lblLugNacPrDVP.Text = "";
            lblNombrePadreFprDVP.Text = "";
            lblApellido1PadreFprDVP.Text = "";
            lblApellido2PadreFprDVP.Text = "";
            lblNacionalidadPadreFprDVP.Text = "";
            lblNombreMadreFprDVP.Text = "";
            lblApellido1MadreFprDVP.Text = "";
            lblApellido2MadreFprDVP.Text = "";
            lblNacionalidadMadreFprDVP.Text = "";
            txtanotacionesMarginalesDVP.Text = "";
            //------------------------------------------
            lblDatActaPICurp.Text = "";
            lblDatActaPINombre.Text = "";
            lblDatActaPIApellido1.Text = "";
            lblDatActaPIApellido2.Text = "";
            txtMontoPagar.Text = "0.00";
            lblEstatusCambio.Text = "...";
            lblTimer.Text = "...";
            //-------------------------------------------
            btnConfirmar.Enabled = true;
            Global.Multipago = null;
            Global.Acta = null;
        }

        #endregion

        #region METODOS DE EVENTOS

        private void btnBuscarCurp_Click(object sender, EventArgs e)
        {
            if (WithErrorsCURP())
            {
                string _curp = txtCurpBusqueda.Text.ToString().Trim().ToUpper();
                if (existeCURP(_curp))
                    resetBusqueda(true);
                else
                {
                    this.radDesktopAlertActaNac.CaptionText = "Alerta de búsqueda de CURP!";
                    this.radDesktopAlertActaNac.ContentText = $"No encontramos la curp {_curp}, que nos solicita, favor de verificar el dato....";
                    this.radDesktopAlertActaNac.Show();
                }
            }
            else
            {                
                this.radDesktopAlertActaNac.CaptionText = "Alerta de búsqueda de CURP!";
                this.radDesktopAlertActaNac.ContentText = "Existen registros que aun no se han llenado, favor realizar el correcto llenado...";
                this.radDesktopAlertActaNac.Show();
                //MessageBox.Show("Con errores");
                // Do whatever here... Submit
            }
        }

        private void btnCurpContinuar_Click(object sender, EventArgs e)
        {
            if (WithErrorsCURP_Padres())
            {
                string curp, nombre, paterno, materno;
                curp = txtCurpBusqueda.Text.ToString().Trim().ToUpper();
                nombre = txtNombresCurp.Text.ToString().Trim().ToUpper();
                paterno = txtPrimerApellidoCurp.Text.ToString().Trim().ToUpper();
                materno = txtSegundoApellidoCurp.Text.ToString().Trim().ToUpper();

                BusquedaCurp(curp, nombre, paterno, materno);
                if (Global.Acta != null)
                {
                    AsignaFormulario(Global.Acta);
                    lblEstatusCurp.Text = "LA INFORMACIÓN HA SIDO VALIDADA, FAVOR DE CONTINUAR CON EL TRÁMITE...";
                    lblEstatusDP.Text = "...";
                }
                else
                {
                    this.radDesktopAlertActaNac.CaptionText = "Alerta de búsqueda de CURP!";
                    this.radDesktopAlertActaNac.ContentText = "El registro solicitado no ha sido ubicado, favor de corregir la búsqueda!";
                    this.radDesktopAlertActaNac.Show();
                }

            }
            else
            {
                this.radDesktopAlertActaNac.CaptionText = "Alerta de búsqueda de CURP!";
                this.radDesktopAlertActaNac.ContentText = "Existen registros que aun no se han llenado, favor realizar el correcto llenado...";
                this.radDesktopAlertActaNac.Show();
                //MessageBox.Show("Con errores");
                // Do whatever here... Submit
            }
        }

        private void btnBuscarDatPer_Click(object sender, EventArgs e)
        {
            if (WithErrorsDatosPersonales())
            {
                string nombre, paterno, materno, fNacimiento,sexo,edoNacimiento;
               
                nombre = txtNombreDP.Text.ToString().Trim().ToUpper();
                paterno = txtPrimerApellidoDP.Text.ToString().Trim().ToUpper();
                materno = txtSegundoApellidoDP.Text.ToString().Trim().ToUpper();
                sexo = ddlSexoDP.Text.ToString().ToUpper() == "HOMBRE" ? "MASCULINO" : "FEMENINO";
                edoNacimiento=txtEstadoDG.Text.Trim().ToUpper();
                fNacimiento = $"{txtAnioNacimientoDG.Text.ToString().Trim()}-{ddlMesNacimientoDP.SelectedIndex+1.ToString("##")}-{ddlDiaNacimientoDP.Text.ToString().Trim()}";
                //2016-05-12

                Global.Acta = BusquedaDatosPersonales(nombre, paterno, materno, fNacimiento, sexo, edoNacimiento);
              
                if (Global.Acta!=null)
                {
                    AsignaFormulario(Global.Acta);
                    lblEstatusDP.Text = "LA INFORMACIÓN HA SIDO VALIDADA, FAVOR DE CONTINUAR CON EL TRÁMITE...";
                    lblEstatusCurp.Text = "...";
                }
                else
                {
                    this.radDesktopAlertActaNac.CaptionText = "Alerta de datos personales!";
                    this.radDesktopAlertActaNac.ContentText = "El registro solicitado no ha sido ubicado, favor de corregir la búsqueda!";
                    this.radDesktopAlertActaNac.Show();
                }
            }
            else
            {
                //MessageBox.Show("Con errores");
                // Do whatever here... Submit
                this.radDesktopAlertActaNac.CaptionText = "Alerta de búsqueda de datos personales!";
                this.radDesktopAlertActaNac.ContentText = "Existen registros que aun no se han llenado, favor realizar el correcto llenado...";
                this.radDesktopAlertActaNac.Show();
            }
        }

        private void frmActaNacimiento_Load(object sender, EventArgs e)
        {
            CargarDias();
            CargarMes();
            CargarSexo();
            limpiarCampos();
            txtCurpBusqueda.Text = "VIVD050130MVZLLCA4";
            txtNombresCurp.Text = "FELIPE DE JESUS";
            txtPrimerApellidoCurp.Text = "VILLA";
            txtSegundoApellidoCurp.Text = "REZA";
        }

        private void txtAnioNacimientoDG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void radWizard1_Next(object sender, WizardCancelEventArgs e)
        {
            string Estatus1 = lblEstatusCurp.Text.ToString().Trim();
            bool bandera1 = lblEstatusCurp.Text.ToString().Trim().Count() > 3 ? true : false;
            bool bandera2 = lblEstatusDP.Text.ToString().Trim().Count() > 3 ? true : false;

            if (this.radWizard1.SelectedPage == this.radWizard1.Pages[0])
            {
                if (bandera1 || bandera2)
                {
                    e.Cancel = true;
                    this.radWizard1.SelectedPage = this.radWizard1.Pages[1];
                    radWizard1.CancelButton.Enabled = true;

                }
                else
                {
                    this.radDesktopAlertActaNac.CaptionText = "Alerta de validación de búsqueda!";
                    this.radDesktopAlertActaNac.ContentText = "No se ha validado la información solicitada durante la busqueda, favor realizar el correcto llenado...";
                    this.radDesktopAlertActaNac.Show();
                    e.Cancel = true;
                    this.radWizard1.SelectedPage = this.radWizard1.Pages[0];                 
                }
            }
            else if (this.radWizard1.SelectedPage == this.radWizard1.Pages[1])
            {
                e.Cancel = true;
                this.radWizard1.SelectedPage = this.radWizard1.Pages[2];
            }
        }

        private void radWizard1_Cancel(object sender, EventArgs e)
        {           
            this.radWizard1.SelectedPage = this.radWizard1.Pages[0];
            this.radDesktopAlertActaNac.CaptionText = "Notificación de servicios!";
            this.radDesktopAlertActaNac.ContentText = "movimiento cancelado, retornamos a la página de inicio...";
            this.radDesktopAlertActaNac.Show();
            limpiarCampos();
        }

        private void radWizard1_Finish(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            radWizard1.CancelButton.Enabled = false;
            radWizard1.BackButton.Enabled = false;
            radWizard1.FinishButton.Enabled = false;
            lblEstatusCambio.Text = $"Se realizo un pago de: ${Convert.ToDecimal(txtMontoPagar.Text):#,#0.00}, favor de verificar su cambio $0.00. Gracias";

            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 500; // 1 second
            aTimer.Start();
            lblTimer.Text = counter.ToString();
        }

        private void btnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            this.radWizard1.SelectedPage = this.radWizard1.Pages[0];
            //Limpiar todos los campos....
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Decimal monto = Convert.ToDecimal(txtMontoPagar.Text);
            //Realizar el cobro, ya que todo el proceso esta validado....
            //Activar los sensores y dispensadores....

            //this.radWizard1.Pages[2]
            btnConfirmar.Enabled = false;
            radWizard1.CancelButton.Enabled = false;
            radWizard1.BackButton.Enabled = false;
            radWizard1.FinishButton.Enabled = false;
            if (Global.Multipago != null && Global.Acta != null && procesarPago())
            {
                ImprimirTicketR2();
                lblEstatusCambio.Text = $"Se realizo un pago de: ${monto:#,#0.00}, favor de verificar su cambio $0.00. Gracias";
                registrarPago(monto);
                aTimer = new System.Windows.Forms.Timer();
                aTimer.Tick += new EventHandler(aTimer_Tick);
                aTimer.Interval = 500; // 1 second
                aTimer.Start();
                lblTimer.Text = counter.ToString();
            }  
        }

        private void aTimer_Tick(object sender, EventArgs e)
        {
            counter--;

            if (counter == 0)
            {
                aTimer.Stop();
                this.radWizard1.SelectedPage = this.radWizard1.Pages[0];
                limpiarCampos();
            }
            lblTimer.Text =$"IMPRIMIENDO RECIBO Y REDIRECCIONANDO EN {counter.ToString()} SEG";
        }
        
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirTicketR2();
        }


        #endregion

        #region METODOS DE BASE DE DATOS

        Boolean existeCURP(string curp)
        {
            Boolean existe = false;
            AccesoDatos obj = new AccesoDatos();
            existe = obj.GetExisteCURP(curp,1) > 0;

            return existe;
        }

        ActaNacimientoBE BusquedaDatosPersonales(string nombre, string paterno, string materno, string fNacimiento, string sexo, string edoNacimiento)
        {
            Global.Acta = new ActaNacimientoBE();
            AccesoDatos obj = new AccesoDatos();
            Global.Acta = obj.GetDatosPersonalesRC(nombre, paterno, materno, fNacimiento, sexo, edoNacimiento);

            return Global.Acta;
        }

        ActaNacimientoBE BusquedaCurp(string curp, string nombre, string paterno, string materno)
        {
            Global.Acta = new ActaNacimientoBE();
            AccesoDatos obj = new AccesoDatos();
            Global.Acta = obj.GetCurpByDP(curp,nombre,paterno,materno,2);

            return Global.Acta;
        }
        Multipago GetMultipago(string letra)
        {
            Global.Multipago = new Multipago();
            AccesoDatos obj = new AccesoDatos();
            Global.Multipago = obj.GetMultipago(letra);
            return Global.Multipago;
        }
        Boolean procesarPago()
        {
            //Code

            return true;
        }
        void registrarPago(Decimal total)
        {
            AccesoDatos obj = new AccesoDatos();
            Decimal monto = total;
            Decimal corriente = 0;
            Decimal adicional = 0;

            if (!Global.Multipago.ADICIONAL)
            {
                corriente = monto;
                adicional = 0;
            }
            else
            {
                corriente = monto / 1.10M;
                adicional = monto / 1.10M * 0.10M;
            }
            corriente = Convert.ToDecimal(string.Format("{0:0.00}", corriente));
            adicional = Convert.ToDecimal(string.Format("{0:0.00}", adicional));
            string contribuyente = Global.Acta.CNOMBREREG + " " + Global.Acta.CAPE1REG + "" + Global.Acta.CAPE2REG;
            string rfc = "";
            string domicilio = "";
            string cfdi = "NO";

            RadMessageBox.SetThemeName("TelerikMetroTouch");
            DialogResult result = RadMessageBox.Show(this, "¿Va a requerir factura?", "KIOSCO", MessageBoxButtons.YesNo, RadMessageIcon.Question);

            if (result == DialogResult.Yes)
            {
                frmBusqueda = new BusquedaContribuyente();
                frmBusqueda.ShowDialog(this);
                rfc = "GADW010101000";
                domicilio = "DOMICILIO DE WILBER";
                cfdi = "SI";
            }
            else
            {
                rfc = "XAXX010101000";
                domicilio = "EJEMPLO DE DOMICILIO";
            }
            obj.RegistrarPagoActa(new PagoActa()
            {
                IDPAGOVARIO = Global.Multipago.IDPAGO,
                CORRIENTE = corriente,
                ADICIONAL = adicional,
                TOTAL = monto,
                IMPORTE_LETRA = ImporteLetra.NumPalabra(total),
                CONTRIBUYENTE = contribuyente,
                RFC = rfc,
                DOMICILIO= domicilio,
                CFDI = cfdi,
                PERIODO_ACTUAL = DateTime.Now.Year
            });
            

        }
        #endregion

        void AsignaFormulario(ActaNacimientoBE acta)
        {
            Global.Multipago = GetMultipago("ECA");//Expedición de copias de actas

                //Van asignados todos los campos en el formulario....
            lblCurpAnDVP.Text = acta.CURP.ToString();
            lblEntidadRegAnDVP.Text = acta.CDES_EFR.ToString();
            lblMunRegAnDVP.Text = acta.CMUNNACREG.ToString();
            lblFechaRegAnDVP.Text = acta.DFECHA_REG.ToString();
            lblLibroAnDVP.Text = acta.NLIBRO.ToString();
            lblNumActaAnDVP.Text = acta.CACTA.ToString();
            lblNombrePrDVP.Text = acta.CNOMBREREG.ToString();
            lblPrimerApellidoPrDVP.Text = acta.CAPE1REG.ToString();
            lblSegApellidoPrDVP.Text = acta.CAPE2REG.ToString();
            lblFechaNacPrDVP.Text = acta.DFECHANAC.ToString();
            lblLugNacPrDVP.Text = acta.CLOCNACREG.ToString();
            lblSexoPrDVP.Text = acta.SEXO.ToString();
            //-------------------
            lblNombrePadreFprDVP.Text = acta.CNOMPADRE.ToString();
            lblApellido1PadreFprDVP.Text = acta.CAPE1PADRE.ToString();
            lblApellido2PadreFprDVP.Text = acta.CAPE2PADRE.ToString();
            lblNacionalidadPadreFprDVP.Text = acta.DES_NACPA.ToString();
            lblNombreMadreFprDVP.Text = acta.CNOMMADRE.ToString();
            lblApellido1MadreFprDVP.Text = acta.CAPE1MADRE.ToString();
            lblApellido2MadreFprDVP.Text = acta.CAPE2MADRE.ToString();
            lblNacionalidadMadreFprDVP.Text = acta.DES_NACMADRE.ToString();
            txtanotacionesMarginalesDVP.Text = acta.ANOTMARG.ToString();
            //------------------------------------------
            lblDatActaPICurp.Text = acta.CURP.ToString();
            lblDatActaPINombre.Text = acta.CNOMBREREG.ToString();
            lblDatActaPIApellido1.Text = acta.CAPE1REG.ToString();
            lblDatActaPIApellido2.Text = acta.CAPE2REG.ToString();
            txtMontoPagar.Text = Global.Multipago.A_PAGAR.ToString("#,#0.00");
            lblEstatusCambio.Text = "...";
        }
    }
}
