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
    public partial class Form4 : Form
    {
        Connection con = new Connection();
        public Form4()
        {
            InitializeComponent();
            label1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = button1.Text;
            label1.Visible = true;
            int month = 2;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = button2.Text;
            label1.Visible = true;
            int month = 3;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = button3.Text;
            label1.Visible = true;
            int month = 4;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = button4.Text;
            label1.Visible = true;
            int month = 5;
           dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label1.Text = button5.Text;
            label1.Visible = true;
            int month = 6;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label1.Text = button6.Text;
            label1.Visible = true;
            int month = 7;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label1.Text = button7.Text;
            label1.Visible = true;
            int month = 8;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label1.Text = button8.Text;
            label1.Visible = true;
            int month = 9;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label1.Text = button9.Text;
            label1.Visible = true;
            int month = 10;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label1.Text = button10.Text;
            label1.Visible = true;
            int month = 11;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label1.Text = button11.Text;
            label1.Visible = true;
            int month = 12;
            dataGridView1.DataSource = con.GetEquipmentByMonth(month, Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "pPRDataSet.Shop". При необходимости она может быть перемещена или удалена.
            this.shopTableAdapter.Fill(this.pPRDataSet.Shop);

        }
    }
}
