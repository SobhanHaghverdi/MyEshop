var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
#region Minification

builder.Services.AddWebMarkupMin(options =>
{
    options.AllowMinificationInDevelopmentEnvironment = true;
    options.AllowCompressionInDevelopmentEnvironment = true;
})
    .AddHtmlMinification()
    .AddHttpCompression();

#endregion
#region Db Context

builder.Services.AddDbContext<MyEshopContext>(options =>
{
    options.UseSqlServer("Data Source=.;Initial Catalog=Eshop_DB;Integrated Security=true");
});

#endregion
#region IoC

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

#endregion
#region Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/Logout";
        option.ExpireTimeSpan = TimeSpan.FromDays(10);
    });

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithRedirects("/404NotFound");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseWebMarkupMin();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/Admin"))
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.Redirect("/Account/Login");
        }
        else if (!bool.Parse(context.User.FindFirstValue("IsAdmin")))
        {
            context.Response.Redirect("/Account/Login");
        }
    }
    await next.Invoke();
});

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
