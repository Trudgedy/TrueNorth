using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrueNorthBackoffice.Identities
{
	public class AuthentificationContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
	{
		public AuthentificationContext(DbContextOptions<AuthentificationContext> options) : base(options)
		{

		}
	}
}
