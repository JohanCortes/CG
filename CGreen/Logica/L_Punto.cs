using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CGreen.Datos;
using System.Data;

namespace CGreen.Logica
{
    class L_Punto
    {
        private Int16 e_id { get; set; }
        private string e_nombre { get; set; }
        private string e_direccion { get; set; }
        private D_Punto p = new D_Punto();
        

        public int insertP(string nombre, string direccion)
        {
            e_nombre = nombre;
            e_direccion = direccion;
            return p.insertP(e_nombre, e_direccion);
        }

        public int updateP(Int16 id, string nombre, string direccion)
        {
            e_id = id;
            e_nombre = nombre;
            e_direccion = direccion;
            return p.updateP(e_id, e_nombre, e_direccion);
        }

        public DataTable selectP()
        {
            return p.selectP();
        }
    }
}
