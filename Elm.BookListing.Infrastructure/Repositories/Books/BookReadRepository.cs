using Elm.BookListing.Domain.Abstractions;
using Elm.BookListing.Domain.Entites;
using Elm.BookListing.Domain.Repositories;
using MApp.Framework.Application.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elm.BookListing.Infrastructure.Repositories.Books
{
    public class BookReadRepository : IBookReadRepository
    {
        private readonly IQueryExecuter _queryExecuter;
        public BookReadRepository(IQueryExecuter queryExecuter)
        {
            _queryExecuter = queryExecuter;
        }

        public async Task<PaginationQueryResult<Book>> GetAllWithoutCover(string term = "", PaginationQueryParameters paginationQueryParameters = null)
        {
            var sqlQuery = "SELECT \r\n    bookId as Id, COUNT(1) OVER () AS TotalCount, \r\n    JSON_VALUE(bookinfo, '$.\"BookTitle\"') AS Title, \r\n    JSON_VALUE(bookinfo, '$.\"BookDescription\"') AS [Description], \r\n    JSON_VALUE(bookinfo, '$.\"Author\"') AS Author, \r\n    JSON_VALUE(bookinfo, '$.\"PublishDate\"') AS PublishDate\r\nFROM \r\n    [Book]\r\nWHERE \r\n(@term IS NULL OR @term ='' )OR \r\n (JSON_VALUE(bookinfo, '$.\"BookTitle\"') like '%'+@term+'%' OR\r\n JSON_VALUE(bookinfo, '$.\"BookDescription\"') like '%'+@term+'%' OR\r\n JSON_VALUE(bookinfo, '$.\"Author\"') like '%'+@term+'%' OR\r\n JSON_VALUE(bookinfo, '$.\"PublishDate\"') like '%'+@term+'%') ORDER BY bookId\r\n OFFSET (@PageNumber - 1) * @PageSize ROWS  \r\n FETCH NEXT @PageSize ROWS ONLY;";

            var dbResult = (await _queryExecuter.QueryAsync<BookResultWithCount>(sqlQuery,
                new { term, paginationQueryParameters.PageNumber, paginationQueryParameters.PageSize })).ToList();

            return new PaginationQueryResult<Book>() { CurrentPage = paginationQueryParameters.PageNumber, PageSize = paginationQueryParameters.PageSize, Total = dbResult.FirstOrDefault()?.TotalCount ?? 0, Data = dbResult };
        }
        public async Task<string> GetCoverByBookId(int id)
        {
            var sqlQuery = " select * from [dbo].[Book] where bookId = @id ";
            var dbResult = (await _queryExecuter.QueryFirstOrDefaultAsync<BookJsonResult>(sqlQuery, new { id }));
            var m = JsonConvert.DeserializeObject<BookJsonResult>(dbResult?.BookInfo ?? "{}");
            return m?.CoverBase64 ?? "";
        }
        public Task<PaginationQueryResult<Book>> GetAll(string term = "", PaginationQueryParameters paginationQueryParameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
