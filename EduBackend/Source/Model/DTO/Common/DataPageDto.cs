using EduBackend.Source.Model.Common;

namespace EduBackend.Source.Model.DTO.Common;

public class DataPageDto<T>
{
  public int Page { get; set; }
  public int PageSize { get; set; }
  public int TotalCount { get; set; }
  public int TotalPages { get; set; }
  public List<T> Data { get; set; }

  public static DataPageDto<T> fromDataPage(DataPage<T> dataPage)
  {
    return new DataPageDto<T>(
      dataPage.Page,
      dataPage.PageSize,
      dataPage.TotalCount,
      dataPage.TotalPages,
      dataPage.Data
    );
  }

  public DataPageDto(int page, int pageSize, int totalCount, int totalPages, List<T> data)
  {
    Page = page;
    PageSize = pageSize;
    TotalCount = totalCount;
    TotalPages = totalPages;
    Data = data;
  }
}