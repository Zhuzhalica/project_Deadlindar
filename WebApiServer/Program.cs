using System;
using System.Security.Claims;
using System.Text;
using Deadlindar.Authorization;
using Deadlindar.Models;
using Deadlindar.Repositories;
using Deadlindar.Repositories.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Server.Data;
using WebAPI.Server.Services;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<EventContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IJsonRepository, JsonRepositoryIndividual>();

builder.Services.AddSingleton<INotificationRepository, NotificationRepositoryJson>();
builder.Services.AddSingleton<IEventRepository, EventRepositoryJson>();
builder.Services.AddSingleton<IUserRepository, UserRepositoryDatabase>();
builder.Services.AddSingleton<ILoginGroupsRepository, LoginGroupsRepositoryJson>();
// builder.Services.AddSingleton<IJsonRepository<JsonRepositoryCommon>, GroupRepositoryJson>();
builder.Services.AddSingleton<IGroupRepository, GroupRepositoryJson>();

builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IGroupService, GroupService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/Account/login");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();