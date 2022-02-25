using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CGreen.Datos;

namespace CGreen.Logica
{
    class L_Inventario
    {
        private Int16 e_id_punto { get; set; }
        private D_Inventario i = new D_Inventario();

        public DataTable selectI(Int16 id_punto)
        {
            e_id_punto = id_punto;
            return i.selectI(e_id_punto);
        }
    }
}
