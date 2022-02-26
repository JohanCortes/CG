using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CGreen.Logica;

namespace CGreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        L_Punto p = new L_Punto();
        L_Vendedor v = new L_Vendedor();
        L_Venta vt = new L_Venta();
        L_Producto pr = new L_Producto();
        L_Inventario i = new L_Inventario();

        void imprimir(DataTable t)
        {
            if (t.Rows.Count > 0)
            {
                string res = "";
                foreach (DataColumn col in t.Columns)
                {
                    res += (col.ColumnName)[0].ToString().ToUpper() + col.ColumnName.Remove(0,1) + "\u0009";
                }
                res += "\u000d\u000a\u000d\u000a";
                foreach (DataRow reg in t.Rows)
                {
                    
                    foreach (object dato in reg.ItemArray)
                    {
                        res += dato + "\u0009";
                    }
                    res = res.Remove(res.Length - 1);
                    res += "\u000d\u000a";
                }
                res = res.Remove(res.Length - 2);
                textBox1.Text = res;
            }
            else
            {
                textBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imprimir(p.selectP());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imprimir(v.selectV());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imprimir(vt.selectV());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imprimir(pr.selectP());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            imprimir(i.selectI(1));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            imprimir(vt.selectVP(2));
        }
    }
}
