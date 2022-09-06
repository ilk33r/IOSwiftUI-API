using System;
using System.IO;
using IOBootstrap.NET.Common.Utilities;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace IOSwiftUI.Core.ViewModels;

public class ImageViewModel : ViewModel
{
    public string SaveFile(IFormFile file)
    {
        if (file.Length < 16)
        {
            throw new ImageCorruptException();
        }

        byte[] jpegImage;
        try {
            Image rawImage = Image.Load(file.OpenReadStream());
            jpegImage = ResizedAndEncodeImage(rawImage);
        }
        catch (Exception e)
        {
            Logger.LogDebug("{0}", e.StackTrace);
            throw new ImageCorruptException();
        }

        if (jpegImage == null)
        {
            throw new ImageCorruptException();
        }

        string imagesFolder = Configuration.GetValue<string>(ConfigurationConstants.ImagesFolderKey);
        string folderName = DateTimeOffset.UtcNow.ToString("yyyy-MM");
        string folderPath = Path.Combine(imagesFolder, folderName);

        if (!Directory.Exists(folderPath))
        {
            try
            {
                Directory.CreateDirectory(folderPath);
            }
            catch
            {
                throw new ImageSaveException();
            }
        }

        string newFileName = IORandomUtilities.GenerateGUIDString();
        string newFilePath = String.Format("{0}/{1}.jpg", folderName, newFileName);
        string filePath = Path.Combine(imagesFolder, newFilePath);
        
        try
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(jpegImage, 0, jpegImage.Length);
            fileStream.Flush();
            return filePath;
        }
        catch
        {
            throw new ImageSaveException();
        }
    }

    public void RemoveFile(string fileName)
    {
        string imagesFolder = Configuration.GetValue<string>(ConfigurationConstants.ImagesFolderKey);
        string filePath = Path.Combine(imagesFolder, fileName);

        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
            }
            catch
            {
                throw new ImageNotFoundException();
            }
        }
    }

    private byte[] ResizedAndEncodeImage(Image image)
    {
        int currentWidth = image.Width;
        int currentHeight = image.Height;

        MemoryStream memoryStream = new MemoryStream();
        if (currentWidth <= 1920 && currentHeight <= 1080)
        {
            image.SaveAsJpeg(memoryStream);
            memoryStream.Flush();

            return memoryStream.ToArray();
        }

        float imageAspectRatio = (float)currentWidth / (float)currentHeight;
        float canvasAspectRatio = (float)1920 / (float)1080;
        float resizeFactor = 0;

        if (imageAspectRatio > canvasAspectRatio) {
            resizeFactor = (float)1920 / currentWidth;
        } else {
            resizeFactor = (float)1080 / currentHeight;
        }

        float scaledWidth = (float)currentWidth * resizeFactor;
        float scaledHeight = (float)currentHeight * resizeFactor;

        image.Mutate(im => im.Resize((int)scaledWidth, (int)scaledHeight, true));

        image.SaveAsJpeg(memoryStream);
        memoryStream.Flush();

        return memoryStream.ToArray();
    }
}
