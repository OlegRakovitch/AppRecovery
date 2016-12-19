using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyInstall
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnregister_Click(object sender, EventArgs e)
        {
            var RegisterWindow = new RegisterWindow();
            RegisterWindow.ShowDialog();
        }

        private void btnlofin_Click(object sender, EventArgs e)
        {
            var linkform = new MainWindow();
            linkform.ShowDialog();
        }
    }
}
