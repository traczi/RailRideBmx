using Application.Models.Product;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Application.Services;

public class ImageService : IImageService
{

    private readonly Cloudinary _cloudinary;

    public ImageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    public string UploadImage(string url)
    {
        var uploadParams = new ImageUploadParams{
            File = new FileDescription(url)};
        var uploadResult = _cloudinary.Upload(uploadParams);
        return uploadResult.PublicId;
    }

    public string ImageUrl(string publicId)
    {
        var baseUrl = "https://res.cloudinary.com/dnmiqn9pk/image/upload/";
        var url = $"{baseUrl}{publicId}.jpg";
        return url;
    }
}