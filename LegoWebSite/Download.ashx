<%@ WebHandler Language="C#" Class="Download" %>

using System;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public class Download : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)     
    {
        
        /* sample of get thumb nail image
        string sImageFileName = "";
        int iThumbSize = 0;
        decimal dHeight, dWidth, dNewHeight, dNewWidth;

        sImageFileName = context.Request.QueryString["img"];
        iThumbSize = Convert.ToInt32(context.Request.QueryString["sz"]);

        System.Drawing.Image objImage = System.Drawing.Bitmap.FromFile(System.Web.HttpContext.Current.Server.MapPath("Image Path" + sImageFileName));
        if (sImageFileName != null)
        {
            if (iThumbSize == 1)
            {

                dHeight = objImage.Height;
                dWidth = objImage.Width;
                dNewHeight = 120;
                dNewWidth = dWidth * (dNewHeight / dHeight);
                objImage = objImage.GetThumbnailImage((int)dNewWidth, (int)dNewHeight, new System.Drawing.Image.GetThumbnailImageAbort(callback), new IntPtr());
            }
            if (iThumbSize == 2)
            {

                dHeight = objImage.Height;
                dWidth = objImage.Width;
                dNewHeight = 200;
                dNewWidth = dWidth * (dNewHeight / dHeight);
                objImage = objImage.GetThumbnailImage((int)dNewWidth, (int)dNewHeight, new System.Drawing.Image.GetThumbnailImageAbort(callback), new IntPtr());
            }

            MemoryStream objMemoryStream = new MemoryStream();
            objImage.Save(objMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageContent = new byte[objMemoryStream.Length];
            objMemoryStream.Position = 0;
            objMemoryStream.Read(imageContent, 0, (int)objMemoryStream.Length);
            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(imageContent);
        }
         */
        
        try
        {
            if (context.Request.QueryString["id"] == null) return;
            int iFileId = int.Parse(context.Request.QueryString["id"].ToString());
            DataTable fileTable = LegoWebSite.Buslgic.UserFiles.get_LEGOWEB_USER_FILES(iFileId, 0);
            if(fileTable.Rows.Count>0)
            {
                string sFileName = fileTable.Rows[0]["USER_FILE_NAME"].ToString();
                string sPhysicalFilePath = fileTable.Rows[0]["PHYSICAL_PATH"].ToString();
                if (File.Exists(sPhysicalFilePath))
                { 
                    string sFileType=sPhysicalFilePath.Substring(sPhysicalFilePath.LastIndexOf(".")+1).ToLower();
                    switch (sFileType)
                    {
                        case "jpg":
                            context.Response.ContentType = "image/jpg";
                            context.Response.WriteFile(sPhysicalFilePath);
                            context.Response.Flush();
                            context.Response.Close();                            
                            break;
                        case "jpeg":
                            context.Response.ContentType = "image/jpeg";
                            context.Response.WriteFile(sPhysicalFilePath);
                            context.Response.Flush();
                            context.Response.Close();
                            break;
                        case "png":
                            context.Response.ContentType = "image/png";
                            context.Response.WriteFile(sPhysicalFilePath);
                            context.Response.Flush();
                            context.Response.Close();
                            break;
                        case "gif":
                            context.Response.ContentType = "image/gif";
                            context.Response.WriteFile(sPhysicalFilePath);
                            context.Response.Flush();
                            context.Response.Close();
                            break;
                        case "bmp":
                            context.Response.ContentType = "image/bmp";
                            context.Response.WriteFile(sPhysicalFilePath);
                            context.Response.Flush();
                            context.Response.Close();
                            break;
                        default:
                            context.Response.ContentType = "application/octet-stream";
                            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + sFileName);
                            context.Response.TransmitFile(sPhysicalFilePath);
                            // Ensure the file is properly flushed to the user 
                            context.Response.Flush();
                            // Ensure the response is closed 
                            context.Response.Close();
                            break;                                                
                    }
                }    
            }                
            
        }catch(Exception ex)
        {
            //do nothing
        }finally
        {
            context.Response.Clear();
        }         
    }
          
    private bool callback()
    {
        return true;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}