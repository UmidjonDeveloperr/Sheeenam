using Moq;
using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sheeenam.Api.Tests.xUnit.Services.Foundations.Guests
{
	public partial class GuestServiceTests
	{
		[Fact]
		public async Task ShouldThrowValidationExeptionOnAddIfGuestIsNulAdnLogItAsync()
		{
			//Given
			Guest NullGuest = null;
			var nullGuestException = new NullGuestException();

			var expectedGuestValidationException =
				new GuestValidationException(nullGuestException);

			//When
			ValueTask<Guest> addGuestTask =
				this.guestService.AddGuestAsync(NullGuest);

			//then
			await Assert.ThrowsAsync<GuestValidationException>(() =>
				addGuestTask.AsTask());

			this.loggingBrokerMock.Verify(broker=>
				broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationException))),
				Times.Once);

			this.storageBrokerMock.Verify(broker=>
				broker.InsertGuestAsync(It.IsAny<Guest>()),
				Times.Never);

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task ShouldThrowValidationExceptionOnAddIfGuestIsInvalidAndLogItAsync(
			string invalidText)
		{
			//given
			var invalidGuest = new Guest
			{
				FirstName = invalidText
			};

			var invalidGuestException = new InvalidGuestException();

			invalidGuestException.AddData(
				key: nameof(Guest.Id),
				values: "Id is required");

			invalidGuestException.AddData(
				key: nameof(Guest.FirstName),
				values: "Text is required");

			invalidGuestException.AddData(
				key: nameof(Guest.LastName),
				values: "Text is required");

			invalidGuestException.AddData(
				key: nameof(Guest.DateOfBirthday),
				values: "DateOfBirthday is required");

			invalidGuestException.AddData(
				key: nameof(Guest.Email),
				values: "Text is required");

			invalidGuestException.AddData(
				key: nameof(Guest.Address),
				values: "Text is required");

			var expectedGuestValidationException = 
				new GuestValidationException(invalidGuestException);

			//when
			ValueTask<Guest> addGuestTask =
				this.guestService.AddGuestAsync(invalidGuest);

			//then
			await Assert.ThrowsAsync<GuestValidationException>(()=>
				addGuestTask.AsTask());

			this.loggingBrokerMock.Verify(broker=>
				broker.LogError(It.Is(SameExceptionAs(
					expectedGuestValidationException))),
					Times.Once);

			this.storageBrokerMock.Verify(broker =>
				broker.InsertGuestAsync(It.IsAny<Guest>()),
				Times.Never);

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowValidationExceptionOnAddIfGenderIsInvalidAndLogItAsync()
		{
			//given
			Guest randomGuest = CreateRandomGuest();
			Guest invalidGuest = randomGuest;
			invalidGuest.Gender = GetInvalidEnum<GenderType>();
			var invalidGuestException = new InvalidGuestException();

			invalidGuestException.AddData(
				key: nameof(Guest.Gender),
				values: "Value is invalid");

			var expectedGuestException = 
				new GuestValidationException(invalidGuestException);

			//when
			ValueTask<Guest> addGuestTask =
				this.guestService.AddGuestAsync(invalidGuest);

			//then
			await Assert.ThrowsAsync<GuestValidationException>(()=>
				addGuestTask.AsTask());

			this.loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(
					expectedGuestException))),
						Times.Once);

			this.storageBrokerMock.Verify(broker=>
				broker.InsertGuestAsync(It.IsAny<Guest>()),
				Times.Never);

			this.loggingBrokerMock.VerifyNoOtherCalls();
			this.storageBrokerMock.VerifyNoOtherCalls();
		}

	}
}
