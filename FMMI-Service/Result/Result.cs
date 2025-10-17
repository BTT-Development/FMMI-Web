namespace FMMI_Service.Result;

public class Result
{
    public bool IsSucces { get; protected set; }
    public string Message { get; protected set; }

    public static Result Succes(string message)
    {
        return new Result { IsSucces = true, Message = message };
    }
    public static Result Fail(string message)
    {
        return new Result { IsSucces = false, Message = message };
    }
}

public class Result<T> : Result
{
    public T? Data { get; private set; }

    public new static Result<T> Succes(T data, string message)
    {
        return new Result<T> { IsSucces = true, Message = message, Data = data };
    }
    public new static Result<T> Fail(string message)
    {
        return new Result<T> { IsSucces = false, Message = message };
    }
}
