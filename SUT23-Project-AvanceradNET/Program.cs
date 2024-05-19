
using ClassLibrary_Project_AvanceradNET.Models;
using Microsoft.EntityFrameworkCore;
using SUT23_Project_AvanceradNET.Data;
using SUT23_Project_AvanceradNET.Services;
using System;

namespace SUT23_Project_AvanceradNET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddScoped<IBookingSystem<Customer>, CustomerRepository>();
            builder.Services.AddScoped<IBookingSystem<Appointment>, AppointmentRepository>();
            builder.Services.AddScoped<IAppointment<Appointment>, AppointmentRepository>();
            builder.Services.AddScoped<IBookingSystem<ChangeLog>, ChangeLogRepository>();
            builder.Services.AddScoped<IAppointment<ChangeLog>, ChangeLogRepository>();
            builder.Services.AddScoped<IAuthorize<Company>, AuthorizeRepository>();



            builder.Services.AddDbContext<ProjectDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            var app = builder.Build();

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
        }
    }
}
