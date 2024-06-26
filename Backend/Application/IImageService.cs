﻿using CloudinaryDotNet.Actions;

namespace Application.Services;

public interface IImageService
{
    string UploadImage(string url);
    string ImageUrl(string publicId);
}