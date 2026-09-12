using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi;
using A2Template.Data;
using A2Template.Handler;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using WebAPIvCard.Helper;


public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SupportNonNullableReferenceTypes();
        });

        builder.Services
            .AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, A2AuthHandler>("Authentication", null);


        builder.Services.AddDbContext<A1DbContext>(options => options.UseSqlite(builder.Configuration["A2DBConnection"]));

        builder.Services.AddScoped<IA2Repo, A2Repo>();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("OrganizerOnly", policy => policy.RequireClaim(ClaimTypes.Role, "organizer"));
            options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "user"));
            options.AddPolicy("UserOrOrganizer", policy => policy.RequireRole("user", "organizer"));
        });

        builder.Services.AddMvc(options => options.OutputFormatters.Add(new CalendarOutputFormatter()));

        var app = builder.Build();

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

        app.Run();
    }
}
