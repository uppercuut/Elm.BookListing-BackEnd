using MApp.Framework.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elm.BookListing.Domain.Abstractions
{
    public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : IdentifiedObject
    {
        Task<PaginationQueryResult<TEntity>> GetAll(string term = "", PaginationQueryParameters paginationQueryParameters = default);
    }
}
