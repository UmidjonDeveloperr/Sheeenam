using System;
using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class FailedGuestServiceException : Xeption
	{
        public FailedGuestServiceException(Exception innerException)
            : base(message: "Failed guest service error occured, contact support",
                  innerException)
        { }
    }
}
