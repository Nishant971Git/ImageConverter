namespace ImageConverter.Models
{
    public class UploadFile
    {
        public string Name { get; set; }
        public IFormFile file { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
