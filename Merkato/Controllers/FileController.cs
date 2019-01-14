using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace Merkato.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class FileController : ControllerBase
    {
        private readonly IHostingEnvironment _appEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public FileController(IHostingEnvironment hostingEnvironment )
        {
            _appEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("upload")]
        public String UploadImage()
        {
            String result = string.Empty;


            var httpRequest = HttpContext.Request.Form.Files;

            var files = new List<string>();
            foreach (var Image in httpRequest)
            {

                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    var postedFile = httpRequest[0];
                    //There is an error here
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                        var filePath = Path.Combine(_appEnvironment.WebRootPath, "images\\Agents");

                        using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                        {
                            file.CopyToAsync(fileStream);

                            files.Add(filePath);

                            result = fileName;
                        }

                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        
        [HttpPost("UploadData")]
        public async Task<IActionResult> UploadFile(IFormFile files)
        {
            if (files == null || files.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(_appEnvironment.WebRootPath, "images\\Agents");

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }

            return RedirectToAction("Files");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

       

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }



        //public ActionResult DownloadFile()
        //{
        //    string filename = "File.pdf";
        //    string filepath = AppDomain.CurrentDomain.BaseDirectory + "/Path/To/File/" + filename;
        //    byte[] filedata = System.IO.File.ReadAllBytes(filepath);
        //    string contentType = MimeMapping.GetMimeMapping(filepath);

        //    var cd = new System.Net.Mime.ContentDisposition
        //    {
        //        FileName = filename,
        //        Inline = true,
        //    };

        //    Response.AppendHeader("Content-Disposition", cd.ToString());

        //    return File(filedata, contentType);
        //}



        //public async Task<IActionResult> Download(string filePath)
        //{
        //    try
        //    {
        //        if (filePath == null)
        //            return Content("filename not present");

        //        var userData = _userManager.GetUserAsync(User).Result;
        //        if (userData == null)
        //            return null;

        //        //Vérification autorisation d'acces au repertoire
        //        if (!filePath.Contains(userData.Id))
        //            return RedirectToAction("AccessDenied", "Account", new { area = "" });

        //        var cloudBlock = AzureBlobHelper.GetCloudBlockBlobData(_iConfiguration, filePath);

        //        var memory = new MemoryStream();

        //        await cloudBlock.DownloadToStreamAsync(memory);

        //        memory.Position = 0;
        //        var filename = Path.GetFileName(filePath);
        //        return File(memory, GetContentType(filename), filename);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erreur Boîte de réception");
        //        throw ex;
        //    }

        //}

        //private string GetContentType(string path)
        //{
        //    var types = GetMimeTypes();
        //    var ext = Path.GetExtension(path).ToLowerInvariant();
        //    return types[ext];
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("upload")]
        //public async Task<IActionResult> UploadFilesAsyncActionResult(List<IFormFile> files)
        //{
        //    var filesPath = $"{this._appEnvironment.WebRootPath}/files";

        //    foreach (var file in files)
        //    {
        //        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

        //        // Ensure the file name is correct
        //        fileName = fileName.Contains("\\")
        //            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
        //            : fileName.Trim('"');

        //        var fullFilePath = Path.Combine(filesPath, fileName);

        //        if (file.Length <= 0)
        //        {
        //            continue;
        //        }

        //        using (var stream = new FileStream(fullFilePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //    }

        //    return this.Ok();
        //}
    }

}