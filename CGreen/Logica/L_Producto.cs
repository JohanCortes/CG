using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CGreen.Datos;
using System.Data;

namespace CGreen.Logica
{
    class L_Producto
    {
        private int e_id { get; set; }
        private string e_descripcion { get; set; }
        private decimal e_p_interno { get; set; }
        private decimal e_p_venta { get; set; }
        private D_Producto p = new D_Producto();

        public int insertP(string descripcion, decimal p_interno, decimal p_venta)
        {
            e_descripcion = descripcion;
            e_p_interno = p_interno;
            e_p_venta = p_venta;
            return p.insertP(e_descripcion, e_p_interno, e_p_venta);
        }

        public int updateP(int id, string descripcion, decimal p_interno, decimal p_venta)
        {
            e_id = id;
            e_descripcion = descripcion;
            e_p_interno = p_interno;
            e_p_venta = p_venta;
            return p.updateP(e_id, e_descripcion, e_p_interno, e_p_venta);
        }

        public DataTable selectP()
        {
            return p.selectP();
        }
    }
}
