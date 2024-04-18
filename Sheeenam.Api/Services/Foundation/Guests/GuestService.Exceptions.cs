using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
using System.Threading.Tasks;
using Xeptions;

namespace Sheeenam.Api.Services.Foundation.Guests
{
	public partial class GuestService
	{
		private delegate ValueTask<Guest> ReturningGuestFunction();

		private async ValueTask<Guest> TryCatch(ReturningGuestFunction returningGuestFunction)
		{
			try
			{
				return await returningGuestFunction();
			}
			catch (NullGuestException nullGuestException)
			{
				throw CreateAndLogValidationException(nullGuestException);
			}
			catch(InvalidGuestException invalidGuestException)
			{
				throw CreateAndLogValidationException(invalidGuestException);
			}
		}

		private GuestValidationException CreateAndLogValidationException(Xeption exception)
		{
			var guestValidationExceptiion =
					new GuestValidationException(exception);

			this.loggingBroker.LogError(guestValidationExceptiion);

			return guestValidationExceptiion;
		}
	}
}
