using Elm.BookListing.Application.Dtos;
using Elm.BookListing.Domain.Entites;
using Elm.BookListing.Domain.Repositories;
using MApp.Framework.Application.Queries;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elm.BookListing.Application.Queries
{
    public class BookQueries : IBookQueries
    {
        private readonly IBookReadRepository _bookReadRepository;
        private readonly IDistributedCache _cache;
        public BookQueries(IBookReadRepository bookReadRepository, IDistributedCache cache)
        {
            _bookReadRepository = bookReadRepository;
            _cache = cache;
        }

        public async Task<PaginationQueryResult<BookQueryResult>> GetAllWithoutCover(string term = "", PaginationQueryParameters paginationQueryParameters = default)
        {
            var bookQueryResult = await _bookReadRepository.GetAllWithoutCover(term, paginationQueryParameters);
            return bookQueryResult.Adapt<PaginationQueryResult<BookQueryResult>>();
        }

        public async Task<BookCoverResultDto> GetCoverByBookId(int id)
        {

            ///TODO : Handel Cache deletion on the event of update and delete
            var cacheResult = await _cache.GetStringAsync("bookId#" + id);
            if (cacheResult == null)
            {
                cacheResult = await _bookReadRepository.GetCoverByBookId(id);
                if (cacheResult == "")
                    return null;
                await _cache.SetStringAsync("bookId#" + id, cacheResult);
            }

            return PrepareCover(cacheResult);
        }
        private BookCoverResultDto PrepareCover(string base64)
        {
            var base64Parts = base64.Split(',');
            var imageType = base64Parts[0].Split(':')[1].Split(';')[0];
            var base64Data = base64Parts[1];
            return new BookCoverResultDto() { Content = base64Data, Format = imageType };
        }
    }
}
