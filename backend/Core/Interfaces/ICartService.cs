using System.Threading.Tasks;
using Models;

namespace Core.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(string id);
        Task<Cart> UpdateOrCreateCartAsync(Cart cart);
        Task<bool> EmptyCartAsync(string id);
    }
}