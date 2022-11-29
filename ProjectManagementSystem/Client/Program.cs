using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ProjectManagementSystem.Client;
using ProjectManagementSystem.Client.Services;
using static ProjectManagementSystem.Client.Services.GenericRepoService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ProjectManagementSystem.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ProjectManagementSystem.ServerAPI"));
builder.Services.AddMudServices();
builder.Services.AddScoped<IGenericRepositoryService, GenericRepositoryService>();
builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
