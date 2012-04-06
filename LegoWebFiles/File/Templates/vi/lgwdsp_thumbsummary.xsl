<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 xmlns:date="http://exslt.org/dates-and-times" extension-element-prefixes="date"> 
  <xsl:output method="html" omit-xml-declaration="yes"/>
  <xsl:template match="record">
    
        <xsl:variable name="ThumbExist" select="count(datafield[@tag=245]/subfield[@code='u'])"> </xsl:variable>
        <xsl:variable name="updatedate" select="controlfield[@tag=005]"></xsl:variable>
			<div class="folder-content-news" style="padding-top:8px">      
                 <xsl:choose>
                   <xsl:when test="$ThumbExist">
                     <div class="ads_left">                                                                     
                       <A>
                         <xsl:attribute name="href">
                           <xsl:value-of select="controlfield[@tag=001]/text()"/>
                         </xsl:attribute>
                         <img class="img-subject fl">
                           <xsl:attribute name="src">
                             <xsl:value-of select="datafield[@tag=245]/subfield[@code='u']/text()"/>
                           </xsl:attribute>
                         </img>
                       </A>                                              
                     </div>
                   </xsl:when>
                 </xsl:choose>
<p>
                 <A class="link-title">
                          <xsl:attribute name="href">
                            <xsl:value-of select="controlfield[@tag=001]/text()"/>
                          </xsl:attribute>                         
                            <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>                         
                 </A>
</p>

	</div>			
  </xsl:template>
  </xsl:stylesheet>
