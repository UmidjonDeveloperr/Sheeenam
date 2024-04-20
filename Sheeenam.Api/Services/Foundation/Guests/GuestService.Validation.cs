using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
using System;
using System.Data;
using System.Diagnostics;
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
				(Rule: IsInValid(guest.Address), Parametr: nameof(Guest.Address)),
				(Rule: IsInValid(guest.Gender), Parametr: nameof(Guest.Gender))
				);
		}

		private void ValidateGuestOnModify(Guest guest)
		{
			ValidateGuestNotNull(guest);

			Validate(
				(Rule: IsInValid(guest.Id), Parameter: nameof(guest.Id)),
				(Rule: IsInValid(guest.FirstName), Parameter: nameof(guest.FirstName)),
				(Rule: IsInValid(guest.LastName), Parameter: nameof(guest.LastName)),
				(Rule: IsInValid(guest.DateOfBirthday), Parameter: nameof(guest.DateOfBirthday)),
				(Rule: IsInValid(guest.Email), Parameter: nameof(guest.Email)),
				(Rule: IsInValid(guest.Address), Parameter: nameof(guest.Address)),
				(Rule: IsInValid(guest.Gender), Parameter: nameof(guest.Gender)),
				(Rule: IsInValid(guest.CreatedDate), Parameter: nameof(guest.CreatedDate)),
				(Rule: IsInValid(guest.UpdatedDate), Parameter: nameof(guest.UpdatedDate)),
				(Rule: IsNotRecent(guest.UpdatedDate), Parameter: nameof(guest.UpdatedDate)),

				(Rule: IsSame(
					firstDate: guest.UpdatedDate,
					secondDate: guest.CreatedDate,
					secondDateName: nameof(Guest.CreatedDate)),
				Parameter: nameof(Guest.UpdatedDate)));
		}

		private void ValidateGuestNotNull(Guest guest)
		{
			if (guest is null)
			{
				throw new NullGuestException();
			}
		}

		private static void ValidateStorageGuest(Guest maybeGuest, Guid guestId)
		{
			if(maybeGuest is null)
			{
				throw new NotFoundGuestException(guestId);
			}
		}

		private static void ValidateAgainstStorageGuestOnModify(Guest inputGuest, Guest storageGuest)
		{
			ValidateStorageGuest(storageGuest, inputGuest.Id);

			Validate(
				(Rule: IsNotSame(
					firstDate: inputGuest.CreatedDate,
					secondDate: storageGuest.CreatedDate,
					secondDateName: nameof(Guest.CreatedDate)),
				Parameter: nameof(Guest.CreatedDate)),

				(Rule: IsSame(
					firstDate: inputGuest.UpdatedDate,
					secondDate: storageGuest.UpdatedDate,
					secondDateName: nameof(Guest.UpdatedDate)),
				Parameter: nameof(Guest.UpdatedDate)));
		}

		private static void ValidateGuestId(Guid guestId) =>
			Validate((Rule: IsInValid(guestId), Parameter: nameof(guestId)));

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

		private static dynamic IsInValid(GenderType gender) => new
		{
			Condition = Enum.IsDefined(gender) is false,
			Message = "Value is invalid"
		};

		private static dynamic IsSame(
			DateTimeOffset firstDate,
			DateTimeOffset secondDate,
			string secondDateName) => new
			{
				Condition = firstDate == secondDate,
				Message = $"Date is same as {secondDateName}"
			};

		private static dynamic IsNotSame(
			DateTimeOffset firstDate,
			DateTimeOffset secondDate,
			string secondDateName) => new
			{
				Condition = firstDate != secondDate,
				Message = $"Date is not same as {secondDateName}"
			};

		private dynamic IsNotRecent(DateTimeOffset date) => new
		{
			Condition = isDateNotRecent(date),
			Message = "Date is not recent"
		};
		private bool isDateNotRecent(DateTimeOffset date)
		{
			DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTime();
			TimeSpan timeDifference = currentDateTime.Subtract(date);

			return timeDifference.TotalSeconds is > 60 or < 0;
		}

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
