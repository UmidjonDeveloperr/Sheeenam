using FluentAssertions;
using Moq;
using Sheeenam.Api.Brokers.Logging;
using Sheeenam.Api.Brokers.Storages;
using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Services.Foundation.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Sheeenam.Api.Tests.xUnit.Services.Foundations.Guests
{
	public partial class GuestServiceTests
	{
		private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
		private readonly IGuestService guestService;

        public GuestServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            
            this.guestService = 
                new GuestService(
                    storageBroker: this.storageBrokerMock.Object,
                    loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Guest CreateRandomGuest() =>
                CreateGuestFiller(date: GetRandomDateTimeOffset()).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static T GetInvalidEnum<T>()
        {
            int randomNumber = GetRandomNumber();

            while(Enum.IsDefined(typeof(T), randomNumber) is true)
            {
                randomNumber = GetRandomNumber();
            }

            return (T)(object)randomNumber;
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedXeption)
        {
            return actualException =>
            actualException.Message == expectedXeption.Message
            && actualException.InnerException.Message == expectedXeption.InnerException.Message
            && (actualException.InnerException as Xeption).DataEquals(expectedXeption.InnerException.Data);
        }

        private static Filler<Guest> CreateGuestFiller(DateTimeOffset date)
        {
            var filler = new Filler<Guest>();

            filler.Setup().
                OnType<DateTimeOffset>().Use(date);

            return filler;
        }


    }
}
