using SpotifyRestApi.Interfaces;
using SpotifyRestApi.SpotifyRepositories;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddScoped<IApiPlayerRepository, SpotifyPlayerRepository>();
builder.Services.AddScoped<IApiAuthenticationRepository, SpotifyAuthRepository>();
builder.Services.AddScoped<IApiUserRepository, SpotifyUserRepository>();
builder.Services.AddScoped<IApiPlaylistRepository, SpotifyPlaylistRepository>();


builder.Services.AddControllers(item => item.AllowEmptyInputInBodyModelBinding = true).AddNewtonsoftJson();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://api.spotify.com");
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
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
