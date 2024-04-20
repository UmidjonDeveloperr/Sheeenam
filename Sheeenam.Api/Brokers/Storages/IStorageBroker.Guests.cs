using Sheeenam.Api.Models.Foundations.Guests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheeenam.Api.Brokers.Storages
{
	public partial interface IStorageBroker
	{
		ValueTask<Guest> InsertGuestAsync(Guest guest);
		IQueryable<Guest> SelectAllGuests();
		public ValueTask<Guest> SelectGuestByIdAsync(Guid guestId);
		public ValueTask<Guest> UpdateGuestAsync(Guest guest);
		public ValueTask<Guest> DeleteGuestAsync(Guest guest);
	}
}
