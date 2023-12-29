using Microsoft.AspNetCore.Mvc;
using ParquetConverter.Models;
using Apache.Arrow;
using Apache.Arrow.Ipc;


#nullable enable
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
    Parquet.Rows.Table tbl = null;

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
                List<string> innerList = new List<string>();

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

    public async Task<IActionResult> GetFile()
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


    



    //public async Task<IActionResult> Createe()
   // public async Task<IActionResult> Createe()
   // {
        //if (ModelState.IsValid)
        //{
        //
//            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedFiles");
//            string fileName = Path.GetFileName(model.Parquet.FileName);
//            string filePath = Path.Combine(uploadsFolder, fileName);
        //    
        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        model.Parquet.CopyTo(fileStream);
        //    }

        //    using (var sr = new StreamReader(filePath.ToString()))
        //    {
        //        // Read the stream as a string, and write the string to the console.
        //        Console.WriteLine(sr.ReadToEnd());
        //    }
        //}

//https://stackoverflow.com/questions/20953475/how-to-open-multiple-windows-from-the-controller-in-asp-net-mvc

        //return "This is the Welcome action method...";
//        string fp = _webHostEnvironment.WebRootPath + "/parq";
//        using (StreamWriter writer = new StreamWriter(fp))
//        {
//        using(Stream fs = System.IO.File.OpenRead(filePath.ToString())) {
//            using(ParquetReader reader = await ParquetReader.CreateAsync(fs)) {
//                for(int i = 0; i < reader.RowGroupCount; i++) {
//                    using(ParquetRowGroupReader rowGroupReader = reader.OpenRowGroupReader(i)) {
//
//                        foreach(var df in reader.Schema.GetDataFields()) {
//                            var columnData = await rowGroupReader.ReadColumnAsync(df);
//                            foreach (var item in columnData.Data)
//                            {
//                                System.Console.WriteLine(item);      
//
//
//                                    writer.WriteLine(item);
//                                }                          
//                            }
//
//                            // do something to the column...
//                        }
//                    }
//                }
//            }
//        }
//
//
//
//        return View();
//    }
}