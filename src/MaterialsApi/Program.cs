using MaterialsApi.Extensions;
using MaterialsApi.Mapper.Profiles;
using MaterialsApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddCustomLogger(builder.Configuration);
builder.Services.AddScoped<ErrorMiddleware>();
builder.Services.AddCustomDbContext(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddCustomCors();
builder.Services.AddAutoMapper(typeof(MaterialsProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ErrorMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();