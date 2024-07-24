using Elm.BookListing.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elm.BookListing.Domain.Entites
{
    public class Book : Entity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? PublishDate { get; set; }
        public string? Cover { get; set; }
    }
}
