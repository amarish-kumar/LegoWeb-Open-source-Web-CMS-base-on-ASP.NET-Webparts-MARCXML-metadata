<xsl:stylesheet version="1.0" xmlns:marc="http://www.loc.gov/MARC21/slim" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" omit-xml-declaration="yes"/>
    <xsl:template match="/">
      <div>
        <ul>
          <xsl:apply-templates></xsl:apply-templates>
        </ul>
      </div>
    </xsl:template>
    <xsl:template match="record">
                <li>
                  <img src="images/icon-star.png" border="0" style="vertical-align: middle;"/>
                  <xsl:text>  </xsl:text>
                  <a>
                        <xsl:attribute name="href">
                            {POST_URL}mnuId=<xsl:value-of select="controlfield[@tag=002]"/>&amp;catid=<xsl:value-of select="controlfield[@tag=002]"/>&amp;contentid=<xsl:value-of select="controlfield[@tag=001]"/>
                        </xsl:attribute>
                        <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
                    </a>
                    <span class='date'>
                        (<xsl:value-of select="controlfield[@tag=005]"/>)
                    </span>
                </li>
    </xsl:template>
</xsl:stylesheet>