// ----------------------------------------------------------------------
// <copyright file="FileMetaContentDataProvider.cs" package="LEGOWEB">
//     Copyright (C) 2011 LEGOWEB.ORG. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


using MarcXmlParserEx;
using System.IO;
using LegoWebAdmin.BusLogic;

/// <summary>
/// Summary description for MetaContentDataProvider
/// </summary>
namespace LegoWebAdmin.DataProvider
{
    public static class FileMetaContentDataProvider
    {
        public static CRecord get_DataRecord(String sCategory, string sID)
        {
            CRecords myRecs = new CRecords();
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            if (myRecs.Count > 0)
            {
                switch (sID.ToLower())
                {
                    case "first":
                        return myRecs.Record(0);
                        break;
                    case "last":
                        return myRecs.Record(myRecs.Count - 1);
                        break;
                    default:
                        int iID = int.Parse("0" + sID);
                        myRecs.Filter("001", sID, true);
                        if (myRecs.Count > 0)
                        {
                            return myRecs.Record(0);
                        }
                        else
                        {
                            CRecord myRec = new CRecord();
                            myRec.load_File(FileTemplateDataProvider.get_WorkformTemplateFile(sCategory));
                            return myRec;
                        }
                        break;
                }
            }
            else
            {
                CRecord myRec1 = new CRecord();
                myRec1.load_File(FileTemplateDataProvider.get_WorkformTemplateFile(sCategory));
                return myRec1;
            }
        }

        public static CRecords get_DataRecords(String sCategory)
        {
            CRecords myRecs = new CRecords();
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            return myRecs;        
        }
        public static CRecords get_DataRecords(String sCategory,String sSubCategory)
        {
            CRecords myRecs = new CRecords();
            CRecords retRecs = new CRecords();
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
                myRecs.Filter("003", sCategory + "." + sSubCategory, true);
                for (int i = myRecs.Count; i < myRecs.Count; i++)
                {
                    retRecs.Add(myRecs.Record(i));
                }
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            return retRecs;
        }


        public static DataTable get_DataTableRecords(String sCategory)
        {
            DataTable Data = new DataTable();
            Data.TableName = "CONTENT";
            Data.Columns.Add(new DataColumn("ID", typeof(int)));
            Data.Columns.Add(new DataColumn("TITLE", typeof(string)));
            Data.Columns.Add(new DataColumn("CATEGORY", typeof(string)));

            CRecords myRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
                if (myRecs.Count > 0)
                {
                    for (int i = myRecs.Count - 1; i >= 0; i--)
                    {
                        DataRow row = Data.NewRow();
                        row["ID"] = myRecs.Record(i).Controlfields.Controlfield("001").Value;
                        row["TITLE"] = myRecs.Record(i).Datafields.Datafield("245").Subfields.Subfield("a").Value;
                        row["CATEGORY"] = myRecs.Record(i).Controlfields.Controlfield("003").Value;
                        Data.Rows.Add(row);
                    }                
                }
            }
            return Data;        
        }
        public static CRecords get_LastDataRecords(String sCategory, int iNumberOfRecord)
        {
            CRecords myRecs = new CRecords();
            CRecords retRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            if (myRecs.Count <= iNumberOfRecord)
            {
                for (int i = myRecs.Count-1; i >=0; i--)
                {
                    retRecs.Add(myRecs.Record(i));
                }
            }
            else
            {
                for (int j = myRecs.Count - 1; j >= myRecs.Count - iNumberOfRecord; j--)
                {
                    retRecs.Add(myRecs.Record(j));
                }            
            }
            return retRecs;
        }

        public static CRecords get_LastDataRecords(String sCategory,String sSubCategory, int iNumberOfRecord)
        {
            CRecords myRecs = new CRecords();
            CRecords retRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }

            CRecords tempRecs = new CRecords();
            myRecs.Filter("003", sCategory + "." + sSubCategory, true);
            for (int k = 0; k < myRecs.Count; k++)
            {
                tempRecs.Add(myRecs.Record(k));
            }            
            if (tempRecs.Count <= iNumberOfRecord)
            {
                for (int i = tempRecs.Count - 1; i >= 0; i--)
                {
                    retRecs.Add(tempRecs.Record(i));
                }
            }
            else
            {
                for (int j = tempRecs.Count - 1; j >= myRecs.Count - iNumberOfRecord; j--)
                {
                    retRecs.Add(tempRecs.Record(j));
                }
            }
            return retRecs;
        }

        public static int add_DataRecord(String sCategory, CRecord addRecord)
        {

            addRecord.Controlfields.Controlfield("005").Value = DateTime.Now.ToLongDateString();
            CRecords myRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            int iID = 0;
            myRecs.Filter("001",iID.ToString(),true);
            while (myRecs.Count > 0)
            {
                iID++;
                myRecs.Refresh();
                myRecs.Filter("001", iID.ToString(), true);
            }
            CControlfield Cf = new CControlfield();
            if (addRecord.Controlfields.get_Controlfield("001", ref Cf))
            {
                Cf.Value = iID.ToString();
            }
            else
            {
                Cf = new CControlfield();
                Cf.Tag = "001";
                Cf.Value = iID.ToString();
                addRecord.Controlfields.Add(Cf);
            }
            myRecs.Add(addRecord);
            myRecs.Save(dataFileName);
            myRecs.Refresh();
            return iID;
        }

        public static void save_DataRecord(String sCategory, CRecord saveRecord)
        {

            saveRecord.Controlfields.Controlfield("005").Value = DateTime.Now.ToLongDateString();

            CRecords myRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            CControlfield Cf = new CControlfield();
            String sID = "0";
            if (saveRecord.Controlfields.get_Controlfield("001", ref Cf))
            {
                if(Cf.Value == "") Cf.Value="0";
                sID =Cf.Value;
            }
            else
            {
                Cf = new CControlfield();
                Cf.Tag = "001";
                Cf.Value = "0";
                saveRecord.Controlfields.Add(Cf);
            }
            myRecs.Filter("001", sID, true);
            if (myRecs.Count > 0)
            {
                CRecord ptOldRec=myRecs.Record(0);
                myRecs.Replace(ref ptOldRec, ref saveRecord);
            }
            else
            {
                myRecs.Add(saveRecord);
            }
            myRecs.Save(dataFileName);
        }

        public static void delete_DataRecord(String sCategory, string sID)
        {
            CRecords myRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            myRecs.Filter("001", sID, true);
            if(myRecs.Count>0)
            {
                myRecs.Remove(0);
            }               
            myRecs.Save(dataFileName);
        }

        public static void moveUp_DataRecord(String sCategory, int iID)
        {
            CRecords myRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            myRecs.Filter("001", iID.ToString(), true);
            if (myRecs.Count > 0)
            {
                CRecord moveRec = myRecs.Record(0);
                moveRec.MoveDown();//reverse
            }
            myRecs.Save(dataFileName);
        }

        public static void moveDown_DataRecord(String sCategory, int iID)
        {
            CRecords myRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            myRecs.Filter("001", iID.ToString(), true);
            if (myRecs.Count > 0)
            {
                CRecord moveRec = myRecs.Record(0);
                moveRec.MoveUp();
            }
            myRecs.Save(dataFileName);
        }

        public static void increase_DownloadCount(String sCategory, int iIndex)
        {
            CRecords myRecs = new CRecords();
            
            String dataFileName = System.Configuration.ConfigurationManager.AppSettings["LegoWebFilesPhysicalPath"].ToString() + "File/Data/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "\\" + sCategory + ".xml";
            if (File.Exists(dataFileName))
            {
                myRecs.load_File(dataFileName);
            }
            else
            {
                myRecs.Save(dataFileName);
            }
            if (iIndex < myRecs.Count)
            {
                CRecord myRec = myRecs.Record(iIndex);
                myRec.Datafields.Datafield("245").Subfields.Subfield("n").Value = (int.Parse("0" + myRec.Datafields.Datafield("245").Subfields.Subfield("n").Value) + 1).ToString();
            }
            myRecs.Save(dataFileName);
        }
    }
}