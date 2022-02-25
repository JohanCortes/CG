using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CGreen.Datos
{
    class D_Vendedor
    {
        public int insertV(Int64 cc, string nombre, string apellido, string email, Int64 telefono)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("ins_vendedor", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@cc", SqlDbType.BigInt).Value = cc;
            ins.Parameters.Add("@nom", SqlDbType.VarChar, 15).Value = nombre;
            ins.Parameters.Add("@ape", SqlDbType.VarChar, 15).Value = apellido;
            ins.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
            ins.Parameters.Add("@tel", SqlDbType.BigInt).Value = telefono;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public int updateV(Int64 cc, string email, Int64 telefono)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("up_vendedor", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@cc", SqlDbType.BigInt).Value = cc;
            ins.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
            ins.Parameters.Add("@tel", SqlDbType.BigInt).Value = telefono;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public DataTable selectV()
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlDataAdapter sel = new SqlDataAdapter("sel_vendedor", con);
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
