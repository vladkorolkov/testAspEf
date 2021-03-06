using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using southSoundWebsite.Models;

namespace southSoundWebsite.Controllers;

public class HomeController : Controller
{
    private const string _sessionKey = "";
    private readonly IWebHostEnvironment _appEnv;
    private readonly ILogger<HomeController> _logger;
    private readonly reportsContext _db;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webenv, reportsContext db)
    {
        _logger = logger;
        _appEnv = webenv;
        _db = db;
    }

    public IActionResult Index()
    {

        return View();

    }

    [HttpPost]
    public IActionResult QueryReport(string aartistName)
    {

        HttpContext.Session.SetString(_sessionKey, aartistName);

        return RedirectToAction("ReportShow");

    }
    public IActionResult ReportShow()
    {


        string artistName = HttpContext.Session.GetString(_sessionKey);
        var query = (from q in _db.Ex2s
                     where q.Исполнитель == artistName
                     select q).ToList();

        ViewData["Artist"] = artistName;
        return View(query);
    }

    [HttpGet]
    public IActionResult DownloadReportFile()
    {
        if (_sessionKey != null)
        {
            string artistName = HttpContext.Session.GetString(_sessionKey);
            var query = (from q in _db.Ex2s
                         where q.Исполнитель == artistName
                         select q).ToList();

            DataTable dt = new DataTable("Grid");

            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Альбом"),
                                        new DataColumn("Трек"),
                                        new DataColumn("Площадка"),
                                        new DataColumn("Загрузки/прослушивания"),
                                        new DataColumn("Территория"),
                                        new DataColumn("ISRC"),
                                        new DataColumn("Вознаграждение"), });

            foreach (var line in query)
            {
                dt.Rows.Add(line.НазваниеАльбома,
                            line.НазваниеТрека,
                            line.Площадка,
                            line.КоличествоЗагрузокПрослушиваний,
                            line.Территория,
                            line.IsrcКонтента,
                            line.ВознаграждениеВРубБезНдс);

            }

            using (var stream = new MemoryStream())
            {
                using (ExcelPackage ep = new ExcelPackage(stream))
                {
                    var workSheet = ep.Workbook.Worksheets.Add("report_" + artistName + "_" + DateTime.Now);
                    workSheet.Cells.LoadFromDataTable(dt, true);
                    workSheet.Cells["A1:H1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A1:H1"].Style.Font.Bold = true;
                    ep.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
                }
            }
        }

    }
    public IActionResult QueryReport()
    {
        return View();
    }
    public IActionResult Portoflio()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public IActionResult Releases()
    {

        return View();
    }
}
