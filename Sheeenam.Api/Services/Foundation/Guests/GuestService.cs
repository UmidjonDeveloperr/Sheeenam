using Sheeenam.Api.Brokers.Logging;
using Sheeenam.Api.Brokers.Storages;
using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
using System;
using System.Threading.Tasks;

namespace Sheeenam.Api.Services.Foundation.Guests
{
	public partial class GuestService : IGuestService
	{
		private readonly IStorageBroker storageBroker;
		private	readonly ILoggingBroker loggingBroker;

		public GuestService(IStorageBroker storageBroker, 
			ILoggingBroker loggingBroker)
		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
		}

		public ValueTask<Guest> AddGuestAsync(Guest guest) =>
			TryCatch(async () =>
			{
				
				ValidateGuestOnAdd(guest);

				return await this.storageBroker.InsertGuestAsync(guest);
			});
	}
	
}
