
using System;

namespace Sheeenam.Api.Models.Foundations.Guests
{
	public class Guest
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirthday { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public GenderType Gender { get; set; }
    }
}
