namespace Elm.BookListing.Application.Queries
{
    public record PaginationQueryParameters(int PageSize = 10 , int PageNumber=1, string OrderBy="id", bool IsDescending=true)
    {
    }
}
