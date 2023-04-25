using System.Collections.Generic;
using System.Linq;

namespace API.Model.Models
{
    public class PagedResult<T>
    {
        public int Offset { get; set; }

        public int Total { get; set; }

        public int Count => Results.Count();

        public IEnumerable<T> Results { get; set; } = new List<T>();

        public PagedResult() { }

        public PagedResult(int offset, int total, IEnumerable<T> results)
        {
            Offset = offset;
            Total = total;
            Results = results ?? new List<T>();
        }
    }
}
