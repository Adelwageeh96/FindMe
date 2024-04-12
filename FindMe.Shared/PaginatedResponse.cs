using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Shared
{
    public class PaginatedResponse<T> : Response
    {
        public PaginatedResponse() { }
        public PaginatedResponse(List<T> data)
        {
            Data = data;
        }

        public PaginatedResponse(bool succeeded, List<T> data = default,
                               string message = null!, int count = 0,
                               HttpStatusCode httpStatusCode = HttpStatusCode.OK,
                               int pageNumber = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = pageNumber;
            StatusCode = httpStatusCode;
            IsSuccess = succeeded;
            Message = message;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public new List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginatedResponse<T> Create(List<T> data, int count, int pageNumber, int pageSize)
        {
            return new PaginatedResponse<T>(true, data, null!, count, HttpStatusCode.OK, pageNumber, pageSize);
        }
    }
}
