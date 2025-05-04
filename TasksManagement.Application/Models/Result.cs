namespace TasksManagement.Application.Models;

public readonly record struct Result<T>(
    bool IsSuccess,
    T? Value = default,
    string? Error = null
)
{
    public bool IsFailure => !IsSuccess;

    public static Result<T> Success(T value) => 
        new(true, value);

    public static Result<T> Failure(string error) => 
        new(false, default, error);

    public static implicit operator bool(Result<T> result) => 
        result.IsSuccess;

    public override string ToString() => 
        IsSuccess
            ? $"Success({Value})"
            : $"Failure({Error})";
}

public readonly record struct Result(
    bool IsSuccess,
    string? Error = null
)
{
    public bool IsFailure => !IsSuccess;

    public static Result Success() => 
        new(true);

    public static Result Failure(string error) => 
        new(false, error);

    public static implicit operator bool(Result result) =>
        result.IsSuccess;

    public override string ToString() => 
        IsSuccess ? "Success" : $"Failure({Error})";
}

