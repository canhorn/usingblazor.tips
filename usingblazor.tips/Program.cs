namespace usingblazor.tips;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using usingblazor.tips.Localization.Api;
using usingblazor.tips.Localization.Model;
using usingblazor.tips.Metadata.Api;
using usingblazor.tips.Metadata.State;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var isHosted = builder.Configuration.GetValue<bool>("Hosted");
        if (!isHosted)
        {
            builder.RootComponents.Add<App>("#app"); 
            builder.RootComponents.Add<HeadOutlet>("head::after");
        }

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddSingleton(
            new PageMetadataSettings(
                new List<Assembly>
                {
                        typeof(Program).Assembly
                }
            )
        ).AddScoped<PageMetadataRepository, StandardPageMetadataRepository>();

        // I18n Services
        builder.Services
            .AddScoped(typeof(Localizer<>), typeof(StringBasedLocalizer<>))
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                            // Set Supported Locales
                            new CultureInfo("en-US"),
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en-US");
                        // Formatting numbers, dates, etc.
                        opts.SupportedCultures = supportedCultures;
                        // UI strings that we have localized.
                        opts.SupportedUICultures = supportedCultures;
                }
            );

        await builder.Build().RunAsync();
    }
}
