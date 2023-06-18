using System.Net.Mime;
using System.Text.Json;
using Appointment.Application.Appointments;
using Appointment.Application.Doctors;
using Appointment.Domain.Appointments;
using Appointment.Domain.Doctors;
using Appointment.RestApi;
using Appointment.RestApi.Doctors;
using DevArt.Core.Config;
using DevArt.Core.ErrorHandling;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<AkkaService>();
builder.Services.AddSingleton(provider => AkkaService.ActorSystem);
builder.Services.WireUpDevArtCore(builder.Configuration);
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is BusinessException ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new{ErrorCode = ex.Code, ErrorMessage = ex.Message}));
        }
    });
});
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();