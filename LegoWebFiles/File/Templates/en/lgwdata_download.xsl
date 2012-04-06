<xsl:stylesheet version="1.0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" />
	<xsl:template match="/">
		<xsl:apply-templates></xsl:apply-templates>
	</xsl:template>
	<xsl:template match="record">
    <xsl:variable name="ThumbExist" select="count(datafield[@tag=245]/subfield[@code='u'])" />
    <table width="100%"  cellpadding="0" cellspacing="0" border="0">
      <tr>
        <td valign="middle" align="center" width="132px">
          <xsl:choose>
            <xsl:when test="$ThumbExist">
              <div>
                <img style="max-width:132px;max-height:180px;margin:10px;">
                  <xsl:attribute name="src">
                    <xsl:value-of select="datafield[@tag=245]/subfield[@code='u']"/>
                  </xsl:attribute>
                </img>
              </div>
            </xsl:when>
            <xsl:otherwise xml:space="default">
              <div>
                <img style="max-width:200px;max-height:180px;margin:10px;" src="images/no-image.gif" />
              </div>
            </xsl:otherwise>
          </xsl:choose>
        </td>
        <td valign="top">
          <div style="margin-top:0px">
            <span style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: regular;color:#666677">
              <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
            </span>
            <br/>
            <em style="font-family: Arial, Helvetica, sans-serif; font-size: small;color:#666666">
              <xsl:value-of select="datafield[@tag=245]/subfield[@code='b']/text()" disable-output-escaping="yes"/>
            </em>
            <br/>
            <span style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style:regular;color: #666677">
              <xsl:value-of select="datafield[@tag=260]/subfield[@code='a']/text()"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="datafield[@tag=260]/subfield[@code='b']/text()"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="datafield[@tag=260]/subfield[@code='c']/text()"/>
            </span>
            <br/>
            <span style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style:regular;color: #666677">
              <xsl:value-of select="datafield[@tag=300]/subfield[@code='a']/text()"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="datafield[@tag=300]/subfield[@code='b']/text()"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="datafield[@tag=300]/subfield[@code='c']/text()"/>
            </span>
            <br/>
            <xsl:apply-templates select="datafield[@tag=100]"/>
            <xsl:apply-templates select="datafield[@tag=110]"/>
            <br/>
             <xsl:apply-templates select="datafield[@tag=856]"/>
          </div>
        </td>
      </tr>
      <tr>
        <td colspan="2">
          <div style="clear:both"></div>
            <p style="padding-top:10px">
              <xsl:value-of select="datafield[@tag=520]/subfield[@code='a']/text()" disable-output-escaping="yes"/>
            </p>
        </td>
      </tr>
    </table>
  </xsl:template>
  <xsl:template match="datafield[@tag=100]">
    <span style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;color: #666677">
      <xsl:for-each select="subfield[@code='a']">
        <xsl:value-of select="text()"/>
      </xsl:for-each>
    </span>        
  </xsl:template>
  <xsl:template match="datafield[@tag=110]">
    <span style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;color: #666677">
      <xsl:for-each select="subfield[@code='a']">
        <xsl:value-of select="text()"/>
      </xsl:for-each>
    </span>
  </xsl:template>
  <xsl:template match="datafield[@tag=856]">
      <a>
        <xsl:for-each select="subfield[@code='u']">
          <xsl:attribute name="href">
            javascript:void(0);
          </xsl:attribute>
          <xsl:attribute name="OnClick">
            javascript:window.open('downloadcounter.aspx?urlid=<xsl:value-of select="@id"/>&amp;url=<xsl:value-of select="text()"/>')
          </xsl:attribute>
        </xsl:for-each>
        <img src="images/icondown.gif" class="padding-right"  />
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
<!-- Stylus Studio meta-information - (c)1998-2002 eXcelon Corp.
<metaInformation>
<scenarios ><scenario default="no" name="Ray Charles" userelativepaths="yes" externalpreview="no" url="..\xml\MARC21slim\raycharles.xml" htmlbaseurl="" outputurl="" processortype="internal" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext=""/><scenario default="yes" name="s7" userelativepaths="yes" externalpreview="no" url="..\ifla\sally7.xml" htmlbaseurl="" outputurl="" processortype="internal" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext=""/></scenarios><MapperInfo srcSchemaPath="" srcSchemaRoot="" srcSchemaPathIsRelative="yes" srcSchemaInterpretAsXML="no" destSchemaPath="" destSchemaRoot="" destSchemaPathIsRelative="yes" destSchemaInterpretAsXML="no"/>
</metaInformation>
-->
