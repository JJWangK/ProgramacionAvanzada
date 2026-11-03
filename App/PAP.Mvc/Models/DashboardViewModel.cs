using System.Collections.Generic;

namespace PAP.Mvc.Models
{
    /// <summary>
    /// Totales por tabla que se muestran en el panel de inicio.
    /// </summary>
    public class DashboardViewModel
    {
        public IDictionary<string, int> Totals { get; set; }
    }
}
