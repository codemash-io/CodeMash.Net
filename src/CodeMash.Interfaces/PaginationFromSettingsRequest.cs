using ServiceStack.Configuration;

namespace CodeMash.Interfaces
{
    public class PaginationFromSettingsRequest : IRequestWithPaging
    {
        public IAppSettings AppSettings { get; set; }

        public PaginationFromSettingsRequest(int pageNumber = 0)
        {
            PageSize = AppSettings.Get<int>("PageSize");
            PageNumber = pageNumber;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}