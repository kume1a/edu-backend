using EduBackend.Source.Common.Model;
using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;

namespace EduBackend.Source.Common.Helper;

public class ImageWriter : IImageWriter
{
  private readonly IImageTypeResolver _imageTypeResolver;

  public ImageWriter(IImageTypeResolver imageTypeResolver)
  {
    _imageTypeResolver = imageTypeResolver;
  }

  public async Task<ImageMeta> WriteImage(IFormFile image)
  {
    await ValidateImageFile(image);

    return await WriteFile(image);
  }

  private async Task ValidateImageFile(IFormFile file)
  {
    using var ms = new MemoryStream();
    await file.CopyToAsync(ms);
    var fileBytes = ms.ToArray();

    var imageFormat = await _imageTypeResolver.ResolveImageType(fileBytes);
    if (imageFormat == ImageFormat.Unknown)
    {
      throw new UnsupportedMediaTypeException(ExceptionMessageCode.InvalidImage);
    }
  }

  private static async Task<ImageMeta> WriteFile(IFormFile file)
  {
    if (!Directory.Exists(Constants.PublicResourcePath))
    {
      Directory.CreateDirectory(Constants.PublicResourcePath);
    }

    var extension = $".{file.FileName.Split('.')[^1]}";
    var fileName = Guid.NewGuid().ToString();
    var path = Path.Combine(
      Directory.GetCurrentDirectory(),
      Constants.PublicResourcePath,
      $"{fileName}{extension}"
    );

    await using var bits = new FileStream(path, FileMode.Create);

    await file.CopyToAsync(bits);

    return new ImageMeta(
      filename: fileName,
      extension: extension,
      fileNameWithExtension: $"{fileName}{extension}",
      path: Path.Combine(Constants.PublicResourcePath, $"{fileName}{extension}")
    );
  }
}