using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for FileTemplates
/// </summary>
namespace LegoWebSite.DataProvider
{
    public static class FileTemplateDataProvider
    {
        public static string get_LabelTemplateFile(string expectedTemplateName)
        {
            HttpRequest Request = HttpContext.Current.Request;
            String retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/" + HttpContext.Current.Session["lang"].ToString() + "/" + expectedTemplateName + ".lbl";

            if (File.Exists(retFileName))
            {
                return retFileName;
            }
            else
            {
                retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/default.lbl";
            }
            return retFileName;
        }

        public static string get_XsltTemplateFile(string expectedTemplateName)
        {
            HttpRequest Request = HttpContext.Current.Request;
            String retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/" + HttpContext.Current.Session["lang"].ToString() + "/" + expectedTemplateName + ".xsl";
            if (File.Exists(retFileName))
            {
                return retFileName;
            }
            else
            {
                retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/default.xsl";
            }
            return retFileName;
        }

        public static string get_XsltTemplateFile(string expectedTemplateName, bool bDefaultIfNotExist)
        {
            HttpRequest Request = HttpContext.Current.Request;
            String retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/" + HttpContext.Current.Session["lang"].ToString() + "/" + expectedTemplateName + ".xsl";
            if (File.Exists(retFileName))
            {
                return retFileName;
            }
            else if (bDefaultIfNotExist)
            {
                retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/default.xsl";
            }
            else
            {
                return null;
            }
            return retFileName;
        }

        public static string get_WorkformTemplateFile(string expectedTemplateName)
        {
            HttpRequest Request = HttpContext.Current.Request;
            String retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/" + HttpContext.Current.Session["lang"].ToString() + "/" + expectedTemplateName + ".wfm";
            if (File.Exists(retFileName))
            {
                return retFileName;
            }
            else
            {
                retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/default.wfm";
            }
            return retFileName;
        }

        public static string get_HtmlTemplateFile(string expectedTemplateName)
        {
            HttpRequest Request = HttpContext.Current.Request;
            String retFileName = HttpContext.Current.Application["FCKeditor:UserFilesPhysicalPath"].ToString() + "File/Templates/" + HttpContext.Current.Session["lang"].ToString() + "/" + expectedTemplateName + ".htm";
            if (File.Exists(retFileName))
            {
                return retFileName;
            }
            else
            {
                retFileName = null;
            }
            return retFileName;
        }

    }
}