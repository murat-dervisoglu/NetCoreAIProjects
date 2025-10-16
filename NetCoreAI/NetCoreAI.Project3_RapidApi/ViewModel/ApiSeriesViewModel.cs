using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAI.Project3_RapidApi.ViewModel
{
    public class ApiSeriesViewModel
    {
        public int rank { get; set; }
        public string title { get; set; } = string.Empty;
        public float rating { get; set; }
        public string year { get; set; } = string.Empty;
    }
}
