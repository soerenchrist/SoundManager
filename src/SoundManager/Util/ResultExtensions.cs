using System.Text.Json;
using Ardalis.Result;

namespace SoundManager.Util;

public static class ResultExtensions
{
    public static string ErrorMessages(this Result result)
    {
        if (!result.Errors.Any()) return string.Empty;
        return JsonSerializer.Serialize(result.Errors);
    }

    public static string ErrorMessages<T>(this Result<T> result)
    {
        if (!result.Errors.Any()) return string.Empty;
        return JsonSerializer.Serialize(result.Errors);
    }
}