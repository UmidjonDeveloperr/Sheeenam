using Sheeenam.Api.Brokers.DateTimes;
using Sheeenam.Api.Brokers.Logging;
using Sheeenam.Api.Brokers.Storages;
using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheeenam.Api.Services.Foundation.Guests
{
	public partial class GuestService : IGuestService
	{
		private readonly IStorageBroker storageBroker;
		private	readonly ILoggingBroker loggingBroker;
		private readonly IDateTimeBroker dateTimeBroker;

		public GuestService(IStorageBroker storageBroker,
			ILoggingBroker loggingBroker,
			IDateTimeBroker dateTimeBroker)
		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
			this.dateTimeBroker = dateTimeBroker;
		}

		public ValueTask<Guest> AddGuestAsync(Guest guest) =>
			TryCatch(async () =>
			{
				
				ValidateGuestOnAdd(guest);

				return await this.storageBroker.InsertGuestAsync(guest);
			});

		public IQueryable<Guest> RetrieveAllGuests() =>
			TryCatch(() => this.storageBroker.SelectAllGuests());

		public ValueTask<Guest> RetrieveGuestByIdAsync(Guid guestId) =>
			TryCatch(async () =>
			{
				ValidateGuestId(guestId);

				Guest maybeGuest = await this.storageBroker.SelectGuestByIdAsync(guestId);

				ValidateStorageGuest(maybeGuest, guestId);

				return maybeGuest;
			});


		public ValueTask<Guest> ModifyGuestAsync(Guest guest) =>
			TryCatch(async () =>
			{
				ValidateGuestOnModify(guest);

				var maybeGuest = await this.storageBroker.SelectGuestByIdAsync(guest.Id);

				ValidateAgainstStorageGuestOnModify(inputGuest: guest, storageGuest: maybeGuest);

				return await this.storageBroker.UpdateGuestAsync(guest);
			});

		public ValueTask<Guest> RemoveGuestByIdAsync(Guid guestId) =>
			TryCatch(async () =>
			{
				ValidateGuestId(guestId);

				Guest maybeGuest =
					await this.storageBroker.SelectGuestByIdAsync(guestId);

				ValidateStorageGuest(maybeGuest, guestId);

				return await this.storageBroker.DeleteGuestAsync(maybeGuest);
			});

		
	}
	
}
