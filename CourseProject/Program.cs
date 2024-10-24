using CloudinaryDotNet;
using CourseProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Westwind.AspNetCore.Markdown;

var builder = WebApplication.CreateBuilder(args);
Account account = new Account("diwwzq0co", "249617682517815", "yun22SoAmqeOtTOKSFZGiveNMIA");
Cloudinary cloudinary = new Cloudinary(account);


builder.Services.AddControllersWithViews();
builder.Services.AddMarkdown();
builder.Services.AddSingleton(cloudinary);
builder.Services.AddDbContext<CustomFormsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CustomForms")));


builder.Services.AddIdentity<User, IdentityRole>()
				 .AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<CustomFormsDbContext>()
				.AddDefaultTokenProviders()
				.AddDefaultUI(); 
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using(var scope = app.Services.CreateScope()) 
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var roles = new[] { "Admin", "User" };
	foreach(var role in roles)
	{
		if(!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
	}
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
	string email = "admin@gmail.com";
	string name = "admin";
	string password = "Aa123456_;";
    if(await userManager.FindByEmailAsync(email) == null)
	{
		var user = new User();
		user.Email = email;
		user.UserName = name;
		await userManager.CreateAsync(user,password);
		await userManager.AddToRoleAsync(user, "Admin");
	}
   
}


app.Run();
