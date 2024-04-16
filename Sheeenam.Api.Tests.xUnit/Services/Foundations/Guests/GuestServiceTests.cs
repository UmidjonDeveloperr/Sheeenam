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
using Tynamix.ObjectFiller;
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

        private static Guest CreateRandomGuest() =>
                CreateGuestFiller(date: GetRandomDateTimeOffset()).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Guest> CreateGuestFiller(DateTimeOffset date)
        {
            var filler = new Filler<Guest>();

            filler.Setup().
                OnType<DateTimeOffset>().Use(date);

            return filler;
        }


    }
}
