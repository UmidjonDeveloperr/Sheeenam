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
		}
	}
}
