using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class PaginatedDTO<T> where T : class
    {

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRecords { get; private set; }
        public List<T> Items { get; private set; }

        public PaginatedDTO(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalRecords = count;
            Items = items;
        }


        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }


        public static async Task<PaginatedDTO<T>> CreateAsync(List<T> source, int TotalCount, int pageIndex, int pageSize)
        {
            var items = source.ToList();
            return await Task.FromResult(new PaginatedDTO<T>(items, TotalCount, pageIndex, pageSize));
        }
    }
}
