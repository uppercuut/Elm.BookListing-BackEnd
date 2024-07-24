using Elm.BookListing.Application.Dtos;
using MApp.Framework.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elm.BookListing.Application.Queries
{
    public interface IBookQueries
    {
        Task<PaginationQueryResult<BookQueryResult>> GetAllWithoutCover(string term = "", PaginationQueryParameters paginationQueryParameters = default);

        Task<BookCoverResultDto> GetCoverByBookId(int id);
    }
}
