using EduBackend.Source.Common.Model;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace EduBackend.Source.Common.Helper;

public class ImageMutator : IImageMutator
{
  public async Task<ImageMeta> ResizeWidthAndSave(ImageMeta imageMeta, int width)
  {
    using var image = await Image.LoadAsync(imageMeta.Path);
    
    var aspectRatio = image.Width / (double)image.Height;
    var targetHeight = (int)Math.Floor(width / aspectRatio);

    image.Mutate(x => x.Resize(width, targetHeight));

    return await SaveImage(image, imageMeta.Extension);
  }

  public async Task<ImageMeta> BlurAndSave(ImageMeta imageMeta, float sigma)
  {
    using var image = await Image.LoadAsync(imageMeta.Path);

    image.Mutate(x => x.GaussianBlur(sigma));

    return await SaveImage(image, imageMeta.Extension);
  }

  private static async Task<ImageMeta> SaveImage(Image image, string extension)
  {
    var filename = Guid.NewGuid().ToString();
    var fileNameWithExtension = $"{filename}{extension}";
    var path = Path.Combine(Constants.PublicResourcePath, fileNameWithExtension);

    await image.SaveAsync(path);

    return new ImageMeta(
      filename: filename,
      extension: extension,
      fileNameWithExtension: fileNameWithExtension,
      path: path
    );
  }
}