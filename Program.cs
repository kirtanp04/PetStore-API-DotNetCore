using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Server;
using Server.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });


// Add services to the container.


// Config DB

builder.Services.AddDbContext<DBContext>(options =>
{
    var connectionStr = builder.Configuration.GetConnectionString("local");
    options.UseSqlServer(connectionStr);
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Setting Cors policy 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AdvancedCorsPolicy", builder =>
    {
        builder.WithOrigins("https://your-allowed-origin.com")  // Allow only this origin
               .AllowAnyHeader()   // Allow any headers
               .WithMethods("GET", "POST")  // Allow only GET and POST methods
                .WithExposedHeaders("X-Custom-Header")
               .AllowCredentials()
               .SetPreflightMaxAge(TimeSpan.FromMinutes(5));  // Cache preflight for 5 minutes
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AdvancedCorsPolicy");

app.UseMiddleware<GlobalExceptionMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
