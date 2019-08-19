using System;
using Microsoft.AspNetCore.Identity;

namespace TrueNorthBackoffice.Identities
{
	public class ApplicationUser : IdentityUser<Int32>
	{
		public String Name { get; set; }
		public String Surname { get; set; }

	}
}
