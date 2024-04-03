using FluentAssertions;
using Moq;
using Sheeenam.Api.Brokers.Storages;
using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Services.Foundation.Guests;
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
		private readonly Mock<IStorageBroker> storageBrokerMock;
		private readonly IGuestService guestService;

        public GuestServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            
            this.guestService = 
                new GuestService(storageBroker: this.storageBrokerMock.Object);
        }

        [Fact]
        public async Task ShouldAddGuestAsync()
        {
            Guest randomGuest = new Guest()
            {
                Id = Guid.NewGuid(),
                FirstName = "Alex",
                LastName = "Doe",
                Address = "Brooks Str. #12",
                DateOfBirthday = new DateTimeOffset(),
                Email = "random@gmail.com",
                Gender = GenderType.Male,
                PhoneNumber = "1234567890"
            };

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(randomGuest)).ReturnsAsync(randomGuest);

            Guest actual = await this.guestService.AddGuestAsync(randomGuest);

            actual.Should().BeEquivalentTo(randomGuest);
        }


    }
}
