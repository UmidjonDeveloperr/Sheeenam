using System;

namespace Sheeenam.Api.Brokers.DateTimes
{
	public interface IDateTimeBroker
	{
		DateTimeOffset GetCurrentDateTime();
	}
}
