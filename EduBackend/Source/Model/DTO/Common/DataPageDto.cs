using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Model.DTO.Common;

public class DataPageDto<T>
{
  public int Page { get; set; }
  public int PageSize { get; set; }
  public int TotalCount { get; set; }
  public int TotalPages { get; set; }
  public List<T> Data { get; set; }

  public static async Task<DataPageDto<T>> fromQuery
  (
    IQueryable<T> source,
    int page,
    int pageSize)
  {
    var totalCount = source.Count();
    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    var data = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

    return new DataPageDto<T>(page, pageSize, totalCount, totalPages, data);
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