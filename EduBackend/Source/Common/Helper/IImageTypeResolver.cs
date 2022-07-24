namespace EduBackend.Source.Common.Helper;

public enum ImageFormat
{
  Bmp,
  Jpeg,
  Gif,
  Tiff,
  Png,
  Unknown
}

public interface IImageTypeResolver
{
  Task<ImageFormat> ResolveImageType(byte[] imageBytes);
}