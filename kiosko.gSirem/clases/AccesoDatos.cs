using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.math;
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

        public ActaNacimientoBE GetCurpByDP(string curp, string nombre, string paterno, string materno, int tipo)
        {
            string sqlcad = Global.SqlCad;
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(sqlcad.ToString());
            Int16 r = 0;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                ActaNacimientoBE ol = new ActaNacimientoBE();

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
                    DataRow dr = ds.Tables[0].Rows[0];

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

                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _errorMensaje = "Error: " + _errorMensaje + ", " + ex.Message.ToString();
                }
                return ol;
            }
        }

        public ActaNacimientoBE GetDatosPersonalesRC(string nombre, string paterno, string materno, string fNacimiento, string sexo, string edoNacimiento)
        {
            string sqlcad = Global.SqlCad;
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(sqlcad.ToString());

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                ActaNacimientoBE ol = new ActaNacimientoBE();

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
                    DataRow dr= ds.Tables[0].Rows[0];

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

                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _errorMensaje = "Error: " + _errorMensaje + ", " + ex.Message.ToString();
                }
                return ol;
            }


        }
        public Multipago GetMultipago(string letra)
        {
            string sqlcad = Global.SqlCad;
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(sqlcad.ToString());

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                Multipago mul = new Multipago();

                DbCommand dbc = db.GetStoredProcCommand("kiosco.MultipagoGET");
                db.AddInParameter(dbc, "@LETRAS", System.Data.DbType.String, letra.ToString().ToUpper());
                try
                {
                    DataSet ds = db.ExecuteDataSet(dbc);
                    DataRow dr = ds.Tables[0].Rows[0];

                    mul.IDPAGO = Convert.ToInt64(dr["ID_PAGOVARIO"].ToString().Trim());
                    mul.A_PAGAR = Convert.ToDecimal(dr["A_PAGAR"].ToString().Trim());
                    mul.CUENTA = dr["CUENTA"].ToString().Trim();
                    mul.ADICIONAL = Convert.ToBoolean(dr["CONADICIONAL"].ToString().Trim());
                    mul.REDONDEO = Convert.ToBoolean(dr["REDONDEO"].ToString().Trim());

                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    _errorMensaje = "Error: " + _errorMensaje + ", " + ex.Message.ToString();
                }
                return mul;
            }

        }

        public bool RegistrarPagoActa(PagoActa objCat)
        {
            string sqlcad = Global.SqlCad;
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(sqlcad.ToString());

            _errorMensaje = "";
            bool r = false;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand dbc = db.GetStoredProcCommand("kiosco.cobroPagoActa");
                    db.AddOutParameter(dbc, "@folioactual", System.Data.DbType.Int32, 9);
                    db.AddOutParameter(dbc, "@error", System.Data.DbType.String, 1024);

                    db.AddInParameter(dbc, "@idpagovario", System.Data.DbType.Int64, objCat.IDPAGOVARIO);
                    db.AddInParameter(dbc, "@corriente", System.Data.DbType.Decimal, objCat.CORRIENTE);
                    db.AddInParameter(dbc, "@adicional", System.Data.DbType.Decimal, objCat.ADICIONAL);
                    db.AddInParameter(dbc, "@total", System.Data.DbType.Decimal, objCat.TOTAL);
                    db.AddInParameter(dbc, "@importeletra", System.Data.DbType.String, objCat.IMPORTE_LETRA);
                    db.AddInParameter(dbc, "@contribuyente", System.Data.DbType.String, objCat.CONTRIBUYENTE);
                    db.AddInParameter(dbc, "@rfc", System.Data.DbType.String, objCat.RFC);
                    db.AddInParameter(dbc, "@domicilio", System.Data.DbType.String, objCat.DOMICILIO);
                    db.AddInParameter(dbc, "@cfdi", System.Data.DbType.String, objCat.CFDI);
                    db.AddInParameter(dbc, "@periodoactual", System.Data.DbType.String, objCat.PERIODO_ACTUAL);

                    db.ExecuteNonQuery(dbc, trans);

                    trans.Commit();
                    
                    r = db.GetParameterValue(dbc, "@error").ToString()=="ok";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    r = false;
                    _errorMensaje = "Error: " + _errorMensaje + ", " + ex.Message.ToString();
                }
                conn.Close();
                return r;
            }
        }
    }
}
