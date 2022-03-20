using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGreen.Presentacion.Reports
{
    public partial class FormReport1 : Form
    {
        public List<string> ReportNames = new List<string>();
        public DateTime fecha1 = new DateTime();
        public DateTime fecha2 = new DateTime();
        CultureInfo ci = new CultureInfo("Es-Co");
        public string Dia(DateTime date)
        {
            return ci.DateTimeFormat.GetDayName(date.DayOfWeek);
        }
        public string Mes(DateTime date)
        {
            return ci.DateTimeFormat.GetMonthName(date.Month);
        }
        public FormReport1()
        {
            InitializeComponent();
        }

        private void FormReport1_Load(object sender, EventArgs e)
        {
            DataSetUtilidad.utilidad_puntoDataTable table = dataSetUtilidad.utilidad_punto;
            utilidad_puntoTableAdapter.Fill(table, fecha1, fecha2);

            List<DataRow> rows = (from r in table where !ReportNames.Contains(r.punto) select r).ToList<DataRow>();
            foreach (DataRow row in rows)
            {
                table.Rows.Remove(row);
            }

            DataRow min = table.Where(b => b.utilidad == table.Min(u => u.utilidad)).First(),
                max = table.Where(b => b.utilidad == table.Max(u => u.utilidad)).First();
            ReportParameter p = new ReportParameter("Parameter1",
                $"Utilidad minima ${Convert.ToInt64(min[1])} en {min[0]} el dia " +
                $"{Dia((DateTime)min[2])} {((DateTime)min[2]).Day} " +
                $"de {Mes((DateTime)min[2])} del {((DateTime)min[2]).Year} \n" +
                $"Utilidad maxima ${Convert.ToInt64(max[1])} en {max[0]} el dia " +
                $"{Dia((DateTime)max[2])} {((DateTime)max[2]).Day} " +
                $"de {Mes((DateTime)max[2])} del {((DateTime)max[2]).Year} \n");
            reportViewer1.LocalReport.SetParameters(p);
            reportViewer1.RefreshReport();
        }
    }
}
