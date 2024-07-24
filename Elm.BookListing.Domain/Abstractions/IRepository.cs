using System.Threading.Tasks;

namespace Elm.BookListing.Domain.Abstractions
{
    public interface IRepository<T> where T : IdentifiedObject
    {
        Task<T> GetById(int id);
    }
}
