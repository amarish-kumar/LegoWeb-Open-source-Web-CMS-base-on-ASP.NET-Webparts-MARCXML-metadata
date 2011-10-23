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
        public static DataTable get_PollData(int iMeta_Content_Id, out string sQuestion,out int iTotalVote, string sLangCode)
        {
            iTotalVote = 0;
            sQuestion = null;
            DataTable pollData = new DataTable();
            DataColumn IDcol= new DataColumn("ID");
            IDcol.DataType = System.Type.GetType("System.Int32");
            pollData.Columns.Add(IDcol);

            DataColumn voteCol = new DataColumn("VoteCount");
            voteCol.DataType = System.Type.GetType("System.Int32");
            pollData.Columns.Add(voteCol);

            DataColumn AnswerCol = new DataColumn("Answer");
            AnswerCol.DataType = System.Type.GetType("System.String");
            pollData.Columns.Add(AnswerCol);

            DataColumn orderCol = new DataColumn("OrderNumber");
            voteCol.DataType = System.Type.GetType("System.Int32");
            pollData.Columns.Add(orderCol);
           
            CRecord pollRecord = new CRecord();
            CDatafield Df = new CDatafield();
            CSubfield Sf = new CSubfield();

            pollRecord.load_Xml(LegoWebSite.Buslgic.MetaContents.get_META_CONTENT_MARCXML(iMeta_Content_Id,false));

            pollRecord.Sort();
            //get Question First
            if (sLangCode.ToLower() == "vi")
            {
                sQuestion = pollRecord.Datafields.Datafield("245").Subfields.Subfield("a").Value;
               
            }
            else
            {
                sQuestion = pollRecord.Datafields.Datafield("245").Subfields.Subfield("b").Value;
            }
            
            CDatafields AnswerDfs = pollRecord.Datafields;
          
            AnswerDfs.Filter("650");            

            for (int i = 0; i < AnswerDfs.Count; i++)
            {
                string sAn = "";
                int iID = 0;
                int iVoteCount = 0;
                int iOrderNumber = 0;
                Df = AnswerDfs.Datafield(i);

                if (Df.Subfields.get_Subfield("0", ref Sf))
                {
                    iOrderNumber =String.IsNullOrEmpty(Sf.Value)?0:int.Parse(Sf.Value);
                }
                if (sLangCode.ToLower() == "vi")
                {
                    if (Df.Subfields.get_Subfield("a", ref Sf))
                    {
                        sAn = Sf.Value;
                    }
                    else
                    {
                        sAn = "Không có dữ liệu câu hỏi";
                    }
                }
                else
                {
                    if (Df.Subfields.get_Subfield("b", ref Sf))
                    {
                        sAn = Sf.Value;
                    }
                    else
                    {
                        sAn = "No question data";
                    }
                }
                if (Df.Subfields.get_Subfield("n", ref Sf))
                {
                    iTotalVote += int.Parse(Sf.Value);
                    iVoteCount = int.Parse(Sf.Value);
                    iID = int.Parse(Sf.ID);                
                }
                                
                DataRow row = pollData.NewRow();
                row["ID"] = iID;
                row["Answer"] = sAn;
                row["VoteCount"] = iVoteCount;
                row["OrderNumber"] = iOrderNumber;
                pollData.Rows.Add(row);
            }
            pollData.DefaultView.Sort = " OrderNumber ASC";
            return pollData;
        }


        public static void increase_VoteCount(int iAnswerId)
        {
            DataTable AnswerInfo = LegoWebSite.Buslgic.MetaContentTexts.get_META_CONTENT_TEXT(iAnswerId);
            if (AnswerInfo.Rows.Count > 0)
            {
                DataTable CountInfo = LegoWebSite.Buslgic.MetaContentNumbers.get_META_CONTENT_NUMBER(int.Parse(AnswerInfo.Rows[0]["META_CONTENT_ID"].ToString()), int.Parse(AnswerInfo.Rows[0]["TAG"].ToString()), int.Parse(AnswerInfo.Rows[0]["TAG_INDEX"].ToString()), "n");
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