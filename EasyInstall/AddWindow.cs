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
    public partial class AddWindow : Form
    {
        public bool okpressed;
        public string programmname;
        public string discriprion;
        public string url;
        public AddWindow()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            programmname = tbprogrammname.Text;
            discriprion = tbdiscription.Text;
            url = tburl.Text;

            okpressed = true;
            Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            okpressed = false;
            Close();
        }
    }
}
