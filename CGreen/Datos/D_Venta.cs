using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CGreen.Datos
{
    class D_Venta
    {
        public int insertV(Int64 cc, Int16 id_p, Int16 uni, Int16 unixl, decimal vsu, decimal vcu)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("ins_venta", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@cc_v", SqlDbType.BigInt).Value = cc;
            ins.Parameters.Add("@id_p", SqlDbType.SmallInt).Value = id_p;
            ins.Parameters.Add("@uni", SqlDbType.SmallInt).Value = uni;
            ins.Parameters.Add("@uni_xl", SqlDbType.SmallInt).Value = unixl;
            ins.Parameters.Add("@v_sin_uti", SqlDbType.Money).Value = vsu;
            ins.Parameters.Add("@v_con_uti", SqlDbType.Money).Value = vcu;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public int updateV(int id, decimal vcu)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("up_venta", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ins.Parameters.Add("@v_con_uti", SqlDbType.Money).Value = vcu;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public DataTable selectV()
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlDataAdapter sel = new SqlDataAdapter("sel_venta", con);
            sel.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable tabla = new DataTable();
            sel.Fill(tabla);
            foreach (DataRow reg in tabla.Rows)
            {
                foreach (object dato in reg.ItemArray)
                {
                    Console.Write(dato + "\t");
                }
                Console.WriteLine();
            }
            con.Close();
            return tabla;
        }

        public int insertVP(int id_venta, int id_producto, Int16 cantidad)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("ins_ven_pro", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@id_v", SqlDbType.Int).Value = id_venta;
            ins.Parameters.Add("@id_p", SqlDbType.Int).Value = id_producto;
            ins.Parameters.Add("@can", SqlDbType.SmallInt).Value = cantidad;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public DataTable selectVP(int id_venta)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlDataAdapter sel = new SqlDataAdapter("sel_ven_pro", con);
            sel.SelectCommand.CommandType = CommandType.StoredProcedure;
            sel.SelectCommand.Parameters.Add("@id_v", SqlDbType.Int).Value = id_venta;
            DataTable tabla = new DataTable();
            sel.Fill(tabla);
            foreach (DataRow reg in tabla.Rows)
            {
                foreach (object dato in reg.ItemArray)
                {
                    Console.Write(dato + "\t");
                }
                Console.WriteLine();
            }
            con.Close();
            return tabla;
        }
    }
}
