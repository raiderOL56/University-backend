var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1.- Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();

// 2.- Supported Cultures
string[] supportedCultures = new string[] { "en-US", "es-MX", "fr-FR" };
RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0]) // English by default
    .AddSupportedCultures(supportedCultures) // Add all supported cultures
    .AddSupportedUICultures(supportedCultures); // Add all supported cultures to UI

// 3.- Add localization to app
app.UseRequestLocalization(localizationOptions);    

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
