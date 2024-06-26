﻿using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
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
		public async Task ShouldThrowCriticalDependencyExceptionOnAddSqlErrorOccursAndLogItAsync()
		{
			//given
			Guest someGuest = CreateRandomGuest();
			SqlException sqlException = GetSqlError();

			var failedGuestStorageException = 
				new FailedGuestStorageException(sqlException);

			var expectedGuestDependencyException =
				new GuestDependencyException(failedGuestStorageException);

			this.storageBrokerMock.Setup(broker =>
				broker.InsertGuestAsync(someGuest))
					.ThrowsAsync(sqlException);
			//when
			ValueTask<Guest> addGuestTask = 
				this.guestService.AddGuestAsync(someGuest);


			//then
			await Assert.ThrowsAsync<GuestDependencyException>(()=>
				addGuestTask.AsTask());

			this.storageBrokerMock.Verify(broker=>
				broker.InsertGuestAsync(someGuest),
					Times.Once());

			this.loggingBrokerMock.Verify(broker =>
				broker.LogCritical(It.Is(SameExceptionAs(
					expectedGuestDependencyException))),
						Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndLogItAsync()
		{
			//given
			Guest someGuest = CreateRandomGuest();
			string someMessage = GetRandomString();

			var dublicateKeyException =
				new DuplicateKeyException(someMessage);

			var alreadyExistGuestException =
				new AlreadyExistGuestException(dublicateKeyException);

			var expectedGuestDependencyValidationException =
				new GuestDependencyValidationException(alreadyExistGuestException);

			this.storageBrokerMock.Setup(broker =>
				broker.InsertGuestAsync(someGuest))
				.ThrowsAsync(dublicateKeyException);
			//when
			ValueTask<Guest> addGuestTask =
				this.guestService.AddGuestAsync(someGuest);

			//then
			await Assert.ThrowsAsync<GuestDependencyValidationException>(() =>
				addGuestTask.AsTask());

			this.storageBrokerMock.Verify(broker=>
				broker.InsertGuestAsync(someGuest), 
					Times.Once());

			this.loggingBrokerMock.Verify(broker=>
				broker.LogError(It.Is(SameExceptionAs(
					expectedGuestDependencyValidationException))),
						Times.Once());

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}

		[Fact]
		public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
		{
			//given
			Guest someGuest = CreateRandomGuest();
			var serviceException = new Exception();

			var failedGuestServiceException = 
				new FailedGuestServiceException(serviceException);

			var expectedGuestServiceexception = 
				new GuestServiceException(failedGuestServiceException);

			this.storageBrokerMock.Setup(broker=>
				broker.InsertGuestAsync(someGuest))
					.ThrowsAsync(serviceException);

			//when
			ValueTask<Guest> addGuestTask =
				this.guestService.AddGuestAsync(someGuest);

			//then
			await Assert.ThrowsAsync<GuestServiceException>(() =>
				addGuestTask.AsTask());

			this.storageBrokerMock.Verify(broker=>
				broker.InsertGuestAsync(someGuest),
					Times.Once());

			this.loggingBrokerMock.Verify(broker=>
				broker.LogError(It.Is(SameExceptionAs(
					expectedGuestServiceexception))),
						Times.Once);

			this.storageBrokerMock.VerifyNoOtherCalls();
			this.loggingBrokerMock.VerifyNoOtherCalls();
		}
	}
}
