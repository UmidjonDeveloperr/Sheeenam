using Sheeenam.Api.Brokers.Storages;
using Sheeenam.Api.Models.Foundations.Guests;
using System.Threading.Tasks;

namespace Sheeenam.Api.Services.Foundation.Guests
{
	public class GuestService : IGuestService
	{
		private readonly IStorageBroker storageBroker;

        public GuestService(IStorageBroker storageBroker) =>
			this.storageBroker = storageBroker;

        public ValueTask<Guest> AddGuestAsync(Guest guest) =>
			this.storageBroker.InsertGuestAsync(guest);
	}
}
