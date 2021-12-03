namespace usingblazor.tips.Metadata.Model;

using System.Reflection;
using Microsoft.AspNetCore.Components;
using usingblazor.tips.Localization;
using usingblazor.tips.Localization.Api;
using usingblazor.tips.Metadata.Api;

public class PageMetadataBase
    : ComponentBase,
    PageMetadata
{
    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;
    [Inject]
    public PageMetadataRepository Repository { get; set; } = null!;

    public PageMetadataModel PageMetadata { get; private set; } = new PageMetadataModel();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var route = GetType().GetCustomAttribute<RouteAttribute>()?.Template ?? "";
        if (string.IsNullOrEmpty(route))
        {
            return;
        }
        PageMetadata = Repository.Get(
            route
        );
    }
}
