namespace CodeMash.ServiceModel
{
    public class PaginationFromSettingsRequest : IRequestWithPaging
    {
        public PaginationFromSettingsRequest(int pageNumber = 0, int pageSize = 1000)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}