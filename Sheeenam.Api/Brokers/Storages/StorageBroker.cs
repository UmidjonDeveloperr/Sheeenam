using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sheeenam.Api.Models.Foundations.Guests;
using System;
using System.Configuration.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace Sheeenam.Api.Brokers.Storages
{

	public partial class StorageBroker : EFxceptionsContext, IStorageBroker
	{
		private readonly IConfiguration configuration;

		public StorageBroker(IConfiguration configuration)
		{
			this.configuration = configuration;
			this.Database.Migrate();
		}

		private IQueryable<T> SelectAll<T>() where T : class
		{
			var broker = new StorageBroker(this.configuration);

			return broker.Set<T>();
		}

		private async ValueTask<T> SelectAsync<T>(params object[] objectId) where T : class
		{
			var broker = new StorageBroker(this.configuration);
			
			return await broker.FindAsync<T>(objectId);
		}

		private async ValueTask<T> UpdateAsync<T> (T @object)
		{
			var broker = new StorageBroker(this.configuration);
			broker.Entry(@object).State = EntityState.Modified;
			await broker.SaveChangesAsync();
			return @object;
		}

		private async ValueTask<T> DeleteAsync<T>(T @object)
		{
			var broker = new StorageBroker(this.configuration);
			broker.Entry(@object).State = EntityState.Deleted;
			await broker.SaveChangesAsync();
			return @object;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connectionString =
				this.configuration.GetConnectionString(name: "DefaultConnection");

			optionsBuilder.UseSqlServer(connectionString);
		}

		public override void Dispose() { }
	}
}
