using Lab9.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lab9.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Lab9IdentityDbContextConnection");
builder.Services.AddDbContext<Lab9IdentityDbContext>(options =>options.UseSqlite(connectionString));


builder.Services
.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireUppercase = false;

    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<Lab9IdentityDbContext>();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFoxesRepository, FoxesRepository>();


builder.Services
    .AddAuthentication()
    .AddCookie()
    .AddJwtBearer(
                    JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Tokens:Issuer"],
                            ValidAudience = builder.Configuration["Tokens:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Tokens:Key"]))
                        };
                    }
                );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
    "IsAdminJwt",
    policy =>
    policy
    .RequireRole("Admin")
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    );
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseRouting();
app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.UseFileServer();



using (var scope = app.Services.CreateScope())
{
    using (var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>())
    using (var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>())
    {
        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
        foreach (var user in userManager.Users.Where(x => x.Email.EndsWith("@example.com")))
        {
            userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }
}

app.Run();

//administrator:
//user@example.com
//password


//normal user:
//simple@user.com
//password

