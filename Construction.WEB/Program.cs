//this class contains the services and parameters What we're going to use in the web
using Construction.WEB;
using Construction.WEB.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7000") });

builder.Services.AddSweetAlert2();

builder.Services.AddScoped<IRepository,Repository>();   

await builder.Build().RunAsync();
