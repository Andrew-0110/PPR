using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPR
{
    public partial class Form1 : Form
    {
        Connection con = new Connection();
       
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowHeadersVisible = false;
           dataGridView1.DataSource = con.GetAllEquipment();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
             dataGridView1.Columns[1].Visible = false;
            /*dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[23].Visible = false;*/
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Equipment equipment = new Equipment();
            int year = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                equipment.indexNameEquip = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                equipment.indexEqip = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                equipment.Typeofrepair = dataGridView1.Rows[i].Cells[8].Value.ToString();
                equipment.dateofrepair = Convert.ToDateTime(dataGridView1.Rows[i].Cells[9].Value.ToString());
                if (year - 1 == equipment.dateofrepair.Year)
                {
                   
                    con.DoCount(equipment, year);
                }
            }
            dataGridView1.DataSource = con.GetAllEquipment();
        }

        private void правкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void редакторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void цехаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void месячныйППРToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }
        struct DataParameter
        {
            public string FileName { get; set; }
        }
        DataParameter inputParameter;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string fileName = ((DataParameter)e.Argument).FileName;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)excel.ActiveSheet;
            int index = 1;
            int progress = dataGridView1.RowCount;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (!backgroundWorker1.CancellationPending)
                {
                    backgroundWorker1.ReportProgress(index++ * 100 / progress);
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                             ws.Cells[i+1, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    
                }
            }
            ws.SaveAs(fileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            excel.Quit();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label2.Text = string.Format("Прогресс...{0}", e.ProgressPercentage);
            progressBar1.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(100);
                label2.Text = "Все ОК!!!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                return;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    inputParameter.FileName = sfd.FileName;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    backgroundWorker1.RunWorkerAsync(inputParameter);
                }
            }
        }
    }
}
