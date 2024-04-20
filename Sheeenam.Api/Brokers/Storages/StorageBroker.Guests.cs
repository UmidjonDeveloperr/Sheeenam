using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheeenam.Api.Models.Foundations.Guests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheeenam.Api.Brokers.Storages
{
	public partial class StorageBroker
	{
		public DbSet<Guest> Guests { get; set; }

		public async ValueTask<Guest> InsertGuestAsync(Guest guest)
		{
			using var broker = new StorageBroker(this.configuration);

			EntityEntry<Guest> guestEntityEntry = 
				await broker.Guests.AddAsync(guest);

			await broker.SaveChangesAsync();

			return guestEntityEntry.Entity;
		}

		public IQueryable<Guest> SelectAllGuests() =>
			SelectAll<Guest>();

		public async ValueTask<Guest> SelectGuestByIdAsync(Guid guestId) =>
			await SelectAsync<Guest>(guestId);

		public async ValueTask<Guest> UpdateGuestAsync(Guest guest) =>
			await UpdateAsync(guest);

		public async ValueTask<Guest> DeleteGuestAsync(Guest guest) =>
			await DeleteAsync(guest);

	}
}
