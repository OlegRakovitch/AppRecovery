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
        DataSet localDB = new DataSet();
        string name = "data";
        public MainWindow()
        {
            InitializeComponent();

            localDB.Tables.Add(name);
            localDB.Tables[name].Columns.Add("Programm name", typeof(string));
            localDB.Tables[name].Columns.Add("Discriprion", typeof(string));
            localDB.Tables[name].Columns.Add("Url", typeof(string));
            dataGridView1.DataSource = localDB.Tables[name];

        }

        private void AddProgramm()
        {
            var AddWindow = new AddWindow();
            AddWindow.ShowDialog();
            if (AddWindow.okpressed == true)
            {
                localDB.Tables[name].Rows.Add(AddWindow.programmname, AddWindow.discriprion, AddWindow.url);
            }
        }

        private void btnplus_Click(object sender, EventArgs e)
        {
            AddProgramm();
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
            
            if (dataGridView1.SelectedCells.Count != 0 && !dataGridView1.SelectedCells[0].OwningRow.IsNewRow)
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedCells[0].OwningRow);
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
