namespace TableBookingSystem.Web.Util.Response
{
    public class ApiResponse<T> 
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
