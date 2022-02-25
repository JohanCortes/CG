using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CGreen.Datos
{
    class D_Punto
    {
        public int insertP(string nombre, string direccion)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("ins_punto", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@nom", SqlDbType.VarChar, 20).Value = nombre;
            ins.Parameters.Add("@dir", SqlDbType.VarChar, 30).Value = direccion;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public int updateP(Int16 id, string nombre, string direccion)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("up_punto", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@id", SqlDbType.SmallInt).Value = id;
            ins.Parameters.Add("@nom", SqlDbType.VarChar, 20).Value = nombre;
            ins.Parameters.Add("@dir", SqlDbType.VarChar, 30).Value = direccion;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public DataTable selectP()
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlDataAdapter sel = new SqlDataAdapter("sel_punto", con);
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
