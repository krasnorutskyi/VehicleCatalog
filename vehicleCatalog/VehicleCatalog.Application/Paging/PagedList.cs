using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleCatalog.Application.Paging
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalElements { get; set; }
        public bool HasPreviousPage => TotalCount > 1;
        public bool HasNextPage => PageIndex < TotalCount;

        public PagedList() { }

        public PagedList(IEnumerable<T> elements, PageParameters pageParameters, int totalElements)
        {
            this.PageIndex = pageParameters.PageIndex;
            this.PageSize = pageParameters.PageSize;
            this.TotalElements = totalElements;
            this.TotalCount = (int)Math.Ceiling((double)totalElements/pageParameters.PageSize);

            this.AddRange(elements);
        }

    }
}
