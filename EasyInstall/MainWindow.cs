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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnplus_Click(object sender, EventArgs e)
        {
            var AddWindow = new AddWindow();
            AddWindow.ShowDialog();
        }

        private void btninstall_Click(object sender, EventArgs e)
        {
            var InstallWindow = new InstallWindow();
            InstallWindow.ShowDialog();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            var EditWindow = new AddWindow();
            EditWindow.ShowDialog();
        }

        private void btnminus_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
