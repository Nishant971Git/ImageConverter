using ImageConverter.Interface;

namespace ImageConverter.Service
{
    public class UploadService : IUpload
    {
        public (string filePath, bool succeeded) UploadFileCode(IFormFile file, string folderPath, string fileName)
        {
            try
            {
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/DocumentUpload/", folderPath)))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/DocumentUpload/", folderPath));
                }
                string text = Guid.NewGuid().ToString() + "-" + Path.GetFileName(file.FileName);
                string path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/DocumentUpload", folderPath, fileName);
                using (FileStream target = new FileStream(path2, FileMode.Create))
                {
                    file.CopyTo(target);
                }
                return (fileName, true);
            }
            catch (Exception)
            {
                return (null, false);
            }
        }



    }
}
