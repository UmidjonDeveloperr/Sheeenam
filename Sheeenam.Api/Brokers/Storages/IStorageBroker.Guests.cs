//================================================
//Copiright(c) Coalition of Good-Hearted Engineers
//Free To Use To Find Comfort and Peace 
//================================================

using Sheeenam.Api.Models.Foundations.Guests;
using System.Threading.Tasks;

namespace Sheeenam.Api.Brokers.Storages
{
	public partial interface IStorageBroker
	{
		ValueTask<Guest> InsertGuestAsync(Guest guest);
	}
}
