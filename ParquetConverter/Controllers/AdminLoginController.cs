using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using ParquetConverter.Models;
using System;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ParquetConverter.Controllers;

public class AdminLoginController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AdminLoginController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }
    
}