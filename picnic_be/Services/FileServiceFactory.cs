namespace picnic_be.Services
{
    public interface IFileServiceFactory
    {
        IFileService CreateImagesFileService(string subject);
    }
    public class FileServiceFactory : IFileServiceFactory
    {
        public IFileService CreateImagesFileService(string subject)
        {
            string path = Path.Combine("Images", subject);
            return new FileService(path);
        }
    }
}
