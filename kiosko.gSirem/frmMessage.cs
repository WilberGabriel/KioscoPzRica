using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;


namespace kiosko.gSirem
{
    public partial class frmMessage : Telerik.WinControls.UI.RadForm
    {
        public frmMessage(string msg, string title, RadMessageIcon icon)
        {
            InitializeComponent();
            txtMessage.Text = msg;
            this.Text = title;
            this.picIcon.Image = GetRadMessageIcon(icon);
            txtMessage.Font = new Font("Segoe UI", 20);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private static Bitmap GetRadMessageIcon(RadMessageIcon icon)
        {
            switch (icon)
            {
                case RadMessageIcon.Info:
                    {
                        Bitmap result = Properties.Resources.question;
                        return result;
                    }
                case RadMessageIcon.Question:
                    {
                        Bitmap result = Properties.Resources.question;
                        return result;
                    }
                case RadMessageIcon.Exclamation:
                    {
                        Bitmap result = Properties.Resources.question;
                        return result;
                    }
                case RadMessageIcon.Error:
                    {
                        Bitmap result = Properties.Resources.question;
                        return result;
                    }
                default:
                    return null;
            }
        }
        public Bitmap MessageIcon
        {
            set
            {
                if (value != null)
                {
                    Bitmap bitmap = value;
                    if (bitmap.Size.Height > 32 || bitmap.Size.Width > 32)
                    {
                        bitmap = new Bitmap(bitmap, new Size(32, 32));
                    }

                    picIcon.Image = bitmap;
                    picIcon.Visible = true;
                }
                else
                {
                    picIcon.Image = null;
                    picIcon.Visible = false;
                }
            }
        }
    }
}
