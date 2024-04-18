using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
using System;
using System.Data;
using System.Net.Http;

namespace Sheeenam.Api.Services.Foundation.Guests
{
	public partial class GuestService
	{
		private	void ValidateGuestOnAdd(Guest guest) 
		{
			ValidateGuestNotNull(guest);

			Validate(
				(Rule: IsInValid(guest.Id), Parametr: nameof(Guest.Id)),
				(Rule: IsInValid(guest.FirstName), Parametr: nameof(Guest.FirstName)),
				(Rule: IsInValid(guest.LastName), Parametr: nameof(Guest.LastName)),
				(Rule: IsInValid(guest.DateOfBirthday), Parametr: nameof(Guest.DateOfBirthday)),
				(Rule: IsInValid(guest.Email), Parametr: nameof(Guest.Email)),
				(Rule: IsInValid(guest.Address), Parametr: nameof(Guest.Address))
				);
		}
		private void ValidateGuestNotNull(Guest guest)
		{
			if (guest is null)
			{
				throw new NullGuestException();
			}
		}

		private static dynamic IsInValid(Guid id) => new
		{
			Condition = id == Guid.Empty,
			Message = "Id is required"
		};

		private static dynamic IsInValid(string text) => new
		{
			Condition = string.IsNullOrWhiteSpace(text),
			Message = "Text is required"
		};

		private static dynamic IsInValid(DateTimeOffset date) => new
		{
			Condition = date == default,
			Message = "DateOfBirthday is required"
		};

		private	static void Validate(params (dynamic Rule, string Parametr)[] validations)
		{
			var invalidGuestException = new InvalidGuestException();

			foreach((dynamic rule, string parametr) in validations)
			{
				if (rule.Condition)
				{
					invalidGuestException.UpsertDataList(
						key: parametr,
						value: rule.Message);
				}
			}

			invalidGuestException.ThrowIfContainsErrors();
		}

	}
}
