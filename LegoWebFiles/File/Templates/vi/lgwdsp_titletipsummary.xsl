<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" omit-xml-declaration="yes"/>
    <xsl:template match="/">
      <xsl:apply-templates></xsl:apply-templates>
    </xsl:template>
    <xsl:template match="record">
		<div style="padding-top:5px;position:relative;clear:both;">
          <span class="icon-11-arr-target">
            <a  class="link-title">
                    <xsl:attribute name="href">
                      <xsl:value-of select="controlfield[@tag=001]/text()"/>
                    </xsl:attribute>       
                    <xsl:attribute  name="onmouseover">Tip('<xsl:value-of select="datafield[@tag=245]/subfield[@code='b']/text()"/>')</xsl:attribute>                   
                    <xsl:attribute name="onmouseout">UnTip()</xsl:attribute>
                    <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
            </a>
          </span>	
		</div>
    </xsl:template>
</xsl:stylesheet>