<xsl:stylesheet version="1.0" xmlns:marc="http://www.loc.gov/MARC21/slim" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes"/>
  <xsl:template match="record">
    <div class="newsDetail">
      <div style="padding-bottom:15px">
        <span style="font-family:arial;font-size:14pt;font-weight:bold;">
          <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
        </span>
      </div>
      <div class="lead">
        <xsl:value-of select="datafield[@tag=245]/subfield[@code='b']/text()"/>
      </div>
      <div>
        <p>
          <xsl:value-of select="datafield[@tag=520]/subfield[@code='a']/text()" disable-output-escaping="yes"/>
        </p>
      </div>
    </div>
  </xsl:template>
</xsl:stylesheet>
