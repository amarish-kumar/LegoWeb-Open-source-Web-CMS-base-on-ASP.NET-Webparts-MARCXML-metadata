using System;
using System.Data;
using System.Collections.Generic;

using System.Web;
using MarcXmlParserEx;
/// <summary>
/// Summary description for MetaContentEditorDataProvider
/// </summary>
namespace LegoWebSite.DataProvider
{
    public class MetaContentEditorDataProvider
    {
        private DataTable _TableData = null;
        private CRecord _MarcData = null;

        public MetaContentEditorDataProvider()
        {
            create_TableDataTable();
            _MarcData = new CRecord();
        }

        public void create_TableDataTable()
        {
            _TableData = new DataTable();
            _TableData.TableName = "MARC";
            _TableData.Columns.Add(new DataColumn("TAG", typeof(string)));
            _TableData.Columns.Add(new DataColumn("TAG_INDEX", typeof(int)));
            _TableData.Columns.Add(new DataColumn("INDICATOR", typeof(string)));
            _TableData.Columns.Add(new DataColumn("SUBFIELD_ID", typeof(string)));
            _TableData.Columns.Add(new DataColumn("SUBFIELD_CODE", typeof(string)));
            _TableData.Columns.Add(new DataColumn("SUBFIELD_TYPE", typeof(string)));
            _TableData.Columns.Add(new DataColumn("SUBFIELD_LABEL", typeof(string)));
            _TableData.Columns.Add(new DataColumn("SUBFIELD_VALUE", typeof(string)));
        }

        public Int32 MetaContentID
        {
            get
            {                
                return (Int32)Convert.ToDouble("0" + _MarcData.Controlfields.Controlfield("001").Value);
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("001", ref Cf))
                {
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "001";
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }

        public Int16 CategoryID
        {
            get
            {
                return (Int16)Convert.ToDouble("0" + _MarcData.Controlfields.Controlfield("002").Value);
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("002", ref Cf))
                {
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "002";
                    Cf.Value = value.ToString();
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }

        public bool IsPublic
        {
            get
            {
                if (_MarcData.Controlfields.Controlfield("003").Value == "" || _MarcData.Controlfields.Controlfield("003").Value == "0" || _MarcData.Controlfields.Controlfield("003").Value.ToUpper() == "FALSE") return false;
                if (_MarcData.Controlfields.Controlfield("003").Value.ToUpper() == "1" || _MarcData.Controlfields.Controlfield("003").Value.ToUpper() == "TRUE") return true;
                return false;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("003", ref Cf))
                {
                    Cf.Type = "BOOLEAN";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "003";
                    Cf.Type = "BOOLEAN";
                    Cf.Value = value.ToString();
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }
        public string EntryDate
        {
            get
            {
                return _MarcData.Controlfields.Controlfield("004").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("004", ref Cf))
                {
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "004";
                    Cf.Value = value.ToString();
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }
        public string ModifyDate
        {
            get
            {
                return _MarcData.Controlfields.Controlfield("005").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("005", ref Cf))
                {
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "005";
                    Cf.Value = value.ToString();
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }
        public string Creator
        {
            get
            {
                return _MarcData.Controlfields.Controlfield("006").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("006", ref Cf))
                {
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "006";
                    Cf.Value = value.ToString();
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }
        public string Modifier
        {
            get
            {
                return _MarcData.Controlfields.Controlfield("007").Value;
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("007", ref Cf))
                {
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "007";
                    Cf.Value = value.ToString();
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }

        public String LangCode
        {
            get
            {
                return _MarcData.Controlfields.Controlfield("008").get_ValueByPos(35, 36);//use only 2 language code character
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("008", ref Cf))
                {
                    Cf.set_ValueByPos(value, 35, 36);
                }
                else
                {
                    Cf.Tag = "008";
                    Cf.set_ValueByPos(value, 35, 36);
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }
        public int AccessLevel
        {
            get
            {
                return (int)Convert.ToDouble("0" + _MarcData.Controlfields.Controlfield("009").Value);
            }
            set
            {
                CControlfield Cf = new CControlfield();
                if (_MarcData.Controlfields.get_Controlfield("009", ref Cf))
                {
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                }
                else
                {
                    Cf.Tag = "009";
                    Cf.Type = "NUMBER";
                    Cf.Value = value.ToString();
                    _MarcData.Controlfields.Add(Cf);
                }
            }
        }
        public CRecord MarcData
        {
            set
            {
                _MarcData = value;
                bind_MarcToTable();
            }
            get
            {
                return _MarcData;
            }
        }

        public DataTable TableData
        {
            set
            {
                _TableData = value;
            }
            get
            {
                return _TableData;
            }
        }

        public void bind_MarcToTable()
        {
            if (MarcData.Datafields.Count == 0) return;
            create_TableDataTable();
            CDatafield Df = new CDatafield();
            DataRow nRow;
            //add datafields
            for (int i = 0; i < _MarcData.Datafields.Count; i++)
            {
                Df = _MarcData.Datafields.Datafield(i);
                for (int j = 0; j < Df.Subfields.Count; j++)
                {
                    nRow = _TableData.NewRow();
                    nRow["TAG"] = Df.Tag;
                    nRow["TAG_INDEX"] = i + 1;
                    nRow["INDICATOR"] = Df.Ind1 + Df.Ind2;
                    nRow["SUBFIELD_ID"] = Df.Subfields.Subfield(j).ID;
                    nRow["SUBFIELD_CODE"] = Df.Subfields.Subfield(j).Code;
                    nRow["SUBFIELD_TYPE"] = Df.Subfields.Subfield(j).Type;
                    nRow["SUBFIELD_VALUE"] = Df.Subfields.Subfield(j).Value;
                    _TableData.Rows.Add(nRow);
                }
            }
        }

        public void bind_TableToMarc()
        {
            if (_TableData.Rows.Count == 0) return;
            //remove all old datafields
            while (_MarcData.Datafields.Count > 0)
            {
                _MarcData.Datafields.Remove(0);
                _MarcData.Datafields.Refresh();
            }

            for (int i = 0; i < _TableData.Rows.Count; i++)
            {
                CDatafield Df = new CDatafield();
                int tagIndex = int.Parse(_TableData.Rows[i]["TAG_INDEX"].ToString());
                Df.Tag = _TableData.Rows[i]["TAG"].ToString();
                while ((i < _TableData.Rows.Count) && (tagIndex == int.Parse(_TableData.Rows[i]["TAG_INDEX"].ToString())))
                {
                    CSubfield Sf = new CSubfield();
                    Sf.ID = _TableData.Rows[i]["SUBFIELD_ID"].ToString();
                    Sf.Code = _TableData.Rows[i]["SUBFIELD_CODE"].ToString();
                    Sf.Type = _TableData.Rows[i]["SUBFIELD_TYPE"].ToString();
                    Sf.Value = _TableData.Rows[i]["SUBFIELD_VALUE"].ToString();
                    Df.Subfields.Add(Sf);
                    i++;
                }
                i--;//back to for step
                _MarcData.Datafields.Add(Df);
            }
        }

        public CRecord MarcLabel
        {
            set
            {
                CRecord labelRec = value;
                for (int i = 0; i < _TableData.Rows.Count; i++)
                {
                    if (int.Parse(_TableData.Rows[i]["TAG"].ToString()) < 10)
                    {
                        _TableData.Rows[i]["SUBFIELD_LABEL"] = labelRec.Controlfields.Controlfield(_TableData.Rows[i]["TAG"].ToString()).Value;
                    }
                    else
                    {
                        _TableData.Rows[i]["SUBFIELD_LABEL"] = labelRec.Datafields.Datafield(_TableData.Rows[i]["TAG"].ToString()).Subfields.Subfield(_TableData.Rows[i]["SUBFIELD_CODE"].ToString()).Value;
                    }
                }
            }
        }

        public void load_WorkformData(String sWFDataFileName)
        {
            CRecord dataRec = new CRecord();
            dataRec.load_File(sWFDataFileName);
            this.MarcData = dataRec;
        }

        public void load_WorkformLabel(String sWFLabelFileName)
        {
            CRecord labelRec = new CRecord();
            labelRec.load_File(sWFLabelFileName);
            this.MarcLabel = labelRec;
        }
    }
}