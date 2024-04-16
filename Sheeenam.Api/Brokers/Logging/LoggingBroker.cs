using Microsoft.Extensions.Logging;
using System;

namespace Sheeenam.Api.Brokers.Logging
{
	public class LoggingBroker : ILoggingBroker
	{
		private readonly ILogger<ILoggingBroker> logger;

		public LoggingBroker(ILogger<ILoggingBroker> logger) =>
			this.logger = logger;

		public void LogError(Exception exception) =>
			this.logger.LogError(exception, exception.Message);

		public void LogCritical(Exception exception) =>
			this.logger.LogCritical(exception, exception.Message);
	}
}
