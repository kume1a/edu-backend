using System.Text;

namespace EduBackend.Source.Common.Helper;

public class ImageTypeResolver : IImageTypeResolver
{
  private readonly byte[] _bmpBytes = Encoding.ASCII.GetBytes("BM");
  private readonly byte[] _gifBytes = Encoding.ASCII.GetBytes("GIF");
  private readonly byte[] _tiffBytes = { 73, 73, 42 };
  private readonly byte[] _tiff2Bytes = { 77, 77, 42 };
  private readonly byte[] _jpegBytes = { 255, 216, 255, 224 };
  private readonly byte[] _jpeg2Bytes = { 255, 216, 255, 225 };
  private readonly byte[] _pngBytes = { 137, 80, 78, 71 };  
  
  public Task<ImageFormat> ResolveImageType(byte[] imageBytes)
  {
    if (_bmpBytes.SequenceEqual(imageBytes.Take(_bmpBytes.Length)))
    {
      return Task.FromResult(ImageFormat.Bmp);
    }

    if (_gifBytes.SequenceEqual(imageBytes.Take(_gifBytes.Length)))
    {
      return Task.FromResult(ImageFormat.Gif);
    }

    if (_pngBytes.SequenceEqual(imageBytes.Take(_pngBytes.Length)))
    {
      return Task.FromResult(ImageFormat.Png);
    }

    if (_tiffBytes.SequenceEqual(imageBytes.Take(_tiffBytes.Length)))
    {
      return Task.FromResult(ImageFormat.Tiff);
    }

    if (_tiff2Bytes.SequenceEqual(imageBytes.Take(_tiff2Bytes.Length)))
    {
      return Task.FromResult(ImageFormat.Tiff);
    }

    if (_jpegBytes.SequenceEqual(imageBytes.Take(_jpegBytes.Length)))
    {
      return Task.FromResult(ImageFormat.Jpeg);
    }

    if (_jpeg2Bytes.SequenceEqual(imageBytes.Take(_jpeg2Bytes.Length)))
    {
      return Task.FromResult(ImageFormat.Jpeg);
    }

    return Task.FromResult(ImageFormat.Unknown);
  }
}