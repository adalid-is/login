using CRUDPersonas;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configuration = builder.Configuration;
builder.Services.AddSingleton(configuration);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7143") });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTelerikBlazor();

await builder.Build().RunAsync();
