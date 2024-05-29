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

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization(); 

app.MapControllerRoute(
    name: "LeetWelcome",
    pattern: "{controller=Leets}/{action=Welcome}");

app.MapControllerRoute(
    name: "default", 
    pattern: "Leets/Translatefunapi",
    defaults: new { controller = "Leets", action = "GetSavedFunTranslation"});


app.Run(); 