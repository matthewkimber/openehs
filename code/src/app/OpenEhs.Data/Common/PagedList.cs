using System.Collections.Generic;

namespace OpenEhs.Data.Common
{
    /// <summary>
    /// Generic class that keeps track of paginating a record set
    /// </summary>
    /// <typeparam name="T">The type of data to be paginated</typeparam>
    public class PagedList<T> : List<T>, IPagedList
    {
        public int RecordCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex*PageSize) <= RecordCount; }
        }

        public PagedList(IList<T> source, int pageIndex, int pageSize, int recordCount)
        {
            RecordCount = recordCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = RecordCount / PageSize;

            if (RecordCount % PageSize > 0)
                PageCount++;

            AddRange(source);
        }
    }
}