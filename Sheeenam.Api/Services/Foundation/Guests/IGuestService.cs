using Sheeenam.Api.Models.Foundations.Guests;
using System.Threading.Tasks;

namespace Sheeenam.Api.Services.Foundation.Guests
{
    public interface IGuestService
    {
        ValueTask<Guest> AddGuestAsync(Guest guest);
    }
}
