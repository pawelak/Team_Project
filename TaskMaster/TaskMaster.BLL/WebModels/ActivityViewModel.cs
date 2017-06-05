using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.BLL.WebModels
{
    public class ActivityViewModel
    {
        public string Name { get; set; }
        public double Second { get; set; }
        public double Minute { get; set; }
        public double Hour { get; set; }
        public TimeSpan Time { get; set; }
        public string Percent { get; set; }
    }
}
