﻿using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheeenam.Api.Services.Foundation.Guests
{
	public partial class GuestService
	{
		private delegate IQueryable<Guest> ReturningGuestsFunction();
		private delegate ValueTask<Guest> ReturningGuestFunction();

		private async ValueTask<Guest> TryCatch(ReturningGuestFunction returningGuestFunction)
		{
			try
			{
				return await returningGuestFunction();
			}
			catch (NullGuestException nullGuestException)
			{
				throw CreateAndLogValidationException(nullGuestException);
			}
			catch(InvalidGuestException invalidGuestException)
			{
				throw CreateAndLogValidationException(invalidGuestException);
			}
			catch(SqlException sqlException)
			{
				var failedGuestStorageException = 
					new FailedGuestStorageException(sqlException);

				throw CreateAndLogCriticalException(failedGuestStorageException);
			}
			catch(DuplicateKeyException dublicateKeyException)
			{
				var alreadyExistGuestException =
					new AlreadyExistGuestException(dublicateKeyException);

				throw CreateAndLogDependencyValidationException(alreadyExistGuestException);
			}
			catch(Exception exception)
			{
				var failedGuestServiceException =
					new FailedGuestServiceException(exception);

				throw CreateAndLogServiceException(failedGuestServiceException);
			}
		}

		private IQueryable<Guest> TryCatch(ReturningGuestsFunction returningGuestsFunction)
		{
			try
			{
				return returningGuestsFunction();
			}
			catch (SqlException sqlException)
			{
				var failedGuestServiceException = new FailedGuestServiceException(sqlException);

				throw CreateAndLogCriticalException(failedGuestServiceException);
			}
			catch (Exception serviException)
			{
				var failedGuestServiceException = new FailedGuestServiceException(serviException);

				throw CreateAndLogServiceException(failedGuestServiceException);
			}
		}


		private GuestValidationException CreateAndLogValidationException(Xeption exception)
		{
			var guestValidationExceptiion =
					new GuestValidationException(exception);

			this.loggingBroker.LogError(guestValidationExceptiion);

			return guestValidationExceptiion;
		}

		private GuestDependencyException CreateAndLogCriticalException(Xeption exception)
		{
			var guestDependencyException = 
				new GuestDependencyException(exception);

			this.loggingBroker.LogCritical(guestDependencyException);

			return guestDependencyException;
		}

		private GuestDependencyValidationException CreateAndLogDependencyValidationException(
			Xeption exception)
		{
			var guestDependencyValidationException = 
				new GuestDependencyValidationException(exception);

			this.loggingBroker.LogError(guestDependencyValidationException); 
			
			return guestDependencyValidationException;
		}

		private GuestServiceException CreateAndLogServiceException(Xeption exception)
		{
			var guestServiceException = new GuestServiceException(exception);
			this.loggingBroker.LogError(guestServiceException);

			return guestServiceException;
		}
	}
}
