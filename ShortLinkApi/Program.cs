using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShortLink.Application.Interfaces;
using ShortLink.Application.Services;
using ShortLink.Domain.Interface;
using ShortLink.Infra.Data.Context;
using ShortLink.Infra.Data.Repositories;
using ShortLink.Infra.IoC;
using ShortLinkApi.Middelware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "ShortLinkapi ",
		Description = "ShortLinkapi ASP.NET Core Web API",
		TermsOfService = new Uri("https://ShortLinkapi.bashiridev.ir/terms"),

		License = new OpenApiLicense
		{
			Name = "Use under LICX",
			Url = new Uri("https://ShortLinkapi.bashiridev.ir/license"),
		}
	});
	// Set the comments path for the Swagger JSON and UI.
	//var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	//var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	//c.IncludeXmlComments(xmlPath);
});
#region db context
builder.Services.AddDbContext<ShortLinkContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ShortLinkSqlConnection"));
            });
#endregion
#region repository
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
#endregion

#region service
builder.Services.AddScoped<ILinkService, LinkService>();
#endregion
builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
            });
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
//app.UseShortLinkUrlRedirect();



app.UseAuthorization();

app.MapControllers();

app.Run();
