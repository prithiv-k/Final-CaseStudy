using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services for controllers with views
builder.Services.AddControllersWithViews(); // Enables [Route] + View

// ✅ Register named HttpClient for API calls
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5043/"); // Your Web API base URL
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// ✅ Enable session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Adjust as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ✅ Error handling
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Admin/Error");
}

// ✅ Use static files, routing, and session
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

// ✅ Enable attribute-based routing
app.MapControllers(); // This supports all [Route()] in your controllers

app.Run();