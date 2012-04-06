<xsl:stylesheet version="1.0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />
  <xsl:template match="/">
    <xsl:apply-templates></xsl:apply-templates>
  </xsl:template>
  <xsl:template match="record">
    <div class="article-container">
      <xsl:apply-templates select="datafield[@tag=245]"/>
      <xsl:apply-templates select="datafield[@tag=520]"/>
      <xsl:variable name="ReportHeight" select="datafield[@tag=300]/subfield[@code='h']/text()">
      </xsl:variable>
      <xsl:variable name="ReportSource" select="datafield[@tag=856]/subfield[@code='u']/text()">
      </xsl:variable>

      <iframe width="100%">
        <xsl:attribute name="src">
          <xsl:value-of select="$ReportSource" />
        </xsl:attribute>
        <xsl:attribute name="height">
          <xsl:value-of select="$ReportHeight" />
        </xsl:attribute>
      </iframe>
    </div>
  </xsl:template>

  <xsl:template match="datafield[@tag=245]">
    <xsl:for-each select="subfield[@code='a']">
      <div class="article-titletext">
        <xsl:value-of select="text()"/>
      </div>
    </xsl:for-each>
    <xsl:for-each select="subfield[@code='b']">
      <div class="article-summarytext">
        <xsl:value-of select="text()"/>
      </div>
    </xsl:for-each>
  </xsl:template>
  <xsl:template match="datafield[@tag=520]">
    <xsl:for-each select="subfield[@code='a']">
      <div class="article-bodytext">
        <xsl:value-of select="text()" disable-output-escaping="yes"/>
      </div>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
