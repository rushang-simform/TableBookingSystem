using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TableBookingSystem.Application.DTOs.Response
{
    public class GenericResponse<T> : BaseResponse
        where T : class
    {
        public T Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Data);
        }

        public static GenericResponse<T> Fail(string errorMessage)
        {
            return new GenericResponse<T> { IsSuccess = false, Message = errorMessage };
        }

        public static GenericResponse<T> Success(T data)
        {
            return new GenericResponse<T> { IsSuccess = true, Data = data };
        }
    }

    public enum QueryReponseStatus
    {
        Found = 1,
        NotFound = 2,
        Deleted = 3
    }
    public class GenericQueryResponse<T> : BaseResponse
    {
        public QueryReponseStatus Status { get; set; }
        public T Data { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(Data);
        }
    }

    public class PureResponse : BaseResponse
    {

    }
}
