using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

/// <summary>
/// Summary description for UrlQuery
/// </summary>
public class UrlQuery
{
    /// <summary>
    /// Base on current page
    /// </summary>
    public UrlQuery()
    {
        this.url = HttpContext.Current.Request.Url.AbsolutePath;
    }
    /// <summary>
    /// Base on other page
    /// </summary>
    /// <param name="value">The url of the page to reference, i.e.: '/path/to/folder/page.aspx?param1=1&param2=2'</param>
    public UrlQuery(string value)
    {
        int q = value.IndexOf('?');
        if (q != -1)
        {
            this.url = value.Substring(0, q);
            this.queryString = NameValueCollection(value);
        }
        else
        {
            this.url = value;
        }
    }
    /// <summary>
    /// Get and set Url parameters
    /// </summary>
    public string this[string param]
    {
        get
        {
            return this.Get(param);
        }
        set
        {
            this.Set(param, value);
        }
    }

    private string url;
    /// <summary>
    /// The Url of the page, without QueryString
    /// </summary>
    /// <value>/path/to/folder/page.aspx</value>
    public string Url
    {
        get
        {
            return this.url;
        }
    }
    /// <summary>
    /// Returns the virtual folder the page is in
    /// </summary>
    /// <value>/path/to/folder/</value>
    public string VirtualFolder
    {
        get
        {
            return this.Url.Substring(0, Url.LastIndexOf("/") + 1);
        }
    }
    /// <summary>
    /// The AbsoluteUri
    /// </summary>
    /// <value>page.aspx?param1=1&param2=2</value>
    public string AbsoluteUri
    {
        get
        {
            return this.Url + this.Get();
        }
    }
    private NameValueCollection queryString;
    /// <summary>
    /// Get the QueryString for the page
    /// </summary>
    public NameValueCollection QueryString
    {
        get
        {
            if (this.queryString != null)
            {
                return this.queryString;
            }
            else
            {
                this.queryString = new NameValueCollection(HttpContext.Current.Request.QueryString);
                return this.queryString;
            }
        }
    }
    /// <summary>
    /// Get the QueryString
    /// </summary>
    /// <returns>String in the format ?param1=1&param2=2</returns>
    public string Get()
    {
        string query = "";
        if (this.QueryString.Count != 0)
        {
            query = "?";
            for (int i = 0; i <= this.QueryString.Count - 1; i++)
            {
                if (i != 0)
                {
                    query += "&";
                }
                query += this.QueryString.GetKey(i) + "=" + this.QueryString.Get(i);
            }
        }
        return query;
    }
    /// <summary>
    /// Get parameter from QueryString
    /// </summary>
    /// <param name="param">Parameter to get</param>
    /// <returns>Parameter Value</returns>
    public string Get(string param)
    {
        return this.QueryString[param];
    }
    /// <summary>
    /// Set QueryString parameter
    /// </summary>
    /// <param name="param">Parameter to set</param>
    /// <param name="value">Value of parameter</param>
    public void Set(string param, string value)
    {
        if (param != string.Empty)
        {
            if (value == string.Empty || value == null)
            {
                this.QueryString.Remove(param);
            }
            else
            {
                this.QueryString[param] = value;
            }
        }
    }
    public void Remove(string removeParams)
    {
        string[] List;
        if (removeParams == "")
            List = new string[1];
        else
            List = removeParams.Split(new Char[] { ';' });

        for (int i = 0; i < List.Length; i++)
        {
            try
            {
                this.QueryString.Remove(List[i]);
            }
            catch
            { 
                //do nothing if param is not available
            }
        }    
    }
    /// <summary>
    /// Convert QueryString string to NameValueCollection
    /// http://groups.google.co.uk/groups?hl=en&lr=&ie=UTF-8&safe=off&selm=uyMZ2oaZDHA.652%40tk2msftngp13.phx.gbl
    /// </summary>
    public static NameValueCollection NameValueCollection(string qs)
    {
        NameValueCollection nvc = new NameValueCollection();
        if (String.IsNullOrEmpty(qs)) return nvc;
        //strip string data before the question mark
        qs = qs.IndexOf('?') > 0 ? qs.Remove(0, qs.IndexOf('?') + 1) : qs;
        Array sqarr = qs.Split("&".ToCharArray());
        for (int i = 0; i < sqarr.Length; i++)
        {
            string[] pairs = sqarr.GetValue(i).ToString().Split("=".ToCharArray());
            nvc.Add(pairs[0], pairs[1]);
        }
        return nvc;
    }
    /// <summary>
    /// Copies a form paramater to the QueryString
    /// </summary>
    /// <param name="param">Form Parameter</param>
    public void FormToQuery(string param)
    {

        this.Set(param, HttpContext.Current.Request.Form[param]);
    }
}
