using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class InvalidGuestException : Xeption
	{
        public InvalidGuestException()
            :base(message: "Guest is invalid")
        { }
    }
}
