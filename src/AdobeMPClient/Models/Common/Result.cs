namespace AdobeMPClient.Models.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public Error? Error { get; set; }
    public int StatusCode { get; }

    private Result(bool isSuccess, T? data, Error? error, int statusCode)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T data, int statusCode = 200)
        => new Result<T>(true, data, null, statusCode);

    public static Result<T> Failure(Error error, int statusCode)
        => new Result<T>(false, default, error, statusCode);

    public TResult Match<TResult>(Func<T, int, TResult> onSuccess, Func<Error?, int, TResult> onFailure)
    {
        return IsSuccess
            ? onSuccess(Data!, StatusCode)
            : onFailure(Error, StatusCode);
    }
}