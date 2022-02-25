using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CGreen.Datos;
using System.Data;

namespace CGreen.Logica
{
    class L_Vendedor
    {
        private Int64 e_cc { get; set; }
        private string e_nombre { get; set; }
        private string e_apellido { get; set; }
        private string e_email { get; set; }
        private Int64 e_telefono { get; set; }
        private D_Vendedor v = new D_Vendedor();

        public int insertV(Int64 cc, string nombre, string apellido, string email, Int64 telefono)
        {
            e_cc = cc;
            e_nombre = nombre;
            e_apellido = apellido;
            e_email = email;
            e_telefono = telefono;
            return v.insertV(e_cc, e_nombre, e_apellido, e_email, e_telefono);
        }

        public int updateV(Int64 cc, string email, Int64 telefono)
        {
            e_cc = cc;
            e_email = email;
            e_telefono = telefono;
            return v.updateV(e_cc, e_email, e_telefono);
        }

        public DataTable selectV()
        {
            return v.selectV();
        }
    }
}
