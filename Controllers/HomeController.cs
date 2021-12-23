using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using southSoundWebsite.Models;

namespace southSoundWebsite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
    //    reportsContext db = new reportsContext();
        
    //         string ArtistName = "Босс Терпит Фиаско";
    //         var q = (from x in db.Examples
    //                 where x.Исполнитель == ArtistName
    //                 select x).ToList();

      return View();
        

    }

    [HttpPost]
    public IActionResult QueryReport(string aartistName)
    {
        var query = OperationsDB.ReadFromDbAboutArtist(aartistName);     
        ViewData["Artist"]  = aartistName;
        return View(@"Views\Home\Report.cshtml", query) ;            
            
        
        
    }

    public IActionResult QueryReport( )
    { 
        return View();
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
