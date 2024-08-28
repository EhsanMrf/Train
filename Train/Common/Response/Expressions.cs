
namespace Common.Response
{
    public static class ResponseExpressions
    {

        public static T ReturnDataOrThrow<T>(this T data, System.Exception exception)
        {
            return data == null ? throw exception : data;
        }

        public static void ReturnDataOrThrow(this bool isExists, System.Exception exception)
        {
            if (isExists) throw exception;
        }


        public static ServiceResponse<DataList<T>> ReturnDataOrInstance<T>(this DataList<T> data)
        {
            if (data==null || !data.Items.Any())
                return new ServiceResponse<DataList<T>>();

            return new ServiceResponse<DataList<T>>
            {
                Data = data,
                StatusCode = 200,
                Message = "SuccessFully"
            };
        }

        public static ServiceResponse<T> ReturnDataOrInstance<T>(this T data)
        {
            if (data == null)
                return new ServiceResponse<T>
                {
                    Message = "Not Found",
                    StatusCode = 402,
                };
            return new ServiceResponse<T>(data)
            {
                Message = "SuccessFully",
                StatusCode = 200,
            };
        }
    }
}
