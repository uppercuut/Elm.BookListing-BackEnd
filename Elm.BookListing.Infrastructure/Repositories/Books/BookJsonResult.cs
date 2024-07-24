using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elm.BookListing.Infrastructure.Repositories.Books
{
    public class BookJsonResult
    {
        public int BookId { get; set; }
        public string BookInfo { get; set; }
        public string? CoverBase64 { get; set; }
    }
}
