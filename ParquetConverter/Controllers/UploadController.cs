using Microsoft.AspNetCore.Mvc;
using ParquetConverter.Models;
using Apache.Arrow;
using Apache.Arrow.Ipc;


namespace ParquetConverter.Controllers;

public class UploadController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UploadController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        return View();
    }

    [Microsoft.AspNetCore.Mvc.HttpPost]
    public async Task< Microsoft.AspNetCore.Mvc.ActionResult> UploadFileNew(ParquetModel file)
    {
    Parquet.Rows.Table? tbl = null;

    if (file != null && file.Parquet != null && file.Parquet.Length > 0)
        {
            using (var stream = file.Parquet.OpenReadStream())
            {
                tbl = await Parquet.Rows.Table.ReadAsync(stream);
            }
            ListViewModel model = new ListViewModel{ListOfLists = new List<List<string>>()};
            System.Console.WriteLine(tbl[0]);
            foreach(var j in tbl)
            {
                List<string>? innerList = new List<string>();

                try{
                    for (int i = 0; i < j.Count();i++){                                                
                            innerList.Add(j[i].ToString());
                        }
                    model.ListOfLists.Add(innerList);
                }
                catch {
                }

            }
            return View("ListDisplay", model);

        }
    else{
            return View("Upload");
        }
    }

    [Microsoft.AspNetCore.Mvc.HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    public  IActionResult GetFile()
    {
        string fp = _webHostEnvironment.WebRootPath + "\\parq.txt";
        FileStream stream = System.IO.File.OpenRead(fp);
        return new Microsoft.AspNetCore.Mvc.FileStreamResult(stream, "application/octet-stream");
    }

    public static async Task<RecordBatch> ReadParquetAsync(string filename)
    {
        using (var stream = System.IO.File.OpenRead(filename))
        {
            var fileReader = new ArrowFileReader(stream);
            var recordBatch = await fileReader.ReadNextRecordBatchAsync();
            return recordBatch;
        }
    }
}