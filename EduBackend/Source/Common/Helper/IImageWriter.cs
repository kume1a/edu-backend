using EduBackend.Source.Common.Model;

namespace EduBackend.Source.Common.Helper;

public interface IImageWriter
{
  Task<ImageMeta> WriteImage(IFormFile image);
}