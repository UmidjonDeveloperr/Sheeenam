using System;
using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class GuestServiceException : Xeption
	{
        public GuestServiceException(Exception innerException)
            :base(message: "Guest service error occured, contact support",
                 innerException)
        { }
    }
}
