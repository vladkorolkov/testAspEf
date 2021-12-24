using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using southSoundWebsite.Models;

namespace southSoundWebsite.Controllers;

public class HomeController : Controller
{
    private const string _sessionKey = "";
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
       
      return View();

    }

    [HttpPost]
    public IActionResult QueryReport(string aartistName)
    {
        
        HttpContext.Session.SetString(_sessionKey,aartistName);
            
        
        return  RedirectToAction("ReportShow");            
  
    }
    public IActionResult ReportShow()
    {
        ViewData["Artist"]  = _sessionKey;
        string value = HttpContext.Session.GetString(_sessionKey);
        var query = OperationsDB.ReadFromDbAboutArtist(value); 
        return View(query);
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
