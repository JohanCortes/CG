using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGreen.Presentacion.Reports
{
    public partial class FormReport2 : Form
    {
        public List<string> ReportNames = new List<string>();
        public DateTime fecha1 = new DateTime();
        public DateTime fecha2 = new DateTime();
        public FormReport2()
        {
            InitializeComponent();
        }


        private void FormReport2_Load(object sender, EventArgs e)
        {
            DataSetUtilidad.utilidad_puntoDataTable table = dataSetUtilidad.utilidad_punto; 
            utilidad_puntoTableAdapter.Fill(table, fecha1, fecha2);
            List<DataRow> rows = (from r in table where !ReportNames.Contains(r.punto) select r).ToList<DataRow>();
            foreach (DataRow row in rows)
            {
                table.Rows.Remove(row);
            }
            reportViewer1.RefreshReport();
        }
    }
}
