using University_Backend.Data;
using Microsoft.OpenApi.Models;
// 1.- usings to work with EF
using Microsoft.EntityFrameworkCore;
using University_Backend.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// 9.- Config Swagger to take care of authorization of JWT
builder.Services.AddSwaggerGen(options =>
{
    // We define the Security for authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
// 4.- Here add custom services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddJwtTokenServices(builder.Configuration); // 7.- Add Service of JWT Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
}); // 8.- Add Authorization 
// 2.- Connetcion with SQL Server Express
// 3.- Add DBcontext
string? connectionString = configuration.GetConnectionString("UniversityDB");
builder.Services.AddDbContext<DbContext_University>(options => options.UseSqlServer(connectionString));
// 5.- CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicyName", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 6.- Tell app to use CORS
app.UseCors("CorsPolicyName");

app.Run();
