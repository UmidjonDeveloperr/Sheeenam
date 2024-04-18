using System;
using Xeptions;

namespace Sheeenam.Api.Models.Foundations.Guests.Exceptions
{
	public class FailedGuestStorageException : Xeption
	{
        public FailedGuestStorageException(Exception innerException)
            :base(message: "Failed guest storage error occured, contact suppurt", 
                 innerException)
        { }
    }
}
