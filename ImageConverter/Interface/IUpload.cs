namespace ImageConverter.Interface
{
    public interface IUpload
    {
        public (string filePath, bool succeeded) UploadFileCode(IFormFile file, string folderPath, string fileName);
    }
}
