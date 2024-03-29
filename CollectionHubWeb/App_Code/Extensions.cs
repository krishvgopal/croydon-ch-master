﻿using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

/* 
    Written by Alexander Hitchins, 7th Nov 2014
  
    The MIT License (MIT)

    Copyright (c) 2014 - Hitchins IT Services Ltd

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
 */

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

    public static byte[] ReadFully(Stream input)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }

    //public static IDataReader GetDateTime(this object value)
    //{
    //    //return HttpUtility.UrlPathEncode(url);
    //}
}
