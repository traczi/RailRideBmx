namespace Application.IServices;

public interface IImageService
{
    string UploadImage(string url);
    string ImageUrl(string publicId);
}