using Elm.BookListing.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elm.BookListing.Infrastructure.Repositories.Books
{
    public class BookResultWithCount : Book
    {
        public int TotalCount { get; set; }
    }
}
