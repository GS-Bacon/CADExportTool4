namespace CADExportTool.Core.Models;

/// <summary>
/// 処理結果を表すジェネリッククラス（値あり）
/// </summary>
/// <typeparam name="T">結果の値の型</typeparam>
public class Result<T>
{
    /// <summary>処理が成功したかどうか</summary>
    public bool IsSuccess { get; }

    /// <summary>処理結果のデータ</summary>
    public T? Data { get; }

    /// <summary>エラーメッセージ</summary>
    public string? ErrorMessage { get; }

    /// <summary>発生した例外</summary>
    public Exception? Exception { get; }

    private Result(bool isSuccess, T? data, string? errorMessage, Exception? exception)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    /// <summary>成功結果を作成</summary>
    public static Result<T> Success(T data) => new(true, data, null, null);

    /// <summary>失敗結果を作成（エラーメッセージ）</summary>
    public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage, null);

    /// <summary>失敗結果を作成（例外）</summary>
    public static Result<T> Failure(Exception exception) => new(false, default, exception.Message, exception);

    /// <summary>失敗結果を作成（エラーメッセージと例外）</summary>
    public static Result<T> Failure(string errorMessage, Exception exception) => new(false, default, errorMessage, exception);
}

/// <summary>
/// 処理結果を表すクラス（値なし）
/// </summary>
public class Result
{
    /// <summary>処理が成功したかどうか</summary>
    public bool IsSuccess { get; }

    /// <summary>エラーメッセージ</summary>
    public string? ErrorMessage { get; }

    /// <summary>発生した例外</summary>
    public Exception? Exception { get; }

    private Result(bool isSuccess, string? errorMessage, Exception? exception)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    /// <summary>成功結果を作成</summary>
    public static Result Success() => new(true, null, null);

    /// <summary>失敗結果を作成（エラーメッセージ）</summary>
    public static Result Failure(string errorMessage) => new(false, errorMessage, null);

    /// <summary>失敗結果を作成（例外）</summary>
    public static Result Failure(Exception exception) => new(false, exception.Message, exception);

    /// <summary>失敗結果を作成（エラーメッセージと例外）</summary>
    public static Result Failure(string errorMessage, Exception exception) => new(false, errorMessage, exception);
}
