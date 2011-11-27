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
    public class MetaContentObject : CRecord
    {
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
                return String.IsNullOrEmpty(this.Controlfields.Controlfield("002").Value) ? 0 : int.Parse(this.Controlfields.Controlfield("002").Value);
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
                string sValue = value.ToString();
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
    }
}