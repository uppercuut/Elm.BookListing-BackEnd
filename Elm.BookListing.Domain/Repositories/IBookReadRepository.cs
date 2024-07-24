using Elm.BookListing.Domain.Abstractions;
using Elm.BookListing.Domain.Entites;
using MApp.Framework.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Elm.BookListing.Domain.Repositories
{
    public interface IBookReadRepository : IReadRepository<Book>
    {
        Task<PaginationQueryResult<Book>> GetAllWithoutCover(string term = "", PaginationQueryParameters paginationQueryParameters = default);
        Task<string> GetCoverByBookId(int id);
    }
}