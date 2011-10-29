// ----------------------------------------------------------------------
// <copyright file="ContentEditorDataHelper.cs" company="HIENDAI SOFTWARE COMPANY">
//     Copyright (C) 2010-2011 HIENDAI SOFTWARE COMPANY. All rights reserved.
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
        public string Alias
        {
            get
            {
                return this.Controlfields.Controlfield("003").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("003", ref Cf))
                {
                    Cf.Type = "TEXT";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "003";
                    Cf.Type = "TEXT";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }
        //using tow characters 4,5 in leader as record status
        public int RecordStatus
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
                string sValue=value.ToString();
                this.set_LeaderValueByPos(sValue, 4, 5);
            }
        }
        public string EntryDate
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
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "004";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }
        public string ModifyDate
        {
            get
            {
                return this.Controlfields.Controlfield("005").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("005", ref Cf))
                {
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "005";
                    Cf.Value = value.ToString();
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
        
        public int AccessLevel
        {
            get
            {
                return (int)Convert.ToDouble("0" + this.Controlfields.Controlfield("009").Value);
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (this.Controlfields.get_Controlfield("009", ref Cf))
                {
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "009";
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                    this.Controlfields.Add(Cf);
                }
            }
        }        
        //try to using first character in leader to indicate isHotList
        public bool isHotContent
        {
            get
            {
                if (this.get_LeaderValueByPos(0, 0) == " " || this.get_LeaderValueByPos(0, 0) == "0" || this.get_LeaderValueByPos(0, 0) == "F") return false;
                if (this.get_LeaderValueByPos(0, 0) == "1" || this.get_LeaderValueByPos(0, 0).ToUpper() == "T") return true;
                return false;
            }
            set
            {
                if (value == true)
                {
                    this.set_LeaderValueByPos("1", 0, 0);
                }
                else
                {
                    this.set_LeaderValueByPos("0", 0, 0);
                }
            }
        }   

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