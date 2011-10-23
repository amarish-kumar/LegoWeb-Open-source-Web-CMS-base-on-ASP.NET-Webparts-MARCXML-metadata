<xsl:stylesheet version="1.0" xmlns:marc="http://www.loc.gov/MARC21/slim" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 xmlns:date="http://exslt.org/dates-and-times" extension-element-prefixes="date">

  <xsl:output method="html" omit-xml-declaration="yes"/>
  <xsl:template match="record">

    <xsl:variable name="ThumbExist" select="count(datafield[@tag=245]/subfield[@code='u'])" />
         <xsl:choose>
        <xsl:when test="$ThumbExist">
          <div>
              <div class="link_title">
                <a>
                  <xsl:attribute name="href">
                    {POST_URL}mnuId=<xsl:value-of select="controlfield[@tag=002]"/>&amp;catid=<xsl:value-of select="controlfield[@tag=002]"/>&amp;contentid=<xsl:value-of select="controlfield[@tag=001]"/>
                  </xsl:attribute>
                  <strong>
                    <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
                  </strong>
                </a>
                <span class='date'>
                  (<xsl:value-of select="controlfield[@tag=005]"/>)
                </span>
              </div>
              <div class="First_Sum">
                <xsl:value-of select="datafield[@tag=245]/subfield[@code='b']/text()"/>
              </div>
          </div>
          <div style="float:right;">
            <a>
              <xsl:attribute name="href">
                {POST_URL}mnuId=<xsl:value-of select="controlfield[@tag=002]"/>&amp;catid=<xsl:value-of select="controlfield[@tag=002]"/>&amp;contentid=<xsl:value-of select="controlfield[@tag=001]"/>
              </xsl:attribute>
              <span>
                Chi tiết...
              </span>
            </a>
           
          </div>
        
            <a>
              <xsl:attribute name="href">
                download_counter.aspx?meta_content_id=<xsl:value-of select="controlfield[@tag=001]"/>&amp;url=<xsl:value-of select="datafield[@tag=856]/subfield[@code='u']"/>
              </xsl:attribute>
              <b>Tải về</b>
            </a>
        
        </xsl:when>
      </xsl:choose>
    </xsl:template>
</xsl:stylesheet>
