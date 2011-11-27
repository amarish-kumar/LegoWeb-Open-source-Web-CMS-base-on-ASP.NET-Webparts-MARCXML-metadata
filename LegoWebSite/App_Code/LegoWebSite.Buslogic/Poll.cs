using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MarcXmlParserEx;


/// <summary>
/// Summary description for Poll
/// </summary>
/// 

namespace LegoWebSite.Buslgic
{
    public class Polls
    {
        public static DataTable get_PollData(int iPollContentId, out string sQuestion,out int iTotalVoteCount)
        {
            iTotalVoteCount = 0;
            sQuestion = null;
            DataTable pollData = new DataTable();
            DataColumn IDcol= new DataColumn("ID");
            IDcol.DataType = System.Type.GetType("System.Int32");
            pollData.Columns.Add(IDcol);

            DataColumn voteCountCol = new DataColumn("VoteCount");
            voteCountCol.DataType = System.Type.GetType("System.Int32");
            pollData.Columns.Add(voteCountCol);

            DataColumn ChoiceCol = new DataColumn("Choice");
            ChoiceCol.DataType = System.Type.GetType("System.String");
            pollData.Columns.Add(ChoiceCol);

            DataColumn orderCol = new DataColumn("OrderNumber");
            orderCol.DataType = System.Type.GetType("System.Int32");
            pollData.Columns.Add(orderCol);
           
            CRecord pollRecord = new CRecord();
            CDatafield Df = new CDatafield();
            CSubfield Sf = new CSubfield();

            pollRecord.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(iPollContentId, 0));

            pollRecord.Sort();
            //get Question First
            sQuestion = pollRecord.Datafields.Datafield("245").Subfields.Subfield("a").Value;
            
            CDatafields ChoiceDfs = pollRecord.Datafields;
          
            ChoiceDfs.Filter("650");            

            for (int i = 0; i < ChoiceDfs.Count; i++)
            {
                string sChoice = "";
                int iID = 0;
                int iVoteCount = 0;
                int iOrderNumber = 0;
                Df = ChoiceDfs.Datafield(i);

                if (Df.Subfields.get_Subfield("0", ref Sf))
                {
                    iOrderNumber =String.IsNullOrEmpty(Sf.Value)?0:int.Parse(Sf.Value);
                }

                if (Df.Subfields.get_Subfield("a", ref Sf))
                {
                    sChoice = Sf.Value;
                    iID = int.Parse(Sf.ID);        
                }
                else
                {
                    sChoice = "No choice info";
                }

                if (Df.Subfields.get_Subfield("n", ref Sf))
                {
                    iTotalVoteCount += int.Parse(Sf.Value);
                    iVoteCount = int.Parse(Sf.Value);                            
                }
                                
                DataRow row = pollData.NewRow();
                row["ID"] = iID;
                row["Choice"] = sChoice;
                row["VoteCount"] = iVoteCount;
                row["OrderNumber"] = iOrderNumber;
                pollData.Rows.Add(row);
            }
            pollData.DefaultView.Sort = " OrderNumber ASC";
            return pollData;
        }


        public static void increase_VoteCount(int iChoiceId)
        {
            //not work now - something is wrong!
            DataTable ChoiceInfo = LegoWebSite.Buslgic.MetaContentTexts.get_META_CONTENT_TEXT(iChoiceId);
            if (ChoiceInfo.Rows.Count > 0)
            {
                DataTable CountInfo = LegoWebSite.Buslgic.MetaContentNumbers.get_META_CONTENT_NUMBER(int.Parse(ChoiceInfo.Rows[0]["META_CONTENT_ID"].ToString()), int.Parse(ChoiceInfo.Rows[0]["TAG"].ToString()), int.Parse(ChoiceInfo.Rows[0]["TAG_INDEX"].ToString()), "n");
                if (CountInfo.Rows.Count > 0)
                {
                    Decimal count = CountInfo.Rows[0]["SUBFIELD_VALUE"] != DBNull.Value ? (Decimal)CountInfo.Rows[0]["SUBFIELD_VALUE"] : 0;
                    int iCount = Convert.ToInt16(count) + 1;
                    //call update function
                    LegoWebSite.Buslgic.MetaContentNumbers.update_META_CONTENT_NUMBER_VALUE(int.Parse(CountInfo.Rows[0]["META_CONTENT_NUMBER_ID"].ToString()), iCount);
                }
            }            
        
        }
       
    }
}