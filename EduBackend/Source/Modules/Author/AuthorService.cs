using EduBackend.Source.Common.Helper;
using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
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

  public async Task<AuthorDto> CreateAuthor(string name, IFormFile image)
  {
    var imageMeta = await _imageWriter.WriteImage(image);
    var resizedImageMeta = await _imageMutator.ResizeWidthAndSave(imageMeta, 300);
    var blurImageMeta = await _imageMutator.BlurAndSave(resizedImageMeta, 12);

    await Task.Run(() => File.Delete(imageMeta.Path));

    var author = await _authorRepository.CreateEntity(
      name,
      imagePath: resizedImageMeta.Path,
      blurImagePath: blurImageMeta.Path
    );

    return _authorMapper.ShallowMap(author);
  }

  public async Task<AuthorDto> UpdateAuthor(long authorId, string? name, IFormFile? image)
  {
    if (!await _authorRepository.ExistsById(authorId))
    {
      throw new NotFoundException(ExceptionMessageCode.AuthorNotFound);
    }

    string? imagePath = null;
    string? blurImagePath = null;
    if (image != null)
    {
      var imageMeta = await _imageWriter.WriteImage(image);
      var resizedImageMeta = await _imageMutator.ResizeWidthAndSave(imageMeta, 300);
      var blurImageMeta = await _imageMutator.BlurAndSave(resizedImageMeta, 12);

      imagePath = resizedImageMeta.Path;
      blurImagePath = blurImageMeta.Path;

      await Task.Run(() => File.Delete(imageMeta.Path));
    }

    var oldImages = await _authorRepository.GetImagePathsById(authorId);

    var author = await _authorRepository.UpdateById(
      authorId,
      name,
      imagePath: imagePath,
      blurImagePath: blurImagePath
    );

    if (oldImages is not null && imagePath is not null && blurImagePath is not null)
    {
      await Task.Run(
        () =>
        {
          File.Delete(oldImages.ImagePath);
          File.Delete(oldImages.BlurImagePath);
        }
      );
    }

    return _authorMapper.ShallowMap(author!);
  }

  public async Task DeleteAuthorById(long id)
  {
    var oldImages = await _authorRepository.GetImagePathsById(id);

    var didDelete = await _authorRepository.DeleteById(id);
    if (!didDelete)
    {
      throw new NotFoundException(ExceptionMessageCode.AuthorNotFound);
    }

    if (oldImages is not null)
    {
      await Task.Run(
        () =>
        {
          File.Delete(oldImages.ImagePath);
          File.Delete(oldImages.BlurImagePath);
        }
      );
    }
  }
}