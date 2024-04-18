using System;
using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class GuestDependencyValidationException : Xeption
	{
        public GuestDependencyValidationException(Exception innerException)
            :base(message: "Guest dependency validation error occured, " +
                 "fix the errors and try again",
                    innerException)
        { }
    }
}
