using ImageConverter.Interface;
using ImageConverter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using OfficeOpenXml;
using System.Security.Claims;
using System.Text;
using OfficeOpenXml.Core.ExcelPackage;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.ExtendedProperties;
using ExcelDataReader;

namespace ImageConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUpload _upload;

        public HomeController(ILogger<HomeController> logger,IUpload upload)
        {
            _logger = logger;
            _upload = upload;
        }

        [Route("/clear-image-maker")]
        public IActionResult ImageConverter()
        {
            return View();
        }

        [Route("clear-image-maker/list")]
        public IActionResult ClearImageMaker(UploadFile uploadFile)
        {

            var folderPath = @"C:\Users\Nishant\source\repos\ImageConverter\ImageConverter\UploadedImgs\";

            var (FilePath, Succeeded) = _upload.UploadFileCode(uploadFile.file,folderPath,uploadFile.FileName);
            /*string filename = @"C:\Users\Nishant\Desktop\resu.jpg";*/ //source image location

            var savedimg = @"C:\Users\Nishant\source\repos\ImageConverter\ImageConverter\UploadedImgs\"+ uploadFile.FileName;

            Bitmap img = new Bitmap(savedimg);   //image to bitmap                 
            var name = "result1.jpg";  //your output image name
            
            Compress(img, @"C:\Users\Nishant\source\repos\ImageConverter\ImageConverter\ConvertedImage\" + name, 100); //level 0~100, 0 is the worst resolution

            Byte[] b = System.IO.File.ReadAllBytes(@"C:\Users\Nishant\source\repos\ImageConverter\ImageConverter\ConvertedImage\result1.jpg");

            return Ok();
        }

        public async void ExcelUpload()
        {
            string filepath= @"\C:\\Users\\Nishant\\Downloads\\QueryForSelectCandidate.xlsx\";

            IFormFile file = null;
            
            string ExcelUploadPath = @"C:\Users\Nishant\source\repos\ImageConverter\ImageConverter\ExcelUpload\";
            


        }


        [Route("img-bse-64")]
        public async Task<IActionResult> ImgToBase64()
        
        {
            Application excelApp = new Application();


            string excelpath = @"C:\Users\Nishant\source\repos\ImageConverter\ImageConverter\ExcelUpload\QueryForSelectCandidate.xlsx";

            using(var package = new ExcelPackage(new FileInfo(excelpath)))
            {
                var firstsheet = package.Workbook.Worksheets["QueryForSelectCandidate.xlsx"];
                
            }

            //using (var stream = File.Open(excelpath,FileMode.Open))
            //{
            //    using (var reader = ExcelReaderFactory.CreateReader(stream))
            //    {

            //    }
            //}
            string filepath = @"C:\Users\Nishant\Desktop\CSIR-465-images\10000412\sign-10000412.jpg";

            byte[] imageArray = System.IO.File.ReadAllBytes(filepath);

            

            string filebase64 = Convert.ToBase64String(imageArray);




            return Ok();
        }


        public static void Compress(Bitmap srcBitMap, string destFile, long level)
        {
            Stream s = new FileStream(destFile, FileMode.Create); //create FileStream,this will finally be used to create the new image 
            Compress(srcBitMap, s, level);  //main progress to compress image
            s.Close();
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        private static void Compress(Bitmap srcBitmap, Stream destStream, long level)
        {
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, level);
            myEncoderParameters.Param[0] = myEncoderParameter;
            srcBitmap.Save(destStream, myImageCodecInfo, myEncoderParameters);
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
}