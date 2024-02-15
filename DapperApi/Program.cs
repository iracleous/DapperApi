using DapperApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IPersonCrud, PersonCrud>();

builder.Services.AddControllers();

//cors 1/3 
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//cors 2/3 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//cors 3/3
app.UseCors(MyAllowSpecificOrigins);


app.Run();
