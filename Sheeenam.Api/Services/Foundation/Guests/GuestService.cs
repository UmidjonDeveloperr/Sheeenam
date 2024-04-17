using Sheeenam.Api.Brokers.Logging;
using Sheeenam.Api.Brokers.Storages;
using Sheeenam.Api.Models.Foundations.Guests;
using System;
using System.Threading.Tasks;

namespace Sheeenam.Api.Services.Foundation.Guests
{
	public class GuestService : IGuestService
	{
		private readonly IStorageBroker storageBroker;
		private	readonly ILoggingBroker loggingBroker;

		public GuestService(IStorageBroker storageBroker, 
			ILoggingBroker loggingBroker)
		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
		}

		public async ValueTask<Guest> AddGuestAsync(Guest guest) =>
			await this.storageBroker.InsertGuestAsync(guest);
	}
	
}
