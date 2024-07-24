using System.Collections.Generic;
namespace MApp.Framework.Application.Queries
{
    public class PaginationQueryResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
