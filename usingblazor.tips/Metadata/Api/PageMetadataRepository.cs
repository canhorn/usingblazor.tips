namespace usingblazor.tips.Metadata.Api;

using System.Collections.Generic;
using usingblazor.tips.Metadata.Model;

public interface PageMetadataRepository
{
    PageNavigation Nav();
    IEnumerable<PageMetadataModel> All();
    PageMetadataModel Get(
        string route
    );
}
