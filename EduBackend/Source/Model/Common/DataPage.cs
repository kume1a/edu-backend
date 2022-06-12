using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Model.Common;

public class DataPage<T>
{
  public int Page { get; }
  public int PageSize { get; }
  public int TotalCount { get; }
  public int TotalPages { get; }
  public List<T> Data { get; }

  public static async Task<DataPage<T>> FromQuery(
    IQueryable<T> source,
    int page,
    int pageSize)
  {
    var totalCount = source.Count();
    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    var data = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

    return new DataPage<T>(page, pageSize, totalCount, totalPages, data);
  }

  private DataPage(int page, int pageSize, int totalCount, int totalPages, List<T> data)
  {
    Page = page;
    PageSize = pageSize;
    TotalCount = totalCount;
    TotalPages = totalPages;
    Data = data;
  }
}