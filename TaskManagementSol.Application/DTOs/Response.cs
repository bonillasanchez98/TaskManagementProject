namespace TaskManagementSol.Application
{
    /// <summary>
    /// Esta clase servira de ayudar para dar una respuesta formatada al cliente.
    /// </summary>
    public class Result
    {

        public bool IsSuccess { get; set; }
        public string Message{ get; set; }
        public dynamic? Data { get; set; }

        public Result() { }

        public Result(bool isSuccess, string message, dynamic? data = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static Result Success(string message, dynamic? data = null)
        {
            return new Result(true, message, data);
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }
    }

}
