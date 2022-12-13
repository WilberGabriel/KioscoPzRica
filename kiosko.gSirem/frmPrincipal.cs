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
        public frmPrincipal()
        {
            InitializeComponent();
        }

        public void loadform(object Form)
        {
           string cad = ConfigurationManager.AppSettings["sqlCad"].ToString().Trim();
            Global.SqlCad = cad.ToString();

            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        private void radTileElement1_Click(object sender, EventArgs e)
        {
            KioskoHome frmk = new KioskoHome();
            frmk.ShowDialog();
        }

        private void radTileElement2_Click(object sender, EventArgs e)
        {
            loadform(new frmActaNacimiento());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
