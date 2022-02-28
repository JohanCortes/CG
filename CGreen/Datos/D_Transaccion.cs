using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CGreen.Datos
{
    class D_Transaccion
    {
        public int insertT(long cc, decimal valor, byte id_tipo)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("ins_transaccion", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@id_producto", SqlDbType.BigInt).Value = cc;
            ins.Parameters.Add("@id_punto", SqlDbType.Money).Value = valor;
            ins.Parameters.Add("@id_tipo", SqlDbType.TinyInt).Value = id_tipo;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public DataTable selectT(long cc)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlDataAdapter sel = new SqlDataAdapter("sel_transaccion", con);
            sel.SelectCommand.CommandType = CommandType.StoredProcedure;
            sel.SelectCommand.Parameters.Add("@cc", SqlDbType.BigInt).Value = cc;
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
