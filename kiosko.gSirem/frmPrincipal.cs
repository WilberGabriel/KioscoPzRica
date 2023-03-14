using com.sun.activation.registries;
using com.sun.corba.se.impl.orbutil.graph;
using kiosko.gSirem.Properties;
using org.apache.logging.log4j.core.appender.routing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kiosko.gSirem
{
    public partial class frmPrincipal : Form
    {
        frmActaNacimiento frmActa = null;
        public frmPrincipal()
        {
            InitializeComponent(); string cad = ConfigurationManager.AppSettings["sqlCad"].ToString().Trim();
            Global.SqlCad = cad.ToString();
            btnSalir.Visible = false;
        }

        //METODO PARA ABRIR FORM DENTRO DE PANEL-----------------------------------------------------
        private void OpenFormIntoPanel(object formHijo)
        {
            btnSalir.Visible = true;
            if (this.mainpanel.Controls.Contains((Control)formHijo))
            {
                Form fh = formHijo as Form;
                fh.TopLevel = false;
                fh.FormBorderStyle = FormBorderStyle.None;
                fh.Dock = DockStyle.Fill;
                this.mainpanel.Tag = fh;
                fh.Show();
            }
            else
            {
                if (this.mainpanel.Controls.Count > 0)
                    this.mainpanel.Controls.RemoveAt(0);
                Form fh = formHijo as Form;
                fh.TopLevel = false;
                fh.FormBorderStyle = FormBorderStyle.None;
                fh.Dock = DockStyle.Fill;

                this.mainpanel.Controls.Add(fh);
                this.mainpanel.Tag = fh;
                fh.Show();
            }
        }

        private void radTileElement1_Click(object sender, EventArgs e)
        {
            KioskoHome frmk = new KioskoHome();
            frmk.ShowDialog();
        }

        private void radTileElement2_Click(object sender, EventArgs e)
        {
            if (frmActa == null)
            {
                frmActa = new frmActaNacimiento();
                frmActa.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            }
            OpenFormIntoPanel(frmActa);
        }
        private void MostrarFormLogo()
        {
            OpenFormIntoPanel(new frmLogo());
            btnSalir.Visible = false;
        }

        //METODO PARA MOSTRAR FORMULARIO DE LOGO Al CERRAR OTROS FORM ----------------------------------------------------------
        private void MostrarFormLogoAlCerrarForms(object sender, FormClosedEventArgs e)
        {
            MostrarFormLogo();
            if ((Control)sender == (Control)frmActa)
                frmActa = null;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (frmActa != null)
                frmActa.Close();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            MostrarFormLogo();
        }
        public void changeProcesando(bool visible, string msg = "")
        {
            lblProcesando.Text = msg;
            lblProcesando.Visible = visible;
        }
    }
}
