﻿using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheeenam.Api.Models.Foundations.Guests;
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
		public async Task ShouldAddGuestAsync()
		{
			//given
			Guest randomGuest = CreateRandomGuest();
			Guest inputGuest = randomGuest;
			Guest storageGuest = inputGuest;
			Guest expectedGuest = storageGuest.DeepClone();

			this.storageBrokerMock.Setup(broker =>
				broker.InsertGuestAsync(inputGuest)).
					ReturnsAsync(storageGuest);

			//formula
			Guest actualGuest = 
				await this.guestService.AddGuestAsync(inputGuest);

			//yechim
			actualGuest.Should().BeEquivalentTo(expectedGuest);

			this.storageBrokerMock.Verify(broker =>
				broker.InsertGuestAsync(inputGuest), Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
