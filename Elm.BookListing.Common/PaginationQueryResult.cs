using System.Collections.Generic;
namespace Elm.BookListing.Application.Queries
{
    public class PaginationQueryResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
