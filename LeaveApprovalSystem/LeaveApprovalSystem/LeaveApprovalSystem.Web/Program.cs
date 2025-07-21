using LeaveApprovalSystem.Core.Interfaces;
using LeaveApprovalSystem.Data;
using LeaveApprovalSystem.Domain;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Service Registrations
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// EF Core: SQL Server context
builder.Services.AddDbContext<LeaveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Dependency Injection: Repositories & Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Session State
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// App Pipeline
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();



