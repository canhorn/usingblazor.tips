namespace usingblazor.tips.Metadata.Api;

using System.Collections.Generic;
using System.Reflection;

public class PageMetadataSettings
{
    public IEnumerable<Assembly> PageAssemblyList { get; }

    public PageMetadataSettings(
        IEnumerable<Assembly> pageAssemblyList
    )
    {
        PageAssemblyList = pageAssemblyList;
    }
}
