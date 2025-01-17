using AngularApp5.Server.Data;
using AngularApp5.Server.Errors;
using AngularApp5.Server.Extensions;
using AngularApp5.Server.Helpers;
using AngularApp5.Server.Interfaces;
using AngularApp5.Server.Middleware;
using AngularApp5.Server.Models.Identity;
using AngularApp5.Server.Repos;
using AngularApp5.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:52523") //angular app address
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

//adding db contexts
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));



//adding identity services
builder.Services.AddIdentityServices(builder.Configuration);
//var identityBuilder = builder.Services.AddIdentityCore<AppUser>();
//identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
//identityBuilder.AddEntityFrameworkStores<UserContext>();
//identityBuilder.AddSignInManager<SignInManager<AppUser>>();
//builder.Services.AddAuthentication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gymfinity API", Version = "v1" });
});
//builder.Services.AddSwaggerGen();

//overwriting api behavior to get a list of errors
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});


var app = builder.Build();

//user seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var identityContext = services.GetRequiredService<UserContext>();
        await identityContext.Database.MigrateAsync();
        await UserContextSeed.SeedUsersAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while configuring the UserManager.");
    }
}


//using middleware for custom error handling
app.UseMiddleware<ExceptionMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gymfinity API v1"); });
}

//redirecting blank endpoints to ErrorController
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//using cors
app.UseCors("AllowAngularApp");

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
