﻿using System;

namespace Sheeenam.Api.Brokers.DateTimes
{
	public class DateTimeBroker : IDateTimeBroker
	{
		public DateTimeOffset GetCurrentDateTime() =>
			DateTimeOffset.UtcNow;
	}
}
