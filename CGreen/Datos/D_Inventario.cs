using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CGreen.Datos
{
    class D_Inventario
    {
        public DataTable selectI(Int16 id_punto)
        {
            SqlConnection con = new SqlConnection(D_Conexion.cadcon());
            con.Open();
            SqlDataAdapter sel = new SqlDataAdapter("sel_inventario", con);
            sel.SelectCommand.CommandType = CommandType.StoredProcedure;
            sel.SelectCommand.Parameters.Add("@id_p", SqlDbType.SmallInt).Value = id_punto;
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
