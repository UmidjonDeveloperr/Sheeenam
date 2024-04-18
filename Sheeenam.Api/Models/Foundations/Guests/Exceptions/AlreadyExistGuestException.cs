using System;
using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class AlreadyExistGuestException : Xeption
	{
        public AlreadyExistGuestException(Exception innerException)
            : base(message: "Guest already exists", innerException)
        { }
    }
}
