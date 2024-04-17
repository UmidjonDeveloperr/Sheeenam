using Sheeenam.Api.Brokers.Logging;
using Sheeenam.Api.Brokers.Storages;
using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
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

		public async ValueTask<Guest> AddGuestAsync(Guest guest)
		{
			try
			{
				if(guest is null)
				{
					throw new NullGuestException();
				}
				return await this.storageBroker.InsertGuestAsync(guest);
			}
			catch (NullGuestException nullGuestException)
			{
				var guestValidationExceptiion =
					new GuestValidationException(nullGuestException);

				this.loggingBroker.LogError(guestValidationExceptiion);

				throw guestValidationExceptiion;
			}

			
		}
	}
	
}
