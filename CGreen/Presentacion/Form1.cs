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

        private void button1_Click(object sender, EventArgs e)
        {
            p.selectP();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            v.selectV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vt.selectV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pr.selectP();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            i.selectI(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            vt.selectVP(2);
        }
    }
}
