<xsl:stylesheet version="1.0" xmlns:marc="http://www.loc.gov/MARC21/slim" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />
  <xsl:template match="/">
    <xsl:apply-templates></xsl:apply-templates>
  </xsl:template>
  <xsl:template match="record">
    <xsl:variable name="FlashTitle" select="datafield[@tag=245]/subfield[@code='a']/text()">
    </xsl:variable>
    <xsl:variable name="FlashWidth" select="datafield[@tag=300]/subfield[@code='w']/text()">
    </xsl:variable>
    <xsl:variable name="FlashHeight" select="datafield[@tag=300]/subfield[@code='h']/text()">
    </xsl:variable>
    <xsl:variable name="FlashSource" select="datafield[@tag=856]/subfield[@code='u']/text()">
    </xsl:variable>           
 <center>
   <xsl:choose>
     <xsl:when test="contains($FlashSource,'.jpg')">
       <img>
         <xsl:attribute name="src">
           <xsl:value-of select="$FlashSource" />
         </xsl:attribute>
       </img>
     </xsl:when>
     <xsl:when test="contains($FlashSource,'.png')">
       <img>
         <xsl:attribute name="src">
           <xsl:value-of select="$FlashSource" />
         </xsl:attribute>
       </img>
     </xsl:when>
     <xsl:when test="contains($FlashSource,'.gif')">
       <img>
         <xsl:attribute name="src">
           <xsl:value-of select="$FlashSource" />
         </xsl:attribute>
       </img>
     </xsl:when>

     <xsl:when test="contains($FlashSource,'.swf')">
       <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" id="flashControl" align="middle" border="0">
         <xsl:attribute name="width">
           <xsl:value-of select="$FlashWidth" />
         </xsl:attribute>
         <xsl:attribute name="height">
           <xsl:value-of select="$FlashHeight" />
         </xsl:attribute>
         <param name="movie">
           <xsl:attribute name="value">
             <xsl:value-of select="$FlashSource" />
           </xsl:attribute>
         </param>
         <param name="quality" value="high" />
         <param name="wmode" value="transparent"/>
         <param name="bgcolor" value="#ffffff" />
         <embed quality="high" bgcolor="#ffffff" wmode="transparent" name="flashControl" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer">
           <xsl:attribute name="width">
             <xsl:value-of select="$FlashWidth" />
           </xsl:attribute>
           <xsl:attribute name="height">
             <xsl:value-of select="$FlashHeight" />
           </xsl:attribute>
           <xsl:attribute name="src">
             <xsl:value-of select="$FlashSource" />
           </xsl:attribute>
         </embed>
       </object>
     </xsl:when>
   </xsl:choose>
   
 </center>
  </xsl:template>
</xsl:stylesheet>
