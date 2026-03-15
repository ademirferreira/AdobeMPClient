namespace AdobeMPClient.Extensions;

public static class UriExtensions
{
    public static string AddQueryParam(this string url, string key, string? value)
    {
        if (string.IsNullOrEmpty(value)) return url;

        var separator = url.Contains("?") ? "&" : "?";
        var encodedValue = Uri.EscapeDataString(value);

        return $"{url}{separator}{key}={encodedValue}";
    }

    public static string AddQueryParam(this string url, string key, int? value)
    {
        return value.HasValue ? url.AddQueryParam(key, value.ToString()) : url;
    }

    public static string AddQueryParam(this string url, string key, bool? value)
    {
        return value.HasValue ? url.AddQueryParam(key, value.ToString()!.ToLower()) : url;
    }

    public static string AddQueryParam(this string url, string key, IEnumerable<string>? values)
    {
        var list = values?.ToList();

        if (list is not { Count: > 0 })
            return url;

        foreach (var value in list)
            url = url.AddQueryParam(key, value);

        return url;
    }
}
