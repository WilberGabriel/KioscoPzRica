using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiosko.gSirem
{
   public class Multipago
    {
        Int64 _IDPAGO;
        Decimal _APAGAR;
        String _CUENTA;
        Boolean _ADICIONAL;

        public Int64 IDPAGO { get => _IDPAGO; set => _IDPAGO = value; }
        public Decimal A_PAGAR { get => _APAGAR; set => _APAGAR = value; }
        public string CUENTA { get => _CUENTA; set => _CUENTA = value; }
        public Boolean ADICIONAL { get => _ADICIONAL; set => _ADICIONAL = value; }
    }
   public class ActaNacimientoBE
    {
        Int16 existeCurp;
        String _S_IDDOCUMENTO, _ID, _CCRIP, _CURP, _NANIO, _NTOMO, _NLIBRO, _FOJA, _CACTA, _DFECHA_REG,
                 _CNOMBREREG, _CAPE1REG, _CAPE2REG, _SEXO, _DFECHANAC, _CHORANAC, _CLOCNACREG, _CMUNNACREG,
                 _CDES_EFR, _CPAISNACRE, _CCOMPARECE, _REGISVM, _CNOMPADRE, _CAPE1PADRE, _CAPE2PADRE,
                 _CEDADPADRE, _DES_NACPA, _CNOMMADRE, _CAPE1MADRE, _CAPE2MADRE, _CEDADMADRE, _DES_NACMADRE, _ANOTMARG;

        public string S_IDDOCUMENTO { get => _S_IDDOCUMENTO; set => _S_IDDOCUMENTO = value; }
        public string ID { get => _ID; set => _ID = value; }
        public string CCRIP { get => _CCRIP; set => _CCRIP = value; }
        public string CURP { get => _CURP; set => _CURP = value; }
        public string NANIO { get => _NANIO; set => _NANIO = value; }
        public string NTOMO { get => _NTOMO; set => _NTOMO = value; }
        public string NLIBRO { get => _NLIBRO; set => _NLIBRO = value; }
        public string FOJA { get => _FOJA; set => _FOJA = value; }
        public string CACTA { get => _CACTA; set => _CACTA = value; }
        public string DFECHA_REG { get => _DFECHA_REG; set => _DFECHA_REG = value; }
        public string CNOMBREREG { get => _CNOMBREREG; set => _CNOMBREREG = value; }
        public string CAPE1REG { get => _CAPE1REG; set => _CAPE1REG = value; }
        public string CAPE2REG { get => _CAPE2REG; set => _CAPE2REG = value; }
        public string SEXO { get => _SEXO; set => _SEXO = value; }
        public string DFECHANAC { get => _DFECHANAC; set => _DFECHANAC = value; }
        public string CHORANAC { get => _CHORANAC; set => _CHORANAC = value; }
        public string CLOCNACREG { get => _CLOCNACREG; set => _CLOCNACREG = value; }
        public string CMUNNACREG { get => _CMUNNACREG; set => _CMUNNACREG = value; }
        public string CDES_EFR { get => _CDES_EFR; set => _CDES_EFR = value; }
        public string CPAISNACRE { get => _CPAISNACRE; set => _CPAISNACRE = value; }
        public string CCOMPARECE { get => _CCOMPARECE; set => _CCOMPARECE = value; }
        public string REGISVM { get => _REGISVM; set => _REGISVM = value; }
        public string CNOMPADRE { get => _CNOMPADRE; set => _CNOMPADRE = value; }
        public string CAPE1PADRE { get => _CAPE1PADRE; set => _CAPE1PADRE = value; }
        public string CAPE2PADRE { get => _CAPE2PADRE; set => _CAPE2PADRE = value; }
        public string CEDADPADRE { get => _CEDADPADRE; set => _CEDADPADRE = value; }
        public string DES_NACPA { get => _DES_NACPA; set => _DES_NACPA = value; }
        public string CNOMMADRE { get => _CNOMMADRE; set => _CNOMMADRE = value; }
        public string CAPE1MADRE { get => _CAPE1MADRE; set => _CAPE1MADRE = value; }
        public string CAPE2MADRE { get => _CAPE2MADRE; set => _CAPE2MADRE = value; }
        public string CEDADMADRE { get => _CEDADMADRE; set => _CEDADMADRE = value; }
        public string DES_NACMADRE { get => _DES_NACMADRE; set => _DES_NACMADRE = value; }
        public string ANOTMARG { get => _ANOTMARG; set => _ANOTMARG = value; }
        public short ExisteCurp { get => existeCurp; set => existeCurp = value; }
    }
}
