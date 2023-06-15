
using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Common.ASCII;
using URLShortenerAPI.Context;
using URLShortenerAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddDbContext(builder.Configuration);
string _policyName = "CorsPolicy";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: _policyName, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddRouting(options =>
{
    options.ConstraintMap["non-ascii"] = typeof(NonAsciiRouteConstraint);
});
builder.Services.AddMvc();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapFallback(async (ApiDbContext db, HttpContext httpContext) =>
{
    var getHttpPath = httpContext.Request.Path.ToUriComponent().Trim('/');
    var getHttpPathMatch = await db.Url.FirstOrDefaultAsync(x => x.ShortUrl.ToLower().Trim() == getHttpPath.ToLower().Trim());
    if (getHttpPathMatch == null) return Results.BadRequest("Invalid URL");
    return Results.Redirect(getHttpPathMatch.Url.ToLower());
});

app.MapControllers();
app.Run();
