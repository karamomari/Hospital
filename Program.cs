using Hospital.Models;
using Hospital.Repositories.Implementations;
using Hospital.Service.Implementations;
using Hospital.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {

                op.Password.RequiredLength = 4;
                op.Password.RequireDigit = false;
                op.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<MvcHospitalcontext>();



            builder.Services.AddDbContext<MvcHospitalcontext>(op =>
               op.UseSqlServer(
                  builder.Configuration.GetConnectionString("DB"),
                    sqlOptions =>
                    {
                       sqlOptions.CommandTimeout(180); 
                    })
                    );


            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(op =>
            {
                op.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            builder.Services.AddScoped<IPatientRecordRepository, PatientRecordRepository>();

            builder.Services.AddScoped<IDoctorService, DoctorService>();


            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseRouting(); //to direct to controller and action
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
