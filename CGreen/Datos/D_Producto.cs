using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CGreen.Datos
{
    class D_Producto
    {
        public int insertP(string descripcion, decimal p_interno, decimal p_venta)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("ins_producto", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = descripcion;
            ins.Parameters.Add("@v_in", SqlDbType.Money).Value = p_interno;
            ins.Parameters.Add("@v_ven", SqlDbType.Money).Value = p_venta;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public int updateP(int id, string descripcion, decimal p_interno, decimal p_venta)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("up_producto", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ins.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = descripcion;
            ins.Parameters.Add("@v_in", SqlDbType.Money).Value = p_interno;
            ins.Parameters.Add("@v_ven", SqlDbType.Money).Value = p_venta;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public DataTable selectP()
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlDataAdapter sel = new SqlDataAdapter("sel_producto", con);
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
    }
}
