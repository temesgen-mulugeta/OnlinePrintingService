using System.ComponentModel.DataAnnotations;
using System.Net;

namespace OnlinePrintingService.Models
{
    public abstract class ApiResponse
    {
        public bool StatusIsSuccessful { get; set; }
        public HttpStatusCode ResponseCode { get; set; }

        public ErrorStateResponse ErrorState { get; set; }
        public string ResponseResult { get; set; }
    }

    public abstract class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }
}