namespace Application;

public interface IImageService
{
    string UploadImage(string url);
    string ImageUrl(string publicId);
}