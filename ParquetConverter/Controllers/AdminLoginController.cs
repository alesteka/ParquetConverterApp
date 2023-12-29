using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using ParquetConverter.Models;
using System.IO;
//using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http.Abstractions;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using ParquetConverter.Models;
using System.IO;
using Parquet;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ParquetConverter.Controllers;

public class AdminLoginController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AdminLoginController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }    // 

    public IActionResult Index()
    {
        return View();
    }
    
}