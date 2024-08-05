using FirstProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder().Build();

var services = new ServiceCollection();

services.AddDbContext<DataContext>(x =>
    x.UseSqlServer(configuration.GetConnectionString("DataContext")));
//"Host=localhost;Port=5432;Database=UsersLunguage;Username=postgres;Password=111111";
//"Server=PS-3052023\TESTMSSQL;Port=1433;Database=TestLesson;Username=test;Password=test";
//services.AddTransient<UserService>();

//services.AddTransient<RoleService>();
//services.AddTransient<ProfessionService>();

//var sp = services.BuildServiceProvider();
