using System;
using System.IO;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Exceptions;
using IOSwiftUI.Core.ViewModels;
using Microsoft.Extensions.Configuration;

namespace IOSwiftUI.WebApi.Statics.ViewModels;

public class ImageAssetViewModel : ImageViewModel
{
    public FileStream GetImageFile(string publicId)
    {
        string fileName = GetFileName(publicId);
        if (String.IsNullOrEmpty(fileName))
        {
            throw new ImageNotFoundException();
        }

        string imagesFolder = Configuration.GetValue<string>(ConfigurationConstants.ImagesFolderKey);
        string imagePath = Path.Combine(imagesFolder, fileName);

        if (!File.Exists(imagePath))
        {
            throw new ImageNotFoundException();
        }

        return File.OpenRead(imagePath);
    }
}
