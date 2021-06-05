using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPR
{
    public partial class Form3 : Form
    {
        Connection con = new Connection();
        public Form3()
        {
            InitializeComponent();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = con.GetAllEquipment();
            dataGridView1.Columns[1].Visible = false;
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Невозможно осуществить поиск по пустому значению!", "ИНФОРМАЦИЯ!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                dataGridView1.DataSource = con.Search(Convert.ToInt32(textBox1.Text));
        }
    }
}
