﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CGreen.Logica;
using CGreen.Datos;

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
        L_Control c = new L_Control();
        L_Transaccion t = new L_Transaccion();

        void imprimir(DataTable t)
        {
            if (t.Rows.Count > 0)
            {
                string res = "";
                foreach (DataColumn col in t.Columns)
                {
                    res += col.ColumnName[0].ToString().ToUpper() + col.ColumnName.Remove(0,1) + "\t";
                }
                res += "\u000d\u000a";
                foreach (char c in res)
                {
                    if (c == 9)
                    {
                        res += "----";
                    }
                    res += "-";
                }
                res += "\u000d\u000a";
                foreach (DataRow reg in t.Rows)
                {
                    foreach (object dato in reg.ItemArray)
                    {
                        res += dato + "\t";
                    }
                    res = res.Remove(res.Length - 1);
                    res += "\u000d\u000a";
                }
                res = res.Remove(res.Length - 2);
                textBox1.Text = res;
            }
            else
            {
                textBox1.Clear();
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

        private void button7_Click(object sender, EventArgs e)
        {
            imprimir(c.selectC(new DateTime(2022, 2, 26), 1));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            imprimir(t.selectT(489418912));
        }

        private void query(string q)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            if ((q.ToLower().Contains("insert") || q.ToLower().Contains("update") || 
                q.ToLower().Contains("delete")) && !q.ToLower().Contains("exec "))
            {
                SqlCommand cmd = new SqlCommand(q, con);
                textBox1.Text= cmd.ExecuteNonQuery() + " fila(s) afectada(s).";
                con.Close();
            }
            else
            {
                SqlDataAdapter cmd = new SqlDataAdapter(q,con);
                DataTable table = new DataTable();
                cmd.Fill(table);
                imprimir(table);
                con.Close();
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 188 && e.Shift)
            {
                Console.WriteLine(textBox2.Text);
                try
                {
                    query(textBox2.Text);
                }
                catch
                {
                    textBox1.Text = "Wrong Query";
                }
                textBox2.SelectAll();
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Presentacion.Form2 f = new Presentacion.Form2();
            f.Show();
        }
    }
}
