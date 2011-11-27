<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" omit-xml-declaration="yes" />
  <xsl:template match="/">
    <xsl:apply-templates></xsl:apply-templates>                        
  </xsl:template>  
  <xsl:template match="record">
    <xsl:variable name="Title" select="datafield[@tag=245]/subfield[@code='a']/text()"></xsl:variable>    
    <xsl:variable name="DefaultHeight"><xsl:value-of select="datafield[@tag=300]/subfield[@code='h']/text()"/></xsl:variable>
    <xsl:variable name="DefaultWidth"><xsl:value-of select="datafield[@tag=300]/subfield[@code='w']/text()"/></xsl:variable>
    <xsl:apply-templates select="datafield[@tag=856]">
      <xsl:with-param name="dHeight" select="$DefaultHeight"/>
      <xsl:with-param name="dWidth" select="$DefaultWidth"/>
    </xsl:apply-templates>
  </xsl:template>
  <xsl:template match="datafield[@tag=856]">
    <xsl:param name="dHeight" />
    <xsl:param name="dWidth" />

    <xsl:variable name="MediaWidth" select="subfield[@code='w']/text()"/>
    <xsl:variable name="MediaHeight" select="subfield[@code='h']/text()"/>
    <xsl:variable name="MediaSource" select="subfield[@code='u']/text()"/>

    <xsl:choose>
      <xsl:when test="contains($MediaSource,'.jpg') or contains($MediaSource,'.png') or contains($MediaSource,'.gif')">
        <img>
          <xsl:attribute name="width">
            <xsl:choose>
              <xsl:when test ="$MediaWidth">
                <xsl:value-of select ="$MediaWidth"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dWidth"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>

          <xsl:attribute name="height">
            <xsl:choose>
              <xsl:when test ="$MediaHeight">
                <xsl:value-of select ="$MediaHeight"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dHeight"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>          
          <xsl:attribute name="src">
            <xsl:value-of select="$MediaSource" />
          </xsl:attribute>
        </img>
      </xsl:when>

      <xsl:when test="contains($MediaSource,'.swf')">
        <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" id="flashControl" align="middle" border="0">
          <xsl:attribute name="width">
            <xsl:choose>
              <xsl:when test ="$MediaWidth">
                <xsl:value-of select ="$MediaWidth"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dWidth"/>
              </xsl:otherwise>
            </xsl:choose>                        
          </xsl:attribute>
          <xsl:attribute name="height">
            <xsl:choose>
              <xsl:when test ="$MediaHeight">
                <xsl:value-of select ="$MediaHeight"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dHeight"/>
              </xsl:otherwise>
            </xsl:choose>                       
          </xsl:attribute>
          <param name="movie">
            <xsl:attribute name="value">
              <xsl:value-of select="$MediaSource" />
            </xsl:attribute>
          </param>
          <param name="quality" value="high" />
          <param name="wmode" value="transparent"/>
          <param name="bgcolor" value="#ffffff" />
          <embed quality="high" bgcolor="#ffffff" wmode="transparent" name="flashControl" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer">
            <xsl:attribute name="width">
              <xsl:choose>
                <xsl:when test ="$MediaWidth">
                  <xsl:value-of select ="$MediaWidth"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select ="$dWidth"/>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:attribute>
            <xsl:attribute name="height">
              <xsl:choose>
                <xsl:when test ="$MediaHeight">
                  <xsl:value-of select ="$MediaHeight"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select ="$dHeight"/>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:attribute>
            <xsl:attribute name="src">
              <xsl:value-of select="$MediaSource" />
            </xsl:attribute>
          </embed>
        </object>
      </xsl:when>

      <xsl:when test="contains($MediaSource,'.wmv') or contains($MediaSource,'.mpeg') or contains($MediaSource,'.avi') or contains($MediaSource,'.wav')  or contains($MediaSource,'.wma')">
        <OBJECT id="mediaPlayer" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6">
          <xsl:attribute name="width">
            <xsl:choose>
              <xsl:when test ="$MediaWidth">
                <xsl:value-of select ="$MediaWidth"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dWidth"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="height">
            <xsl:choose>
              <xsl:when test ="$MediaHeight">
                <xsl:value-of select ="$MediaHeight"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dHeight"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <param name="URL">
            <xsl:attribute name="value">
              <xsl:value-of select="$MediaSource" />
            </xsl:attribute>
          </param>
            <PARAM NAME="rate" VALUE="1"/>
            <PARAM NAME="balance" VALUE="0"/>
            <PARAM NAME="currentPosition" VALUE="0"/>
            <PARAM NAME="defaultFrame" VALUE=""/>
            <PARAM NAME="playCount" VALUE="1"/>
            <PARAM NAME="autoStart" VALUE="-1"/>
            <PARAM NAME="ShowControl" VALUE="true"/>
            <PARAM NAME="ShowStatusBar" VALUE="true"/>
            <PARAM NAME="currentMarker" VALUE="0"/>
            <PARAM NAME="invokeURLs" VALUE="-1"/>
            <PARAM NAME="baseURL" VALUE=""/>
            <PARAM NAME="volume" VALUE="50"/>
            <PARAM NAME="mute" VALUE="0"/>
            <PARAM NAME="uiMode" VALUE="full"/>
            <PARAM NAME="stretchToFit" VALUE="1"/>
            <PARAM NAME="windowlessVideo" VALUE="0"/>
            <PARAM NAME="enabled" VALUE="-1"/>
            <PARAM NAME="enableContextMenu" VALUE="-1"/>
            <PARAM NAME="fullScreen" VALUE="0"/>
            <PARAM NAME="SAMIStyle" VALUE=""/>
            <PARAM NAME="SAMILang" VALUE=""/>
            <PARAM NAME="SAMIFilename" VALUE=""/>
            <PARAM NAME="captioningID" VALUE=""/>
            <PARAM NAME="enableErrorDialogs" VALUE="0"/>
            <PARAM NAME="_cx" VALUE="8467"/>
            <PARAM NAME="_cy" VALUE="7938"/>
            </OBJECT>
      </xsl:when>
                     
      <xsl:when test="contains($MediaSource,'.mov')">
       <object>
          <xsl:attribute name="width">
            <xsl:choose>
              <xsl:when test ="$MediaWidth">
                <xsl:value-of select ="$MediaWidth"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dWidth"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="height">
            <xsl:choose>
              <xsl:when test ="$MediaHeight">
                <xsl:value-of select ="$MediaHeight"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dHeight"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          
          <PARAM NAME="autostart" VALUE="true"/>
          <PARAM NAME="src">
            <xsl:attribute name="src">
              <xsl:value-of select="$MediaSource" />
            </xsl:attribute>
          </PARAM>
          <PARAM NAME="autoplay" VALUE="true"/>
          <PARAM NAME="controller" VALUE="true"/>          
        </object>

        <embed controller="true" autoplay="true" autostart="True" type="audio/wav">
          <xsl:attribute name="src">
            <xsl:value-of select="$MediaSource" />
          </xsl:attribute>
          <xsl:attribute name="width">
            <xsl:choose>
              <xsl:when test ="$MediaWidth">
                <xsl:value-of select ="$MediaWidth"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dWidth"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="height">
            <xsl:choose>
              <xsl:when test ="$MediaHeight">
                <xsl:value-of select ="$MediaHeight"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select ="$dHeight"/>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>                   
        </embed> 
      </xsl:when>
    </xsl:choose>
        
  </xsl:template>  
  
</xsl:stylesheet>
