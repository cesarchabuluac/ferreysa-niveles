using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vales.Helpers
{
    public static class Global
    {
        public static string Perc(int num, int den) { return (num * 100.0 / den).ToString("F0") + "%"; }

        public static double ToDouble(object o)
        {
            if (o == null || o == DBNull.Value) return 0d;
            double v;
            return double.TryParse(o.ToString(), out v) ? v : 0d;
        }
    }
}
