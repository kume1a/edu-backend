using EduBackend.Source.Common.Model;

namespace EduBackend.Source.Common.Helper;

public interface IImageMutator
{
  Task<ImageMeta> ResizeWidthAndSave(ImageMeta imageMeta, int width);
  
  Task<ImageMeta> BlurAndSave(ImageMeta imageMeta, float sigma);
}