using Common;
using WebFramework.Configuration;
using WebFramework.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


SiteSettings _siteSetting = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
#region Add DbContext
builder.Services.AddDbContext(builder.Configuration);
#endregion
#region Register Service
builder.Services.RegisterServices();
#endregion
#region Jwt Service
builder.Services.AddJwtAuthentication(_siteSetting.JwtSettings);
#endregion
#region Add Minimal Mvc
builder.Services.AddMinimalMvc();
#endregion
#region Add Auto Mapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion
//builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
