namespace OpenEhs.Data.Common
{
    public interface IPagedList
    {
        int RecordCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int PageCount { get; set; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
