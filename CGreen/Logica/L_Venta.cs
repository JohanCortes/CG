using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CGreen.Datos;
using System.Data;

namespace CGreen.Logica
{
    class L_Venta
    {
        private Int64 e_cc { get;set; }
        private int e_id { get; set; }
        private Int16 e_id_p { get; set; }
        private Int16 e_uni { get; set; }
        private Int16 e_unixl { get; set; }
        private decimal e_vsu { get; set; }
        private decimal e_vcu { get; set; }

        private int e_id_venta { get;set; }
        private int e_id_producto { get; set; }
        private Int16 e_cantidad { get; set; }
        private D_Venta v = new D_Venta();

        public int insertV(Int64 cc, Int16 id_p, Int16 uni, Int16 unixl, decimal vsu, decimal vcu)
        {
            e_cc = cc;
            e_id_p = id_p;
            e_uni = uni;
            e_unixl = unixl;
            e_vsu = vsu;
            e_vcu = vcu;
            return v.insertV(e_cc, e_id_p, e_uni, e_unixl, e_vsu, e_vcu);
        }

        public int updateV(int id, decimal vcu)
        {
            e_id = id;
            e_vcu = vcu;
            return v.updateV(e_id, e_vcu);
        }

        public DataTable selectV()
        {
            return v.selectV();
        }

        public int insertVP(int id_venta, int id_producto, Int16 cantidad)
        {
            e_id_venta = id_venta;
            e_id_producto = id_producto;
            e_cantidad = cantidad;
            return v.insertVP(e_id_venta, e_id_producto, e_cantidad);
        }

        public DataTable selectVP(int id_venta)
        {
            e_id_venta = id_venta;
            return v.selectVP(e_id_venta);
        }
    }
}
