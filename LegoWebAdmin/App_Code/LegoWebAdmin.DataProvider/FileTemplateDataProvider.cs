// ----------------------------------------------------------------------
// <copyright file="FileTemplateDataProvider.cs" package="LEGOWEB">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for FileTemplates
/// </summary>
namespace LegoWebAdmin.DataProvider
{
    public static class FileTemplateDataProvider
    {
        public static string get_LabelTemplateFile(string expectedTemplateName)
        {

            String retFileName= System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Templates/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "/" + expectedTemplateName + ".lbl";
                if (!File.Exists(retFileName))
                {                    
                    retFileName = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Templates/default.lbl";
                }
                return retFileName;
        }

        public static string get_XsltTemplateFile(string expectedTemplateName)
        {
            
            String retFileName = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Templates/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "/" + expectedTemplateName + ".xsl";
            if (File.Exists(retFileName))
            {
                return retFileName;
            }
            else
            {
                retFileName = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Templates/default.xsl";
            }
            return retFileName;
        }

        public static string get_WorkformTemplateFile(string expectedTemplateName)
        {
            
            String retFileName = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Templates/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "/" + expectedTemplateName + ".wfm";
            if (File.Exists(retFileName))
            {
                return retFileName;
            }
            else
            {
                retFileName = System.Configuration.ConfigurationSettings.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Templates/default.wfm";
            }
            return retFileName;
        }

    }
}