<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes"/>
  <xsl:template match="record">    
    <div class="article-container">
      <xsl:apply-templates select="datafield[@tag=245]"/>
      <xsl:apply-templates select="datafield[@tag=780]"/>
      <xsl:apply-templates select="datafield[@tag=520]"/>     
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
  <xsl:template match="datafield[@tag=780]">
    <span class="article-contextual-links">
      <a>
        <xsl:attribute name="href">
          <xsl:value-of select="subfield[@code='w']/text()"/>
        </xsl:attribute>
        <xsl:text disable-output-escaping="yes">></xsl:text>
        <xsl:value-of select="subfield[@code='t']/text()"/>
      </a>
    </span>
  </xsl:template>
</xsl:stylesheet>
