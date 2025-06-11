namespace CarrerBot.Application._base;

public class Result
{
    #region Status
    public bool Succeeded { get; private set; }
    public bool Failed { get; private set; }
    public SvcErrorType ErrorType { get; private set; }
    #endregion Status

    #region Results
    public string Message { get; private set; } = string.Empty;
    #endregion Results

    #region Set Results
    public static Result SetResult(bool status)
    {
        if (status) return new Result { Succeeded = true };
        else return new Result { Failed = true };
    }

    public static Result SetSuccess()
    {
        var success = new Result { Succeeded = true };
        return success;
    }

    public static Result SetFailed(SvcErrorType type = SvcErrorType.NoResults, string message = null)
    {
        var result = new Result { ErrorType = type, Message = message, Failed = true };
        return result;
    }
    #endregion Set Results

    #region Helpers
    public override string ToString()
    {
        return Succeeded ? "Succeeded" : "Failed";
    }
    #endregion Helpers
}

public class Result<T>
{
    #region Status
    public bool Succeeded { get; private set; }
    public bool Failed { get; private set; }
    public SvcErrorType ErrorType { get; private set; }
    #endregion Status

    #region Results
    public T Value { get; set; }
    public string Message { get; private set; } = string.Empty;
    #endregion Results

    #region Set Results
    public static Result<T> SetSuccess(T value)
    {
        var success = new Result<T> { Value = value, Succeeded = true };
        return success;
    }

    public static Result<T> SetFailed(SvcErrorType type = SvcErrorType.NoResults, string message = null)
    {
        var result = new Result<T> { ErrorType = type, Message = message, Failed = true };
        return result;
    }
    #endregion Set Results

    #region Helpers
    public override string ToString()
    {
        return Succeeded ? "Succeeded" : "Failed";
    }
    #endregion Helpers
}
public class ResultList<T>
{
    #region Status
    public bool Succeeded { get; private set; }
    public bool Failed { get; private set; }
    public SvcErrorType ErrorType { get; private set; }
    #endregion Status

    #region Results
    public IEnumerable<T> Value { get; set; }
    public string Message { get; private set; } = string.Empty;
    #endregion Results

    #region Set Results
    public static ResultList<T> SetResult(IEnumerable<T> value = null)
    {
        if (value.Any()) return new ResultList<T> { Value = value, Succeeded = true };
        else return new ResultList<T> { Failed = true };
    }

    public static ResultList<T> SetSuccess(IEnumerable<T> value)
    {
        var success = new ResultList<T> { Value = value, Succeeded = true };
        return success;
    }

    public static ResultList<T> SetFailed(SvcErrorType type = SvcErrorType.NoResults, string message = null)
    {
        var result = new ResultList<T> { ErrorType = type, Message = message, Failed = true };
        return result;
    }
    #endregion Set Results
}

public enum SvcErrorType
{
    Failed,
    Forbidden,
    NoResults,
}
