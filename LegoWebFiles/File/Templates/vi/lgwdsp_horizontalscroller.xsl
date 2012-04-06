<xsl:stylesheet version="1.0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" />
  <xsl:template match="/">
    <xsl:apply-templates></xsl:apply-templates>
  </xsl:template>
  <xsl:template match="record">  		
          <xsl:variable name="ThumbExist" select="count(datafield[@tag=245]/subfield[@code='u'])" />         
		  <xsl:choose>
            <xsl:when test="$ThumbExist"> 			
			<li>
			 <a>
                <xsl:attribute name="href">
                  <xsl:value-of select="controlfield[@tag=001]/text()"/>
                </xsl:attribute>
				<xsl:attribute name="title">
                <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
                </xsl:attribute>               
				<img>
                <xsl:attribute name="src">
                <xsl:value-of select="datafield[@tag=245]/subfield[@code='u']"/>
                </xsl:attribute>
				<xsl:attribute name="alt">
                <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
                </xsl:attribute>			
                </img>		
              </a>
			</li>
            </xsl:when>
            <xsl:otherwise xml:space="default">
              <div class= "icon-no-image" style="width:132px;height:150px;margin:10px;">
              </div>
            </xsl:otherwise>
          </xsl:choose>				  
  </xsl:template>
  <xsl:template match="datafield[@tag=856]">
    <a>
      <xsl:for-each select="subfield[@code='u']">
        <xsl:attribute name="href">
          javascript:void(0);
        </xsl:attribute>
        <xsl:attribute name="OnClick">
          javascript:window.open('DownloadCounter.aspx?urlid=<xsl:value-of select="@id"/>&amp;url=<xsl:value-of select="text()"/>')
        </xsl:attribute>
      </xsl:for-each>
      <span class="icon-download"></span>
      <span style="text-decoration:underline;">
        <xsl:for-each select="subfield[@code='f']">
          <xsl:value-of select="text()"/>
        </xsl:for-each>
      </span>
      <span>
        <xsl:for-each select="subfield[@code='s']">
          <xsl:value-of select="text()"/>
        </xsl:for-each>
        (
        <xsl:for-each select="subfield[@code='n']">
          <xsl:value-of select="text()"/>
        </xsl:for-each>
        )
      </span>

    </a>

  </xsl:template>
</xsl:stylesheet>
