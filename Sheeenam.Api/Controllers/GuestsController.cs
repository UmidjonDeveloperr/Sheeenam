using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheeenam.Api.Models.Foundations.Guests;
using Sheeenam.Api.Models.Foundations.Guests.Exceptions;
using Sheeenam.Api.Services.Foundation.Guests;
using System.Threading.Tasks;

namespace Sheeenam.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GuestsController : RESTFulController
	{
		private readonly IGuestService guestService;

		public GuestsController(IGuestService guestService)
		{
			this.guestService = guestService;
		}

		[HttpPost]
		public async ValueTask<ActionResult<Guest>> PostGuestAsync(Guest guest)
		{
			try
			{
				Guest postedGuest = await this.guestService.AddGuestAsync(guest);

				return Created(postedGuest);
			}
			catch (GuestValidationException guestValidationException)
			{

				return BadRequest(guestValidationException);
			}
			catch(GuestDependencyValidationException guestDependencyValidationException)
				when(guestDependencyValidationException.InnerException is AlreadyExistGuestException)
			{
				return Conflict(guestDependencyValidationException);
			}
			catch(GuestDependencyValidationException guestDependencyValidationException)
			{
				return BadRequest(guestDependencyValidationException.InnerException);
			}
			catch(GuestDependencyException guestDependencyException)
			{
				return InternalServerError(guestDependencyException.InnerException);
			}
			catch(GuestServiceException guestServiceException)
			{
				return InternalServerError(guestServiceException.InnerException);
			}
		}
	}
}
