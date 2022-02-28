using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGreen.Datos;

namespace CGreen.Logica
{
    class L_Control
    {
        private int e_id_producto { get; set; }
        private short e_id_punto { get; set; }
        private byte e_id_tipo { get; set; }
        private DateTime e_fecha { get; set; }
        private short e_unidades { get; set; }
        private D_Control c = new D_Control();


        public int insertP(int id_producto, short id_punto, byte id_tipo, short unidades)
        {
            e_id_producto = id_producto;
            e_id_punto = id_punto;
            e_id_tipo = id_tipo;
            e_unidades = unidades;
            return c.insertC(e_id_producto, e_id_punto, e_id_tipo, e_unidades);
        }

        public DataTable selectC(DateTime fecha, short id_punto)
        {
            e_fecha = fecha;
            e_id_punto = id_punto;
            return c.selectC(e_fecha, e_id_punto);
        }
    }
}
