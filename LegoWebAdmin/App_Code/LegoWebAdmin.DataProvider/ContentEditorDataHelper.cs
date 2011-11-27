// ----------------------------------------------------------------------
// <copyright file="ContentEditorDataHelper.cs" package="LEGOWEB">
//     Copyright (C) 2011 LEGOWEB.ORG. All rights reserved.
//     www.legoweb.org
//     License: GNU/GPL
//     LEGOWEB IS FREE SOFTWARE
// </copyright>
// ------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;

using System.Web;
using MarcXmlParserEx;
/// <summary>
/// Summary description for MetaContentEditorDataProvider
/// </summary>
namespace LegoWebAdmin.DataProvider
{
    public class ContentEditorDataHelper:CRecord
    {
        public DataTable create_MarcDataTable()
        {
            DataTable marcTable=new DataTable();
            marcTable.TableName = "MARC";
            marcTable.Columns.Add(new DataColumn("TAG", typeof(string)));
            marcTable.Columns.Add(new DataColumn("TAG_INDEX", typeof(int)));
            marcTable.Columns.Add(new DataColumn("INDICATOR", typeof(string)));
            marcTable.Columns.Add(new DataColumn("SUBFIELD_ID", typeof(int)));
            marcTable.Columns.Add(new DataColumn("SUBFIELD_CODE", typeof(string)));
            marcTable.Columns.Add(new DataColumn("SUBFIELD_TYPE", typeof(string)));
            marcTable.Columns.Add(new DataColumn("SUBFIELD_LABEL", typeof(string)));
            marcTable.Columns.Add(new DataColumn("SUBFIELD_VALUE", typeof(string)));
            return marcTable;
        }
        #region using of leader        
        /// <summary>
        /// use first character position in leader to store ImportantLevel: 0,1,2
        /// </summary>
        public int ImportantLevel //
        {
            get
            {
                int iLevel = 0;
                string sLevel = this.get_LeaderValueByPos(0, 0);
                if (!int.TryParse(sLevel, out iLevel))
                {
                    iLevel = 0;
                    this.set_LeaderValueByPos("0", 0, 0);
                }
                return iLevel;
            }
            set
            {
                this.set_LeaderValueByPos(value.ToString(), 0, 0);
            }
        }
        /// <summary>
        /// use second character position in leader to store AccessLevel: 0,1,2
        /// </summary>
    
        public int AccessLevel
        {
            get
            {
                int iLevel = 0;
                string sLevel = this.get_LeaderValueByPos(1, 1);
                if (!int.TryParse(sLevel, out iLevel))
                {
                    iLevel = 0;
                    this.set_LeaderValueByPos("0", 1, 1);
                }
                return iLevel;
            }
            set
            {
                this.set_LeaderValueByPos(value.ToString(), 1, 1);
            }
        }  
        /// <summary>
        ///use fouth and fith character positions in leader to store RecordStatus: -1,0,1 
        /// </summary>
        public int RecordStatus // 4,5 positions in leader
        {
            get
            {
                int iret = 1;
                string sStatus = this.get_LeaderValueByPos(4, 5);
                if (!String.IsNullOrEmpty(sStatus))
                {
                    int.TryParse(sStatus, out iret);
                }
                return iret;
            }
            set
            {
                string sValue = value.ToString();
                this.set_LeaderValueByPos(sValue, 4, 5);
            }
        }
        #endregion using of leader

        #region using of controlfields
        /// <summary>
        /// use controlfield 001 to store meta content ID
        /// </summary>
        public Int32 MetaContentID
        {
            get
            {                
                return (Int32)Convert.ToDouble("0" + this.Controlfields.Controlfield("001").Value);
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("001", ref Cf))
                {
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "001";
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }
        /// <summary>
        /// use controlfield 001 to store category id
        /// </summary>
        public int CategoryID
        {
            get
            {
                return String.IsNullOrEmpty(this.Controlfields.Controlfield("002").Value)?0:int.Parse(this.Controlfields.Controlfield("002").Value);
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("002", ref Cf))
                {
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "002";
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }
        /// <summary>
        /// use controlfield 004 to store local code ex: product code, customer code...
        /// if 004 has value need to check duplicate with other records in the same category before save
        /// </summary>
        public string LocalCode
        {
            get
            {
                return this.Controlfields.Controlfield("004").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("004", ref Cf))
                {
                    Cf.Type = "TEXT";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "004";
                    Cf.Type = "TEXT";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }
        
        /// <summary>
        /// using 005 to store EntryDate and UpdateDate in format of: 
        /// yyyy-MMM-dd hh:mm:ss yyyy-MMM-dd hh:mm:ss
        /// 01234567890123456789012345678901234567890
        /// </summary>

        public string EntryDate
        {
            get
            {
                return this.Controlfields.Controlfield("005").get_ValueByPos(0,19);
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("005", ref Cf))
                {
                    Cf.set_ValueByPos(value.ToString(),0,19);
                }
                else
                {
                    Cf.Tag = "005";
                    Cf.set_ValueByPos(value.ToString(),0,19);
                    this.Controlfields.Add(Cf);
                }
            }
        }

        public string ModifyDate
        {
            get
            {
                return this.Controlfields.Controlfield("005").get_ValueByPos(21, 40);
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("005", ref Cf))
                {
                    Cf.set_ValueByPos(value.ToString(), 21, 40);
                }
                else
                {
                    Cf.Tag = "005";
                    Cf.set_ValueByPos(value.ToString(), 21, 40);
                    this.Controlfields.Add(Cf);
                }
            }
        }
        public string Creator
        {
            get
            {
                return this.Controlfields.Controlfield("006").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("006", ref Cf))
                {
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "006";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }
        public string Modifier
        {
            get
            {
                return this.Controlfields.Controlfield("007").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("007", ref Cf))
                {
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "007";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }
        public String LangCode
        {
            get
            {
                return this.Controlfields.Controlfield("008").get_ValueByPos(35, 36);//use only 2 language code character
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("008", ref Cf))
                {
                    Cf.set_ValueByPos(value, 35, 36);
                }
                else
                {
                    Cf.Tag = "008";
                    Cf.set_ValueByPos(value, 35, 36);
                    this.Controlfields.Add(Cf);
                }
            }
        }
        public string Alias
        {
            get
            {
                return this.Controlfields.Controlfield("009").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("009", ref Cf))
                {
                    Cf.Type = "TEXT";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "009";
                    Cf.Type = "TEXT";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }
        #endregion using of controlfields


        public DataTable get_MarcDatafieldTable()
        {             
            DataTable marcTable = create_MarcDataTable();
            CDatafield Df = new CDatafield();
            DataRow nRow;
            //add datafields
            for (int i = 0; i < this.Datafields.Count; i++)
            {
                Df = this.Datafields.Datafield(i);
                for (int j = 0; j < Df.Subfields.Count; j++)
                {
                    nRow = marcTable.NewRow();
                    nRow["TAG"] = Df.Tag;
                    nRow["TAG_INDEX"] = i + 1;
                    nRow["INDICATOR"] = Df.Ind1 + Df.Ind2;
                    nRow["SUBFIELD_ID"] = String.IsNullOrEmpty(Df.Subfields.Subfield(j).ID) ? 0 : int.Parse(Df.Subfields.Subfield(j).ID);
                    nRow["SUBFIELD_CODE"] = Df.Subfields.Subfield(j).Code;
                    nRow["SUBFIELD_TYPE"] = Df.Subfields.Subfield(j).Type;
                    nRow["SUBFIELD_VALUE"] = Df.Subfields.Subfield(j).Value;
                    marcTable.Rows.Add(nRow);
                }
            }
            return marcTable;
        }

        public void bind_TableDataToMarc(ref DataTable marcDataTable)
        {
            if (marcDataTable.Rows.Count == 0) return;
            //remove all old datafields
            while (this.Datafields.Count > 0)
            {
                this.Datafields.Remove(0);
                this.Datafields.Refresh();
            }

            for (int i = 0; i < marcDataTable.Rows.Count; i++)
            {
                CDatafield Df = new CDatafield();
                int tagIndex = int.Parse(marcDataTable.Rows[i]["TAG_INDEX"].ToString());
                Df.Tag = marcDataTable.Rows[i]["TAG"].ToString();
                while ((i < marcDataTable.Rows.Count) && (tagIndex == int.Parse(marcDataTable.Rows[i]["TAG_INDEX"].ToString())))
                {
                    CSubfield Sf = new CSubfield();
                    Sf.ID = marcDataTable.Rows[i]["SUBFIELD_ID"].ToString();
                    Sf.Code = marcDataTable.Rows[i]["SUBFIELD_CODE"].ToString();
                    Sf.Type = marcDataTable.Rows[i]["SUBFIELD_TYPE"].ToString();
                    Sf.Value = marcDataTable.Rows[i]["SUBFIELD_VALUE"].ToString();
                    Df.Subfields.Add(Sf);
                    i++;
                }
                i--;//back to for step
                this.Datafields.Add(Df);
            }
        }

        public void set_DataTableLabel(ref DataTable marcTable, CRecord labelRec)
        {
            for (int i = 0; i < marcTable.Rows.Count; i++)
            {
                if (int.Parse(marcTable.Rows[i]["TAG"].ToString()) < 10)
                {
                    marcTable.Rows[i]["SUBFIELD_LABEL"] = labelRec.Controlfields.Controlfield(marcTable.Rows[i]["TAG"].ToString()).Value;
                }
                else
                {
                    marcTable.Rows[i]["SUBFIELD_LABEL"] = labelRec.Datafields.Datafield(marcTable.Rows[i]["TAG"].ToString()).Subfields.Subfield(marcTable.Rows[i]["SUBFIELD_CODE"].ToString()).Value;
                }
            }
        }
    }
}