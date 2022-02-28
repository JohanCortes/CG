using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGreen.Datos;

namespace CGreen.Logica
{
    class L_Transaccion
    {
        private long e_cc { get; set; }
        private decimal e_valor { get; set; }
        private byte e_id_tipo { get; set; }
        private D_Transaccion t = new D_Transaccion();


        public int insertT(long cc, decimal valor, byte id_tipo)
        {
            e_cc = cc;
            e_valor = valor;
            e_id_tipo = id_tipo;
            return t.insertT(e_cc, e_valor, e_id_tipo);
        }

        public DataTable selectT(long cc)
        {
            e_cc = cc;
            return t.selectT(e_cc);
        }
    }
}
