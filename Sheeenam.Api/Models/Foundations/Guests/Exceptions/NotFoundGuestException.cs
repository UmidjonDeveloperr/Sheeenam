using System;
using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class NotFoundGuestException : Xeption
	{
        public NotFoundGuestException(Guid guestId)
            :base(message: $"Could not find Guest with id:{guestId}")
        { }
    }
}
