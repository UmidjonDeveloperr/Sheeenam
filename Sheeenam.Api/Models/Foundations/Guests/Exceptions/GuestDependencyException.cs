using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class GuestDependencyException : Xeption
	{
        public GuestDependencyException(Xeption innerException)
            : base(message: "Guest dependency error occured, contact support",
                  innerException)
        { }
    }
}
