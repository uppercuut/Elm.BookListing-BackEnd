using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elm.BookListing.Application.Dtos
{
    public class BookQueryResult
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? PublishDate { get; set; }
        public string? Cover { get; set; }
        public string? PublishDateOnly
        {
            get => PublishDate?.Split('T')[0];
            set => PublishDate = value;
        }
    }
}
