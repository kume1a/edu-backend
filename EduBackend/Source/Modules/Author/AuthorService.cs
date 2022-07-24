using EduBackend.Source.Common.Helper;
using EduBackend.Source.Model.DTO.Author;
using EduBackend.Source.Model.Mapper.Author;

namespace EduBackend.Source.Modules.Author;

public class AuthorService : IAuthorService
{
  private readonly IAuthorMapper _authorMapper;
  private readonly IImageWriter _imageWriter;
  private readonly IAuthorRepository _authorRepository;
  private readonly IImageMutator _imageMutator;

  public AuthorService(
    IAuthorMapper authorMapper,
    IImageWriter imageWriter,
    IAuthorRepository authorRepository,
    IImageMutator imageMutator)
  {
    _authorMapper = authorMapper;
    _imageWriter = imageWriter;
    _authorRepository = authorRepository;
    _imageMutator = imageMutator;
  }

  public async Task<AuthorDto> CreateAuthor(string name, IFormFile formImage)
  {
    var imageMeta = await _imageWriter.WriteImage(formImage);
    var resizedImageMeta = await _imageMutator.ResizeWidthAndSave(imageMeta, 300);
    var blurImageMeta = await _imageMutator.BlurAndSave(resizedImageMeta, 12);

    File.Delete(imageMeta.Path);

    var author = await _authorRepository.CreateEntity(
      name,
      filePath: resizedImageMeta.Path,
      blurFilePath: blurImageMeta.Path
    );

    return _authorMapper.ShallowMap(author);
  }
}