using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleCatalog.Application.Paging
{
    public class PageParameters
    {
        public int PageSize { get; set; } = 12;
        public int PageIndex { get; set; } = 1;
    }
}