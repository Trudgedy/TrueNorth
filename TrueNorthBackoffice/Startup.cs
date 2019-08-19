using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TrueNorthBackoffice.Identities;

namespace TrueNorthBackoffice
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			#region Authentication
			services.AddDbContext<AuthentificationContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole<Int32>>()
				.AddEntityFrameworkStores<AuthentificationContext>()
				.AddRoles<IdentityRole<Int32>>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
				opt.SignIn.RequireConfirmedEmail = true;
			});
			services.AddCors();

			services.Configure<DataProtectionTokenProviderOptions>(o =>
				o.TokenLifespan = TimeSpan.FromDays(7));

			//Jwt Authentification
			var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = false;
				x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero

				};
			});
			#endregion


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
