using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ParquetConverter.Models;

namespace ParquetConverter.Controllers;

public class HomeController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;

    }
    public IActionResult Index()
    {
        return View();
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    public async Task<IActionResult> DownloadFile()
    {
        string fp = _webHostEnvironment.WebRootPath + "\\parq.txt";
        FileStream stream = System.IO.File.OpenRead(fp);
        return new Microsoft.AspNetCore.Mvc.FileStreamResult(stream, "application/octet-stream");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
