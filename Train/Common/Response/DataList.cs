namespace Common.Response
{
    public class DataList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get;private set; }

        public DataList(IEnumerable<T> items, int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
            Items = items;
        }

        public void SetTotalCount(int total)
        {
            TotalCount = total;
        }
    }
}
