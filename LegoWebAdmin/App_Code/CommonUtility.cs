using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Xml;
using System.IO;

/// <summary>
/// Summary description for CommonUtility
/// </summary>

    public enum SortDirections { Asc, Desc }

    public static class CommonUtility
    {

        public static string HighLightText(string source, string key, bool HtmlTagEndcode, string CssSpanClass)
        {
            if (key.Trim() != "")
            {
                source = " " + source;
                string uSource = source.ToUpper();
                uSource = uSource.Replace(">", " ");
                uSource = uSource.Replace("*", " ");
                uSource = uSource.Replace("\"", " ");
                uSource = uSource.Replace("\'", " ");
                uSource = uSource.Replace("?", " ");
                uSource = uSource.Replace(".", " ");
                uSource = uSource.Replace(",", " ");

                string highlighttext = "";
                string uKey = " " + key.ToUpper();
                int f = 0;

                //only highlight word
                f = uSource.IndexOf(uKey);
                if (f > 0)
                {
                    while (f >= 0)
                    {
                        uSource = uSource.Substring(f + uKey.Length);
                        highlighttext = highlighttext + source.Substring(0, f + 1);
                        source = source.Substring(f + 1);
                        if (HtmlTagEndcode)
                        {
                            highlighttext = highlighttext + "&lt;span class='" + CssSpanClass + "'&gt;";
                        }
                        else
                        {
                            highlighttext = highlighttext + "<span class='" + CssSpanClass + "'>";
                        }
                        highlighttext = highlighttext + source.Substring(0, key.Length);
                        if (HtmlTagEndcode)
                        {
                            highlighttext = highlighttext + "&lt;/span&gt;";
                        }
                        else
                        {
                            highlighttext = highlighttext + "</span>";
                        }
                        source = source.Substring(key.Length);
                        f = uSource.IndexOf(uKey);
                    }
                    highlighttext = highlighttext + source;
                }
                else
                {
                    highlighttext = source;
                }
                return highlighttext;
            }
            else
            {
                return source;
            }
        }

        public static void setDefaultButton(Page page, TextBox txtControl, Button dButton)
        {

            string theScript = @"
            <SCRIPT language=""javascript"">

            <!--

            function fnTrapKD(btn, event){

            if (document.all){

              if (event.keyCode == 13){

               event.returnValue=false;

               event.cancel = true;

               btn.click();

              }

            }

            else if (document.getElementById){

              if (event.which == 13){

               event.returnValue=false;

               event.cancel = true;

               btn.click();

              }

            }

            else if(document.layers){

              if(event.which == 13){

               event.returnValue=false;

               event.cancel = true;

               btn.click();

              }

            }

            }

            // -->

            </SCRIPT>";

            page.RegisterStartupScript("ForceDefaultToScript", theScript);

            txtControl.Attributes.Add("onkeydown", "fnTrapKD(" + dButton.ClientID + ",event)");

        }

        public static object GetInitialValue(string parameterName, object defaultValue)
        {
            string value = null;

            if (HttpContext.Current.Request.QueryString[parameterName] != null)
                value = HttpContext.Current.Request.QueryString.GetValues(parameterName)[0];
            else
                if (HttpContext.Current.Request.Form[parameterName] != null)
                    value = HttpContext.Current.Request.Form.GetValues(parameterName)[0];

            return value == null ? defaultValue : value;
        }
        
        public static object GetInitialValue(string parameterName)
        {
            return GetInitialValue(parameterName, null);
        }

        public static void InitializeGridParameters(System.Web.UI.StateBag viewState, string formName, Type sortFields, int pageSize, int pageSizeLimit)
        {
            HttpRequest Request = HttpContext.Current.Request;
            string Param;
            int PageSize = pageSize;
            viewState[formName + "SortField"] = Enum.Parse(sortFields, "Default");
            viewState[formName + "PageNumber"] = 1;
            Param = Request.QueryString[formName + "Order"];
            if (Param != null && Param.Length > 0)
                try
                {
                    viewState[formName + "SortField"] = Enum.Parse(sortFields, Param);
                }
                catch { }
            Param = Request.QueryString[formName + "Dir"];
            if (Param == null || Param.Length == 0 || Param.ToLower() == "asc")
                viewState[formName + "SortDir"] = SortDirections.Asc;
            else
                viewState[formName + "SortDir"] = SortDirections.Desc;
            Param = Request.QueryString[formName + "Page"];
            int PageNumber;
            if (Param != null && Param.Length > 0)
                try
                {
                    PageNumber = Int32.Parse(Param);
                    if (PageNumber >= 0) viewState[formName + "PageNumber"] = PageNumber;
                }
                catch { }
            Param = Request.QueryString[formName + "PageSize"];
            if (Param != null && Param.Length > 0)
                try
                {
                    PageSize = Int32.Parse(Param);
                    if (PageSize <= 0) PageSize = pageSize;
                }
                catch { }
            if ((PageSize > pageSizeLimit || PageSize == 0) && pageSizeLimit != -1)
                PageSize = pageSizeLimit;
            viewState[formName + "PageSize"] = PageSize;
        }

    }

    public class LinkParameterCollection : NameObjectCollectionBase
    {
        protected void AddUnique(string name, string value)
        {
            if (BaseGet(name) == null)
                BaseAdd(name, value);
        }
        
        public void Add(string name, string value)
        {
            BaseAdd(name, value);
        }
       
        public void Add(string name, NameValueCollection values, string keyName)
        {
            if (values[keyName] != null)
                foreach (string val in values.GetValues(keyName))
                    BaseAdd(name, System.Web.HttpUtility.UrlEncode(val));
            else
                BaseAdd(name, "");
        }

        public string ToString(string preserve, string removeList)
        {
            return ToString(preserve, removeList, null);
        }

        public string ToString(string preserve, string removeList, System.Web.UI.StateBag viewState)
        {
            HttpRequest Request = HttpContext.Current.Request;
            HttpServerUtility Server = HttpContext.Current.Server;
            string[] List;

            if (removeList == "")
                List = new string[1];
            else
                List = removeList.Split(new Char[] { ';' });

            if (preserve == "All" || preserve == "GET")
            {
                if (viewState != null)
                {
                    int length = viewState.Count;
                    string[] keys = new string[length];
                    System.Web.UI.StateItem[] values = new System.Web.UI.StateItem[length];
                    viewState.Keys.CopyTo(keys, 0);
                    viewState.Values.CopyTo(values, 0);
                    string cvalue = "";
                    for (int i = 0; i < length; i++)
                    {
                        if (values[i].Value != null)
                            cvalue = values[i].Value.ToString();
                        else
                            cvalue = "";
                        string ckey = "";
                        if (keys[i].EndsWith("SortField") && cvalue != "Default")
                            ckey = keys[i].Replace("SortField", "Order");
                        if (keys[i].EndsWith("SortDir"))
                            ckey = keys[i].Replace("SortDir", "Dir");
                        if (keys[i].EndsWith("PageNumber") && cvalue != "1")
                            ckey = keys[i].Replace("PageNumber", "Page");
                        if (ckey != "" && Array.IndexOf(List, ckey) < 0)
                        {
                            if (this[ckey] == null) Add(ckey, cvalue); else this[ckey] = cvalue;
                        }
                    }
                }
                for (int i = 0; i < Request.QueryString.Count; i++)
                {
                    if (Array.IndexOf(List, Request.QueryString.AllKeys[i]) < 0 && BaseGet(Request.QueryString.AllKeys[i]) == null)
                        foreach (string val in Request.QueryString.GetValues(i))
                            Add(Request.QueryString.AllKeys[i], Server.UrlEncode(val));
                }
            }
            if (preserve == "All" || preserve == "POST")
                for (int i = 0; i < Request.Form.Count; i++)
                {
                    if (Array.IndexOf(List, Request.Form.AllKeys[i]) < 0
                        && Request.Form.AllKeys[i] != "__EVENTTARGET"
                        && Request.Form.AllKeys[i] != "__EVENTARGUMENT"
                        && Request.Form.AllKeys[i] != "__VIEWSTATE"
                        && BaseGet(Request.Form.AllKeys[i]) == null)
                        foreach (string val in Request.Form.GetValues(i))
                            Add(Request.Form.AllKeys[i], Server.UrlEncode(val));
                }

            return ToString("");
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string removeList)
        {
            string Params = ""; string[] List;
            if (removeList == "")
                List = new string[1];
            else
                List = removeList.Split(new Char[] { ';' });
            for (int i = 0; i < Count; i++)
            {
                if (Array.IndexOf(List, BaseGetKey(i)) < 0)
                    Params += BaseGetKey(i) + "=" + Convert.ToString(BaseGet(i)) + "&";
            }
            Params = Params.TrimEnd(new Char[] { '&' });
            if (Params.Length > 0) Params = "?" + Params;
            return Params;
        }

        public object this[string key]
        {
            get
            {
                return BaseGet(key);
            }
            set
            {
                BaseSet(key, value);
            }
        }

        public object this[int index]
        {
            get
            {
                return BaseGet(index);
            }
            set
            {
                BaseSet(index, value);
            }
        }
    }