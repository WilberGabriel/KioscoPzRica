using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kiosko.gSirem.clases
{
    public static class ImporteLetra
    {
        private static string cMontext;
        private static string Centena(int uni, int dec, int cen)
        {
            int num = cen;
            switch (num)
            {
                case 1:
                    {
                        bool flag = dec + uni != 0;
                        if (flag)
                        {
                            cMontext = "CIENTO ";
                        }
                        else
                        {
                            cMontext = "CIEN ";
                        }
                        break;
                    }
                case 2:
                    {
                        cMontext = "DOSCIENTOS ";
                        break;
                    }
                case 3:
                    {
                        cMontext = "TRESCIENTOS ";
                        break;
                    }
                case 4:
                    {
                        cMontext = "CUATROCIENTOS ";
                        break;
                    }
                case 5:
                    {
                        cMontext = "QUINIENTOS ";
                        break;
                    }
                case 6:
                    {
                        cMontext = "SEISCIENTOS ";
                        break;
                    }
                case 7:
                    {
                        cMontext = "setecientos ";
                        break;
                    }
                case 8:
                    {
                        cMontext = "ochocientos ";
                        break;
                    }
                case 9:
                    {
                        cMontext = "novecientos ";
                        break;
                    }
                default:
                    {
                        cMontext = "";
                        break;
                    }
            }
            string str = cMontext;
            return str;
        }

        private static string Decena(int uni, int dec)
        {
            bool flag;
            int num = dec;
            switch (num)
            {
                case 1:
                    {
                        num = uni;
                        switch (num)
                        {
                            case 0:
                                {
                                    cMontext = "diez ";
                                    break;
                                }
                            case 1:
                                {
                                    cMontext = "once ";
                                    break;
                                }
                            case 2:
                                {
                                    cMontext = "doce ";
                                    break;
                                }
                            case 3:
                                {
                                    cMontext = "trece ";
                                    break;
                                }
                            case 4:
                                {
                                    cMontext = "catorce ";
                                    break;
                                }
                            case 5:
                                {
                                    cMontext = "quince ";
                                    break;
                                }
                            case 6:
                                {
                                    cMontext = "dieci";
                                    break;
                                }
                            case 7:
                                {
                                    cMontext = "dieci";
                                    break;
                                }
                            case 8:
                                {
                                    cMontext = "dieci";
                                    break;
                                }
                            case 9:
                                {
                                    cMontext = "dieci";
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        flag = uni != 0;
                        if (flag)
                        {
                            flag = uni <= 0;
                            if (!flag)
                            {
                                cMontext = "veinti";
                            }
                        }
                        else
                        {
                            cMontext = "veinte ";
                        }
                        break;
                    }
                case 3:
                    {
                        cMontext = "treinta ";
                        break;
                    }
                case 4:
                    {
                        cMontext = "cuarenta ";
                        break;
                    }
                case 5:
                    {
                        cMontext = "cincuenta ";
                        break;
                    }
                case 6:
                    {
                        cMontext = "sesenta ";
                        break;
                    }
                case 7:
                    {
                        cMontext = "setenta ";
                        break;
                    }
                case 8:
                    {
                        cMontext = "ochenta ";
                        break;
                    }
                case 9:
                    {
                        cMontext = "noventa ";
                        break;
                    }
                default:
                    {
                        cMontext = "";
                        break;
                    }
            }
            flag = !(uni > 0 & dec > 2);
            if (!flag)
            {
                cMontext = string.Concat(cMontext, "y ");
            }
            string str = cMontext;
            return str;
        }
        private static string Unidad(int uni, int dec)
        {
            int num;
            cMontext = "";
            bool flag = dec == 1;
            if (!flag)
            {
                num = uni;
                switch (num)
                {
                    case 1:
                        {
                            cMontext = "un ";
                            break;
                        }
                    case 2:
                        {
                            cMontext = "dos ";
                            break;
                        }
                    case 3:
                        {
                            cMontext = "tres ";
                            break;
                        }
                    case 4:
                        {
                            cMontext = "cuatro ";
                            break;
                        }
                    case 5:
                        {
                            cMontext = "cinco ";
                            break;
                        }
                }
            }
            num = uni;
            switch (num)
            {
                case 6:
                    {
                        cMontext = "seis ";
                        break;
                    }
                case 7:
                    {
                        cMontext = "siete ";
                        break;
                    }
                case 8:
                    {
                        cMontext = "ocho ";
                        break;
                    }
                case 9:
                    {
                        cMontext = "nueve ";
                        break;
                    }
            }
            string str = cMontext;
            return str;
        }

        public static string NumPalabra(Decimal NumeroDecimal)
        {
            double Numero = (double)NumeroDecimal;
            bool num;
            string[] strArrays;
            int centenas = 1;
            int pos = 0;
            string textuni = "";
            string textdec = "";
            string textcen = "";
            string milestxt = "";
            string monedatxt = "";
            string txtPalabra = "";
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            string Numerofmt = Numero.ToString("000000000000000.00");
            txtPalabra = "";
            while (true)
            {
                num = centenas <= 5;
                if (!num)
                {
                    break;
                }
                int cen = Convert.ToInt32(Numerofmt.Substring(pos, 1));
                int dec = Convert.ToInt32(Numerofmt.Substring(pos + 1, 1));
                int uni = Convert.ToInt32(Numerofmt.Substring(pos + 2, 1));
                pos = pos + 3;
                textcen = Centena(uni, dec, cen);
                textdec = Decena(uni, dec);
                textuni = Unidad(uni, dec);
                int num1 = centenas;
                switch (num1)
                {
                    case 1:
                        {
                            num = cen + dec + uni != 1;
                            if (num)
                            {
                                num = cen + dec + uni <= 1;
                                if (!num)
                                {
                                    milestxt = "Billones ";
                                }
                            }
                            else
                            {
                                milestxt = "Billon ";
                            }
                            break;
                        }
                    case 2:
                        {
                            num = !(cen + dec + uni >= 1 & Convert.ToDouble(Numerofmt.Substring(7, 3)) == 0);
                            if (num)
                            {
                                num = cen + dec + uni < 1;
                                if (!num)
                                {
                                    milestxt = "Mil ";
                                }
                            }
                            else
                            {
                                milestxt = "Mil Millones ";
                            }
                            break;
                        }
                    case 3:
                        {
                            num = !(cen + dec == 0 & uni == 1);
                            if (num)
                            {
                                num = !(cen > 0 | dec > 0 | uni > 1);
                                if (!num)
                                {
                                    milestxt = "Millones ";
                                }
                            }
                            else
                            {
                                milestxt = "Millon ";
                            }
                            break;
                        }
                    case 4:
                        {
                            num = cen + dec + uni < 1;
                            if (!num)
                            {
                                milestxt = "Mil ";
                            }
                            break;
                        }
                    case 5:
                        {
                            num = cen + dec + uni < 1;
                            if (!num)
                            {
                                milestxt = "";
                            }
                            break;
                        }
                }
                centenas++;
                strArrays = new string[] { txtPalabra, textcen, textdec, textuni, milestxt };
                txtPalabra = string.Concat(strArrays);
                milestxt = "";
                textuni = "";
                textdec = "";
                textcen = "";
            }
            num = Convert.ToDouble(Numerofmt) != 0;
            if (num)
            {
                num = Convert.ToDouble(Numerofmt) != 1;
                if (num)
                {
                    num = Convert.ToDouble(Numerofmt) >= 1000000;
                    if (num)
                    {
                        num = Convert.ToDouble(Numerofmt) < 1000000;
                        if (!num)
                        {
                            monedatxt = "de Pesos ";
                        }
                    }
                    else
                    {
                        monedatxt = "Pesos ";
                    }
                }
                else
                {
                    monedatxt = "Peso ";
                }
            }
            else
            {
                monedatxt = "Cero Pesos ";
            }
            strArrays = new string[] { txtPalabra, monedatxt, Numerofmt.Substring(16), "/100 M.N." };
            txtPalabra = string.Concat(strArrays);
            string upper = txtPalabra.ToUpper();
            return upper;
        }
    }
}
