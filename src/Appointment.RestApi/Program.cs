using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<ActorSystem>(provider =>
// {
//     var config = File.ReadAllText("app.conf");
//     var hocon = ConfigurationFactory.ParseString(config);
//     var bootstrap = BootstrapSetup.Create().WithConfig(hocon);
//     var di = DependencyResolverSetup.Create(provider);
//     var actorSystemSetup = bootstrap.And(di);
//     return ActorSystem.Create("the-rules", actorSystemSetup);
// });
// builder.Services.AddSingleton<ICommandDispatcher, InmemoryCommandDispatcher>();
// builder.Services.AddSingleton<IConstraintChecker, ConstraintChecker>();
// builder.Services.AddTransient<IRuleService, RuleService>();


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