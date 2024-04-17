using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;

namespace Sheeenam.Api.Services.Foundation.Guests
{
	public partial class GuestService
	{
		private void ValidateGuestNotNull(Guest guest)
		{
			if (guest is null)
			{
				throw new NullGuestException();
			}
		}
	}
}
