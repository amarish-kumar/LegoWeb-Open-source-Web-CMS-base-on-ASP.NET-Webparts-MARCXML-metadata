using System;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Text;

    public class CGoogleResults
    {
        protected XmlDocument ptCurrentDoc;
        protected XmlNode ptCurrentRootNode;
        protected XmlNodeList ptCurrentResultList;		

        public CGoogleResults()
        {
            ptCurrentDoc = new XmlDocument();
            ptCurrentDoc.LoadXml("<results></results>");
            XmlElement xElem = ptCurrentDoc.DocumentElement;
            ptCurrentResultList = xElem.SelectNodes("./*[local-name() ='result']");
        }
       public void ReConstruct()
        {
            ptCurrentDoc.LoadXml("<results></results>");
            ptCurrentRootNode = ptCurrentDoc.SelectSingleNode("./*[local-name() ='results']");
            XmlElement xElem = ptCurrentDoc.DocumentElement;
            ptCurrentResultList = xElem.SelectNodes("./*[local-name() ='result']");			

        }
        public bool load_Xml(String metsStream) //only available if pXMLDOM is NULL
        {
            ptCurrentDoc.LoadXml(metsStream);
            ptCurrentRootNode = ptCurrentDoc.SelectSingleNode("./*[local-name() ='results']");
            XmlElement xElem = ptCurrentDoc.DocumentElement;
            ptCurrentResultList = xElem.SelectNodes("./*[local-name() ='result']");

            if (ptCurrentRootNode != null)
                return true;
            else
                return false;
        }

        public bool load_File(String XmlFileName)
        {
            XmlTextReader reader = new XmlTextReader(XmlFileName);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            ptCurrentDoc.Load(reader);
            reader.Close();
            ptCurrentRootNode = ptCurrentDoc.SelectSingleNode("./*[local-name() ='results']");
            XmlElement xElem = ptCurrentDoc.DocumentElement;
            ptCurrentResultList = xElem.SelectNodes("./*[local-name() ='result']");


            if (ptCurrentRootNode != null)
                return true;
            else
                return false;
        }

        public bool load_Uri(String XmlUri)
        {
            ptCurrentDoc.Load(XmlUri);
            ptCurrentRootNode = ptCurrentDoc.SelectSingleNode("./*[local-name() ='results']");
            XmlElement xElem = ptCurrentDoc.DocumentElement;
            ptCurrentResultList = xElem.SelectNodes("./*[local-name() ='result']");

            if (ptCurrentRootNode != null)
                return true;
            else
                return false;
        }

        public bool load_RootNode(ref XmlNode ptmetsRootNode)
        {
            ptCurrentRootNode = ptmetsRootNode;
            if (ptCurrentRootNode.Name == "results")
            {
                ptCurrentDoc = ptCurrentRootNode.OwnerDocument;
                return true;
            }
            else
            {
                return false;
            }
        }
        public String OuterXml
        {
            get
            {
                return ptCurrentRootNode.OuterXml;
            }
        }
        /*
         * This attribute uniquely identifies the element with which the root element is associated within
         *  the mets document, and which would allow the element to be referenced unambiguously from
         *  another element or document via an IDREF or an XPTR. For more information on using ID
         *  attributes for internal and external linking see Chapter 4. 
         */
        public Int16 ResultCount
        {
            get
            {
                XmlNode mxNode = (XmlNode)(ptCurrentRootNode.Attributes.GetNamedItem("count"));
                if (mxNode != null)
                    return Convert.ToInt16(((XmlAttribute)(mxNode)).Value);
                else
                    return 0;
            }
        }

        public void Save(String XmlFileName)
        {
            ptCurrentDoc.PreserveWhitespace = true;
            //ptCurrentDoc.Save(XmlWriter.Create(XmlFileName));
            XmlTextWriter wrtr = new XmlTextWriter(XmlFileName, null);
            ptCurrentDoc.Save(wrtr);
            wrtr.Flush();
            wrtr.Close();
        }

        //public CdmdSecs dmdSecs
        //{
        //    get
        //    {
        //        return new CdmdSecs(ptCurrentRootNode);
        //    }
        //}
        public String XsltFile_Transform(String XslFileName)
		{			
			XslCompiledTransform xslt = new XslCompiledTransform(); 					
			xslt.Load(XslFileName);
			//// Transform the file and output an HTML string.			
			XmlNodeReader xmlNodeRdr = new XmlNodeReader(ptCurrentDoc); 
			StringBuilder Sb=new StringBuilder();			
			xslt.Transform(xmlNodeRdr,null, XmlWriter.Create(Sb), null);
			return Sb.ToString();	
		}

        public int Count
        {
            get
            {
                return ptCurrentResultList.Count;
            }
        }

        public CGoogleResult GoogleResult(int ResultIndex)
        {
            CGoogleResult ptResult;
            ptResult = new CGoogleResult();
            XmlNode myNode = ptCurrentResultList.Item(ResultIndex);
            ptResult.load_RootNode(ref myNode);
            return ptResult;
        }

    }


    public class CGoogleResult
    {

        protected XmlDocument ptCurrentDoc;
        protected XmlNode ptCurrentRootNode;


        public CGoogleResult()
        {
            ptCurrentDoc = new XmlDocument();
            ptCurrentDoc.LoadXml("<result code='a'></result>");
            ptCurrentRootNode = ptCurrentDoc.SelectSingleNode("./*[local-name() ='result']");
        }
        public void ReConstruct()
        {
            ptCurrentDoc.LoadXml("<result code='a'></result>");
            ptCurrentRootNode = ptCurrentDoc.SelectSingleNode("./*[local-name() ='result']");
        }
        public bool load_Xml(String GoogleResultStream) //only available if pXMLDOM is NULL
        {
            ptCurrentDoc.LoadXml(GoogleResultStream);
            ptCurrentRootNode = ptCurrentDoc.SelectSingleNode("./*[local-name() ='result']");
            if (ptCurrentRootNode != null)
                return true;
            else
                return false;
        }
        public bool load_RootNode(ref XmlNode ptGoogleResultRootNode)
        {
            ptCurrentRootNode = ptGoogleResultRootNode;
            if (ptCurrentRootNode.Name == "result")
            {
                ptCurrentDoc = ptCurrentRootNode.OwnerDocument;
                return true;
            }
            else
            {
                return false;
            }
        }

        public String OuterXml
        {
            get
            {
                return ptCurrentRootNode.OuterXml;
            }
        }
        public XmlNode RootNode
        {
            set
            {
                ptCurrentRootNode = value;
                ptCurrentDoc = ptCurrentRootNode.OwnerDocument;
            }
            get
            {
                return ptCurrentRootNode;
            }

        }
        public String category
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptcategoryNode;
                ptcategoryNode = xElem.SelectSingleNode("./*[local-name() ='category']");
                if (ptcategoryNode != null)
                {
                    if (ptcategoryNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = ptcategoryNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        ptcategoryNode.AppendChild(ptcategoryNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    ptcategoryNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("category"));
                    ptcategoryNode.AppendChild(ptcategoryNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptcategoryNode;
                ptcategoryNode = xElem.SelectSingleNode("./*[local-name() ='category']");
                if (ptcategoryNode != null)
                {
                    return ptcategoryNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }
        public String doc_id
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptdoc_idNode;
                ptdoc_idNode = xElem.SelectSingleNode("./*[local-name() ='doc_id']");
                if (ptdoc_idNode != null)
                {
                    if (ptdoc_idNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = ptdoc_idNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        ptdoc_idNode.AppendChild(ptdoc_idNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    ptdoc_idNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("doc_id"));
                    ptdoc_idNode.AppendChild(ptdoc_idNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptdoc_idNode;
                ptdoc_idNode = xElem.SelectSingleNode("./*[local-name() ='doc_id']");
                if (ptdoc_idNode != null)
                {
                    return ptdoc_idNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }
        public String event_id
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptevent_idNode;
                ptevent_idNode = xElem.SelectSingleNode("./*[local-name() ='event_id']");
                if (ptevent_idNode != null)
                {
                    if (ptevent_idNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = ptevent_idNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        ptevent_idNode.AppendChild(ptevent_idNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    ptevent_idNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("event_id"));
                    ptevent_idNode.AppendChild(ptevent_idNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptevent_idNode;
                ptevent_idNode = xElem.SelectSingleNode("./*[local-name() ='event_id']");
                if (ptevent_idNode != null)
                {
                    return ptevent_idNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }
        public String title
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode pttitleNode;
                pttitleNode = xElem.SelectSingleNode("./*[local-name() ='title']");
                if (pttitleNode != null)
                {
                    if (pttitleNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = pttitleNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        pttitleNode.AppendChild(pttitleNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    pttitleNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("title"));
                    pttitleNode.AppendChild(pttitleNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode pttitleNode;
                pttitleNode = xElem.SelectSingleNode("./*[local-name() ='title']");
                if (pttitleNode != null)
                {
                    return pttitleNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }
        public String url
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode pturlNode;
                pturlNode = xElem.SelectSingleNode("./*[local-name() ='url']");
                if (pturlNode != null)
                {
                    if (pturlNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = pturlNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        pturlNode.AppendChild(pturlNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    pturlNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("url"));
                    pturlNode.AppendChild(pturlNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode pturlNode;
                pturlNode = xElem.SelectSingleNode("./*[local-name() ='url']");
                if (pturlNode != null)
                {
                    return pturlNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }
        public String flags
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptflagsNode;
                ptflagsNode = xElem.SelectSingleNode("./*[local-name() ='flags']");
                if (ptflagsNode != null)
                {
                    if (ptflagsNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = ptflagsNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        ptflagsNode.AppendChild(ptflagsNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    ptflagsNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("flags"));
                    ptflagsNode.AppendChild(ptflagsNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptflagsNode;
                ptflagsNode = xElem.SelectSingleNode("./*[local-name() ='flags']");
                if (ptflagsNode != null)
                {
                    return ptflagsNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }
        public String time
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode pttimeNode;
                pttimeNode = xElem.SelectSingleNode("./*[local-name() ='time']");
                if (pttimeNode != null)
                {
                    if (pttimeNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = pttimeNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        pttimeNode.AppendChild(pttimeNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    pttimeNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("time"));
                    pttimeNode.AppendChild(pttimeNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode pttimeNode;
                pttimeNode = xElem.SelectSingleNode("./*[local-name() ='time']");
                if (pttimeNode != null)
                {
                    return pttimeNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }
        public String icon
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode pticonNode;
                pticonNode = xElem.SelectSingleNode("./*[local-name() ='icon']");
                if (pticonNode != null)
                {
                    if (pticonNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = pticonNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        pticonNode.AppendChild(pticonNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    pticonNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("icon"));
                    pticonNode.AppendChild(pticonNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode pticonNode;
                pticonNode = xElem.SelectSingleNode("./*[local-name() ='icon']");
                if (pticonNode != null)
                {
                    return pticonNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }
        public String cache_url
        {
            set
            {
                if (value.Length == 0) return;
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptcache_urlNode;
                ptcache_urlNode = xElem.SelectSingleNode("./*[local-name() ='cache_url']");
                if (ptcache_urlNode != null)
                {
                    if (ptcache_urlNode.HasChildNodes)
                    {
                        XmlNode ptEditNode = ptcache_urlNode.FirstChild;
                        ptEditNode.Value = value;
                    }
                    else
                    {
                        ptcache_urlNode.AppendChild(ptcache_urlNode.OwnerDocument.CreateTextNode(value));
                    }
                }
                else
                {
                    ptcache_urlNode = ptCurrentRootNode.AppendChild(ptCurrentRootNode.OwnerDocument.CreateElement("cache_url"));
                    ptcache_urlNode.AppendChild(ptcache_urlNode.OwnerDocument.CreateTextNode(value));
                }
            }
            get
            {
                XmlElement xElem;//Data field node			
                xElem = (XmlElement)(ptCurrentRootNode);
                XmlNode ptcache_urlNode;
                ptcache_urlNode = xElem.SelectSingleNode("./*[local-name() ='cache_url']");
                if (ptcache_urlNode != null)
                {
                    return ptcache_urlNode.InnerText;
                }
                else
                {
                    return "";
                }
            }
        }

    };
