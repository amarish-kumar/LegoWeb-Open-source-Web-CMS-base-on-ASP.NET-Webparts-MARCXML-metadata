<xsl:stylesheet version="1.0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" />
	<xsl:template match="/">
		<xsl:apply-templates></xsl:apply-templates>
	</xsl:template>
	<xsl:template match="record">
    <table width="100%" border="0" bordercolor="#dfd0a3">
      <tr>
      <td valign ="top" align="left" width="132px">
        <xsl:variable name="ThumbExist" select="count(datafield[@tag=245]/subfield[@code='u'])" />
        <xsl:choose>
          <xsl:when test="$ThumbExist">
            <div>
              <img style="width:132px;max-height:150px;margin:10px;">
                <xsl:attribute name="src">
                  <xsl:value-of select="datafield[@tag=245]/subfield[@code='u']"/>
                </xsl:attribute>
              </img>
            </div>
          </xsl:when>
          <xsl:otherwise xml:space="default">
            <div>
              <img style="width:132px;max-height:150px;margin:10px;" src="images/no-image.gif" />
            </div>
          </xsl:otherwise>
        </xsl:choose>
      </td>
        <td valign="top">
          <div style="margin-top:10px">
            <span style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: regular">
              <a>
                <xsl:attribute name="href">
                  <xsl:value-of select="controlfield[@tag=001]"/>
                </xsl:attribute>
                <xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
              </a>
            </span>
            <br/>
            <span style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-weight: normal; font-style: italic;color: #666666">
              <xsl:value-of select="datafield[@tag=245]/subfield[@code='b']/text()"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="datafield[@tag=245]/subfield[@code='c']/text()"/>
            </span>
            <br/>
            <span style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style:regular;color: #666666">
              <xsl:value-of select="datafield[@tag=260]/subfield[@code='a']/text()"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="datafield[@tag=260]/subfield[@code='b']/text()"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="datafield[@tag=260]/subfield[@code='c']/text()"/>
            </span>
            <br/>
            <span style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-weight: normal; font-style:regular;color: #666666">
              <xsl:value-of select="datafield[@tag=100]/subfield[@code='a']/text()"/>
              <xsl:text> </xsl:text>
              <xsl:value-of select="datafield[@tag=110]/subfield[@code='a']/text()"/>
            </span>
            <br/>
            <xsl:apply-templates select="datafield[@tag=856]"/>
          </div>
        </td>
      </tr>
    </table>
  </xsl:template>
  <xsl:template match="datafield[@tag=856]">
      <a>
        <xsl:for-each select="subfield[@code='u']">
          <xsl:attribute name="href">
            javascript:void(0);
          </xsl:attribute>
          <xsl:attribute name="OnClick">
            javascript:window.open('download_counter.aspx?urlid=<xsl:value-of select="@id"/>&amp;url=<xsl:value-of select="text()"/>')
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
