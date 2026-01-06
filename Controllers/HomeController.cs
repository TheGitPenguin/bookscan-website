using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bookScan_website.Models;
using bookScan_website.Services;

namespace bookScan_website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private VersionItemServices _versionItemServices;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _versionItemServices = new VersionItemServices();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Download()
    {
        VersionItem lastItem = _versionItemServices.GetLastVersionItem();
        ViewData["versionItem"] = lastItem;

        string serverVersion = lastItem.ServerVersion + "";
        string clientMajorVersion = lastItem.ClientMajorVersion + "";
        string clientMinorVersion = lastItem.ClientMinorVersion + "";
        string IdPublicationSteps = lastItem.IdPublicationSteps + "";

        string stringVersion = serverVersion + "." + clientMajorVersion + "." + clientMinorVersion + "." + IdPublicationSteps;

        ViewData["stringVersion"] = stringVersion;

        ViewData["urlDownload"] = "https://downloads.thepenguinontheweb.tech/bookscan/bookscan-"+ stringVersion + ".apk";

        return View();
    }

    public IActionResult Changelog()
    {
        IEnumerable<VersionItem> versionItems = _versionItemServices.GetAllVersionItems();

        return View(versionItems);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
