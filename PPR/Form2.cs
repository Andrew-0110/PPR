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
    public partial class Form2 : Form
    {
        private int index;
        Connection con = new Connection();
        public Form2()
        {
            InitializeComponent();
            dataGridView1.DataSource = con.GetAllShops();
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            button2.Visible = true;
            button1.Visible = false;
            button3.Visible = false;
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Нельзя ввести пустое значение!", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (con.InsertShop(textBox1.Text))
                {
                    MessageBox.Show("Запись успешно добавлена!", "СООБЩЕНИЕ!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Clear();
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении записи!", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dataGridView1.DataSource = con.GetAllShops();
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            index = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Нельзя обновить запись пустым значением!", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (con.UpdateShop(index, textBox1.Text))
            {
                MessageBox.Show("Запись успешно обновлена!", "СООБЩЕНИЕ!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Clear();
                dataGridView1.DataSource = con.GetAllShops();
            }
            else
            {
                MessageBox.Show("Ошибка при редактировании записи!", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Нельзя удалить пустое значение!", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (con.DeleteShop(index))
            {
                MessageBox.Show("Запись успешно удалена!", "СООБЩЕНИЕ!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Clear();
                dataGridView1.DataSource = con.GetAllShops();
            }
            else
            {
                MessageBox.Show("Ошибка при удалении записи!", "ОШИБКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
