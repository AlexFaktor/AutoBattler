using App.GameCore.Tools.ShellImporters;
using Microsoft.AspNetCore.Mvc;

namespace App.ContentManagementSystemAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AdventureController
{
    private readonly IServiceProvider _serviceProvider;

    private readonly DownloaderGameConfigService _downloader;

    public AdventureController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _downloader = serviceProvider.GetRequiredService<DownloaderGameConfigService>();
    }
}