using EduBackend.Source.Model.Common;

namespace EduBackend.Source.Model.DTO.Common;

public class DataPageDto<T>
{
  public int Page { get; set; }
  public int PageSize { get; set; }
  public int TotalCount { get; set; }
  public int TotalPages { get; set; }
  public IEnumerable<T> Data { get; set; }

  public static DataPageDto<T> fromDataPage<TE>(DataPage<TE> dataPage, Func<TE, T> mapper)
  {
    var mapped = dataPage.Data.Select(mapper);
    
    return new DataPageDto<T>(
      dataPage.Page,
      dataPage.PageSize,
      dataPage.TotalCount,
      dataPage.TotalPages,
      mapped
    );
  }

  public DataPageDto(int page, int pageSize, int totalCount, int totalPages, IEnumerable<T> data)
  {
    Page = page;
    PageSize = pageSize;
    TotalCount = totalCount;
    TotalPages = totalPages;
    Data = data;
  }
}