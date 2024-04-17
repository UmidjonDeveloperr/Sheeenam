using System;
using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class NullGuestException : Xeption
	{
        public NullGuestException()
            :base(message: "Guest is Null!") { }


    }
}
