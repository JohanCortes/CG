using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CGreen.Datos
{
    class D_Control
    {
        public int insertC(int id_producto, short id_punto, byte id_tipo, short unidades)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlCommand ins = new SqlCommand("ins_control", con);
            ins.CommandType = CommandType.StoredProcedure;
            ins.Parameters.Add("@id_producto", SqlDbType.Int).Value = id_producto;
            ins.Parameters.Add("@id_punto", SqlDbType.SmallInt).Value = id_punto;
            ins.Parameters.Add("@id_tipo", SqlDbType.TinyInt).Value = id_tipo;
            ins.Parameters.Add("@unidades", SqlDbType.SmallInt).Value = unidades;
            int a = ins.ExecuteNonQuery();
            con.Close();
            return a;
        }

        public DataTable selectC(DateTime fecha, short id_punto)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlDataAdapter sel = new SqlDataAdapter("sel_control_inv", con);
            sel.SelectCommand.CommandType = CommandType.StoredProcedure;
            sel.SelectCommand.Parameters.Add("@fecha", SqlDbType.SmallDateTime).Value = fecha;
            sel.SelectCommand.Parameters.Add("@id_punto", SqlDbType.SmallInt).Value = id_punto;
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
