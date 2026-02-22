namespace AdobeMPClient.Extensions;

public sealed class RouteBuilder
{
    private string _template;
    private readonly Dictionary<string, string> _routeValues = new();

    public RouteBuilder(string template)
    {
        _template = template;
    }

    public RouteBuilder WithRouteValue(string key, string value)
    {
        _routeValues[key] = value;
        return this;
    }

    public string Build(string baseUrl)
    {
        var path = _template;

        foreach (var kv in _routeValues)
        {
            var placeholder = "{" + kv.Key + "}";
            path = path.Replace(placeholder, Uri.EscapeDataString(kv.Value));
        }

        var normalizedBase = baseUrl.TrimEnd('/');
        var normalizedPath = path.TrimStart('/');

        return $"{normalizedBase}/{normalizedPath}";
    }
}