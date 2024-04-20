using Sheeenam.Api.Models.Foundations.Guests;
using System.Linq;
using System.Threading.Tasks;

namespace Sheeenam.Api.Services.Foundation.Guests
{
    public interface IGuestService
    {
        ValueTask<Guest> AddGuestAsync(Guest guest);
        IQueryable<Guest> RetrieveAllGuests();
    }
}
