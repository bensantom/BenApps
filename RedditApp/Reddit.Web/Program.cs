using Reddit.Data;
using Reddit.Domain.Data;
using Reddit.Domain.Services.Abstractions;
using Reddit.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IRedditApi, RedditApi>();
builder.Services.AddSingleton<IRedditService, RedditService>();
builder.Services.AddSingleton<IServiceManager, ServiceManager>();
builder.Services.AddDistributedMemoryCache();//for session
builder.Services.AddSession();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFile:Path"].ToString());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
