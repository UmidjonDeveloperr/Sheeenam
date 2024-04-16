using System;

namespace Sheeenam.Api.Brokers.Logging
{
	public interface ILoggingBroker
	{
		void LogError(Exception exception);
		void LogCritical(Exception exception);
	}
}
