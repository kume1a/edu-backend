namespace EduBackend.Source.Common.Model;

public class ImageMeta
{
  public string Filename { get; }

  public string Extension { get; }

  public string FileNameWithExtension { get; }

  public string Path { get; }

  public ImageMeta(string filename, string extension, string fileNameWithExtension, string path)
  {
    Filename = filename;
    Extension = extension;
    FileNameWithExtension = fileNameWithExtension;
    Path = path;
  }
}