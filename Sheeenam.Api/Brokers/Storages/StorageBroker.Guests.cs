<<<<<<< Updated upstream
﻿using Microsoft.EntityFrameworkCore;
=======
﻿//================================================
//Copiright(c) Coalition of Good-Hearted Engineers
//Free To Use To Find Comfort and Peace 
//================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
>>>>>>> Stashed changes
using Sheeenam.Api.Models.Foundations.Guests;

namespace Sheeenam.Api.Brokers.Storages
{
	public partial class StorageBroker
	{
		public DbSet<Guest> Guests { get; set; }
	}
}
