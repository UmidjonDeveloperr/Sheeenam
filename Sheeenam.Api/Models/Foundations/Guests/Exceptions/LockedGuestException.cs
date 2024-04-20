using System;
using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class LockedGuestException : Xeption
	{
        public LockedGuestException(Exception innerException)
            : base(message: "Guest is locked, please try again", innerException)
        { }
    }
}
