using Microsoft.EntityFrameworkCore;
using Sheeenam.Api.Models.Foundations.Guests;

namespace Sheeenam.Api.Brokers.Storages
{
	public partial class StorageBroker
	{
		public DbSet<Guest> Guests { get; set; }
	}
}
