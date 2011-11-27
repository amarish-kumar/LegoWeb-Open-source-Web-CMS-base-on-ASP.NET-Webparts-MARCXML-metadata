<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>  
  <xsl:template match="/">



          
        <div id='firstNew'>      
      <xsl:apply-templates></xsl:apply-templates>
        </div>



          

  </xsl:template>
  <xsl:strip-space elements="*"/>
  <xsl:template match="record">
    <xsl:variable name="contentid" select="controlfield[@tag=001]/text()" />
    <xsl:variable name="title" select="datafield[@tag=245]/subfield[@code='a']/text()" />
    <xsl:variable name="summary" select="datafield[@tag=245]/subfield[@code='b']/text()" />
    <xsl:variable name="posturl" select="datafield[@tag=245]/subfield[@code='l']/text()" />
    <div class="p1_tooltip">
      <a>
        <xsl:attribute name="onmouseover">
          javascript:ShowContent('id<xsl:value-of select="$contentid"/>'); return true;
        </xsl:attribute>
        <xsl:attribute name="onmouseout">
          javascript:HideContent('id<xsl:value-of select="$contentid"/>'); return true;
        </xsl:attribute>
        <xsl:attribute name="href"><xsl:value-of select="$posturl"/>?contentid=<xsl:value-of select="$contentid"/></xsl:attribute>
        <xsl:value-of select="$title"/>        
      </a>      
       <div style="display:none; position:absolute; border-style: solid 1px blue; background-color: #c6e6f6; padding: 5px 0px 2px 5px;width:250px">
         <xsl:attribute name="id">id<xsl:value-of select="$contentid"/></xsl:attribute>         
        <b>
          <xsl:value-of select="$title"/>
        </b><br/>
         <xsl:value-of select="$summary"/>
      </div>
    </div>
  </xsl:template>




</xsl:stylesheet>

