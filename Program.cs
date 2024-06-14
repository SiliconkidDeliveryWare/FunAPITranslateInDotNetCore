var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
 if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Setting the Default Files
app.UseDefaultFiles();

//Adding Static Files Middleware Component to serve the static files
app.UseStaticFiles();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization(); 

app.MapControllerRoute(
    name: "default", 
    pattern: "Leets/Index",
    defaults: new { controller = "Leets", action = "Index"});

app.MapControllerRoute(
    name: "LeetWelcome",
    pattern: "{controller=Leets}/{action=Welcome}");


//This will Run the Application
app.Run(); 