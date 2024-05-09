//this class contains the services and parameters What we're going to use in the api
using Construction.API.Data;
using Construction.API.Helpers;
using Construction.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//this line is the parameter for the connection whit the database
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name = Connectionwhitparams"));


//this is for the required parameters for the security of users
builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireUppercase = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IUserHelper, UserHelper>();

//this is for the seed of the database
builder.Services.AddTransient<SeedDb>();


var app = builder.Build();

SeedData(app);

static void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();


    //this is for allow the cors (Cross-Origin Resource Sharing) of any domain
    app.UseCors(x => x

    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()


    );

    app.Run();
}