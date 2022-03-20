using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CGreen.Logica;

namespace CGreen.Presentacion
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            listBox2.Items.Add(listBox1.SelectedItem);
            listBox1.Items.Remove(listBox1.SelectedItem);
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Items.Add(listBox2.SelectedItem);
            listBox2.Items.Remove(listBox2.SelectedItem);
            if(listBox2.Items.Count <= 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            L_Punto p = new L_Punto();
            DataTable dt = new DataTable();
            dt = p.selectP();
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr.ItemArray[1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reports.FormReport1 f = new Reports.FormReport1();
            for(int i = 0; i < listBox2.Items.Count; i++)
            {
                f.ReportNames.Add(listBox2.Items[i].ToString());
            }
            f.fecha1 = dateTimePicker1.Value;
            f.fecha2 = dateTimePicker2.Value;
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reports.FormReport2 f = new Reports.FormReport2();
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                f.ReportNames.Add(listBox2.Items[i].ToString());
            }
            f.fecha1 = dateTimePicker1.Value;
            f.fecha2 = dateTimePicker2.Value;
            f.Show();
        }
    }
}
