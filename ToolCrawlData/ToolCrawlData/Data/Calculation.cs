using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolCrawlData.Data
{
    public class Calculation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StartDateUtc { get; set; }

        public int? Progress { get; set; }
    }
}
