using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace kiosko.gSirem
{
    public class AccesoDatos
    {
        private string _errorMensaje;
        public string ErrorMensaje
        {
            get { return _errorMensaje; }
            set { _errorMensaje = value; }
        }

        public Int16 GetExisteCURP(string curp, int tipo)
        {
            string sqlcad = Global.SqlCad;
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(sqlcad.ToString());
            Int16 r = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                //List<ActaNacimientoBE> listaCat = new List<ActaNacimientoBE>();

                //IDPropietario= 0, significa que quiero todo el catalogo completo
                DbCommand dbc = db.GetStoredProcCommand("ingreso.busquedaCurpRC");
                db.AddInParameter(dbc, "@tipo", System.Data.DbType.Int32, tipo);
                db.AddInParameter(dbc, "@curp", System.Data.DbType.String, curp.ToString().ToUpper());

                try
                {
                    // DataSet ds = new DataSet(); ds.Tables.Clear();
                    DataSet ds = db.ExecuteDataSet(dbc);
                    foreach (DataRow drInfo in ds.Tables[0].Rows)
                    {
                        r = Convert.ToInt16(drInfo["existeCurp"]);
                    }
                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _errorMensaje = "Error: " + _errorMensaje + ", " + ex.Message.ToString();
                }



                return r;
            }
        }

        public List<ActaNacimientoBE> GetCurpByDP(string curp, string nombre, string paterno, string materno, int tipo)
        {
            string sqlcad = Global.SqlCad;
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(sqlcad.ToString());
            Int16 r = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                List<ActaNacimientoBE> lAC = new List<ActaNacimientoBE>();

                //IDPropietario= 0, significa que quiero todo el catalogo completo
                DbCommand dbc = db.GetStoredProcCommand("ingreso.busquedaCurpRC");
                db.AddInParameter(dbc, "@tipo", System.Data.DbType.Int32, tipo);
                db.AddInParameter(dbc, "@curp", System.Data.DbType.String, curp.ToString().ToUpper());
                db.AddInParameter(dbc, "@nombre", System.Data.DbType.String, nombre.ToString().ToUpper());
                db.AddInParameter(dbc, "@aPaterno", System.Data.DbType.String, paterno.ToString().ToUpper());
                db.AddInParameter(dbc, "@aMaterno", System.Data.DbType.String, materno.ToString().ToUpper());

                try
                {
                    // DataSet ds = new DataSet(); ds.Tables.Clear();
                    DataSet ds = db.ExecuteDataSet(dbc);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ActaNacimientoBE ol = new ActaNacimientoBE();

                        ol.S_IDDOCUMENTO = dr["S_IDDOCUMENTO"].ToString().Trim();

                        ol.ID = dr["ID"].ToString().Trim();
                        ol.CCRIP = dr["CCRIP"].ToString().Trim();
                        ol.CURP = dr["CURP"].ToString().Trim();
                        ol.NANIO = dr["NANIO"].ToString().Trim();
                        ol.NTOMO = dr["NTOMO"].ToString().Trim();
                        ol.NLIBRO = dr["NLIBRO"].ToString().Trim();
                        ol.FOJA = dr["FOJA"].ToString().Trim();
                        ol.CACTA = dr["CACTA"].ToString().Trim();
                        ol.DFECHA_REG = dr["DFECHA_REG"].ToString().Trim();
                        ol.CNOMBREREG = dr["CNOMBREREG"].ToString().Trim();
                        ol.CAPE1REG = dr["CAPE1REG"].ToString().Trim();
                        ol.CAPE2REG = dr["CAPE2REG"].ToString().Trim();
                        ol.SEXO = dr["SEXO"].ToString().Trim();
                        ol.DFECHANAC = dr["DFECHANAC"].ToString().Trim();
                        ol.CHORANAC = dr["CHORANAC"].ToString().Trim();
                        ol.CLOCNACREG = dr["CLOCNACREG"].ToString().Trim();
                        ol.CMUNNACREG = dr["CMUNNACREG"].ToString().Trim();
                        ol.CDES_EFR = dr["CDES_EFR"].ToString().Trim();
                        ol.CPAISNACRE = dr["CPAISNACRE"].ToString().Trim();
                        ol.CCOMPARECE = dr["CCOMPARECE"].ToString().Trim();
                        ol.REGISVM = dr["REGISVM"].ToString().Trim();
                        ol.CNOMPADRE = dr["CNOMPADRE"].ToString().Trim();
                        ol.CAPE1PADRE = dr["CAPE1PADRE"].ToString().Trim();
                        ol.CAPE2PADRE = dr["CAPE2PADRE"].ToString().Trim();
                        ol.CEDADPADRE = dr["CEDADPADRE"].ToString().Trim();

                        ol.DES_NACPA = dr["DES_NACPA"].ToString().Trim();
                        ol.CNOMMADRE = dr["CNOMMADRE"].ToString().Trim();
                        ol.CAPE1MADRE = dr["CAPE1MADRE"].ToString().Trim();
                        ol.CAPE2MADRE = dr["CAPE2MADRE"].ToString().Trim();
                        ol.CEDADMADRE = dr["CEDADMADRE"].ToString().Trim();
                        ol.DES_NACMADRE = dr["DES_NACMADRE"].ToString().Trim();
                        ol.ANOTMARG = dr["ANOTMARG"].ToString().Trim();


                        lAC.Add(ol);

                    }
                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _errorMensaje = "Error: " + _errorMensaje + ", " + ex.Message.ToString();
                }



                return lAC;
            }
        }

        public List<ActaNacimientoBE> GetDatosPersonalesRC(string nombre, string paterno, string materno, string fNacimiento, string sexo, string edoNacimiento)
        {
            string sqlcad = Global.SqlCad;
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(sqlcad.ToString());

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                List<ActaNacimientoBE> lAC = new List<ActaNacimientoBE>();

                //IDPropietario= 0, significa que quiero todo el catalogo completo
                DbCommand dbc = db.GetStoredProcCommand("ingreso.busquedaDatosPersonalesRC");
                db.AddInParameter(dbc, "@nombre", System.Data.DbType.String, nombre.ToString().ToUpper());
                db.AddInParameter(dbc, "@aPaterno", System.Data.DbType.String, paterno.ToString().ToUpper());
                db.AddInParameter(dbc, "@aMaterno", System.Data.DbType.String, materno.ToString().ToUpper());
                db.AddInParameter(dbc, "@fechaNac", System.Data.DbType.String, fNacimiento.ToString().ToUpper());
                db.AddInParameter(dbc, "@sexo", System.Data.DbType.String, sexo.ToString().ToUpper());
                db.AddInParameter(dbc, "@edoNac", System.Data.DbType.String, edoNacimiento.ToString().ToUpper());

                try
                {
                    // DataSet ds = new DataSet(); ds.Tables.Clear();
                    DataSet ds = db.ExecuteDataSet(dbc);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ActaNacimientoBE ol = new ActaNacimientoBE();

                        ol.S_IDDOCUMENTO = dr["S_IDDOCUMENTO"].ToString().Trim();

                        ol.ID = dr["ID"].ToString().Trim();
                        ol.CCRIP = dr["CCRIP"].ToString().Trim();
                        ol.CURP = dr["CURP"].ToString().Trim();
                        ol.NANIO = dr["NANIO"].ToString().Trim();
                        ol.NTOMO = dr["NTOMO"].ToString().Trim();
                        ol.NLIBRO = dr["NLIBRO"].ToString().Trim();
                        ol.FOJA = dr["FOJA"].ToString().Trim();
                        ol.CACTA = dr["CACTA"].ToString().Trim();
                        ol.DFECHA_REG = dr["DFECHA_REG"].ToString().Trim();
                        ol.CNOMBREREG = dr["CNOMBREREG"].ToString().Trim();
                        ol.CAPE1REG = dr["CAPE1REG"].ToString().Trim();
                        ol.CAPE2REG = dr["CAPE2REG"].ToString().Trim();
                        ol.SEXO = dr["SEXO"].ToString().Trim();
                        ol.DFECHANAC = dr["DFECHANAC"].ToString().Trim();
                        ol.CHORANAC = dr["CHORANAC"].ToString().Trim();
                        ol.CLOCNACREG = dr["CLOCNACREG"].ToString().Trim();
                        ol.CMUNNACREG = dr["CMUNNACREG"].ToString().Trim();
                        ol.CDES_EFR = dr["CDES_EFR"].ToString().Trim();
                        ol.CPAISNACRE = dr["CPAISNACRE"].ToString().Trim();
                        ol.CCOMPARECE = dr["CCOMPARECE"].ToString().Trim();
                        ol.REGISVM = dr["REGISVM"].ToString().Trim();
                        ol.CNOMPADRE = dr["CNOMPADRE"].ToString().Trim();
                        ol.CAPE1PADRE = dr["CAPE1PADRE"].ToString().Trim();
                        ol.CAPE2PADRE = dr["CAPE2PADRE"].ToString().Trim();
                        ol.CEDADPADRE = dr["CEDADPADRE"].ToString().Trim();

                        ol.DES_NACPA = dr["DES_NACPA"].ToString().Trim();
                        ol.CNOMMADRE = dr["CNOMMADRE"].ToString().Trim();
                        ol.CAPE1MADRE = dr["CAPE1MADRE"].ToString().Trim();
                        ol.CAPE2MADRE = dr["CAPE2MADRE"].ToString().Trim();
                        ol.CEDADMADRE = dr["CEDADMADRE"].ToString().Trim();
                        ol.DES_NACMADRE = dr["DES_NACMADRE"].ToString().Trim();
                        ol.ANOTMARG = dr["ANOTMARG"].ToString().Trim();


                        lAC.Add(ol);

                    }
                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _errorMensaje = "Error: " + _errorMensaje + ", " + ex.Message.ToString();
                }



                return lAC;
            }


        }
        public List<Multipago> GetMultipago(string letra)
        {
            string sqlcad = Global.SqlCad;
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(sqlcad.ToString());

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                List<Multipago> lisMultipago = new List<Multipago>();

                DbCommand dbc = db.GetStoredProcCommand("kiosco.MultipagoGET");
                db.AddInParameter(dbc, "@LETRAS", System.Data.DbType.String, letra.ToString().ToUpper());
                try
                {
                    DataSet ds = db.ExecuteDataSet(dbc);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Multipago mul = new Multipago();

                        mul.IDPAGO = Convert.ToInt64(dr["ID_PAGOVARIO"].ToString().Trim());
                        mul.A_PAGAR = Convert.ToDecimal(dr["A_PAGAR"].ToString().Trim());
                        mul.CUENTA = dr["CUENTA"].ToString().Trim();
                        mul.ADICIONAL = Convert.ToBoolean(dr["CONADICIONAL"].ToString().Trim());

                        lisMultipago.Add(mul);

                    }
                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _errorMensaje = "Error: " + _errorMensaje + ", " + ex.Message.ToString();
                }



                return lisMultipago;
            }

        }
    }
}
