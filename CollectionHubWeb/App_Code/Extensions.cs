using System.Collections.Specialized;
using System.Web;

/// <summary>
/// Summary description for Extensions
/// </summary>
public static class Extensions
{
    public static string HtmlEncode(this string data)
    {
        return HttpUtility.HtmlEncode(data);
    }

    public static string HtmlDecode(this string data)
    {
        return HttpUtility.HtmlDecode(data);
    }

    public static NameValueCollection ParseQueryString(this string query)
    {
        return HttpUtility.ParseQueryString(query);
    }

    public static string UrlEncode(this string url)
    {
        return HttpUtility.UrlEncode(url);
    }

    public static string UrlDecode(this string url)
    {
        return HttpUtility.UrlDecode(url);
    }

    public static string UrlPathEncode(this string url)
    {
        return HttpUtility.UrlPathEncode(url);
    }
}
