<xsl:stylesheet version="1.0" xmlns:marc="http://www.loc.gov/MARC21/slim" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" omit-xml-declaration="yes"/>
    <xsl:template match="/">
        <xsl:apply-templates></xsl:apply-templates>
    </xsl:template>
    <xsl:template match="record">
		<div class="folder-content" style="padding-top:7px">
			<ul>
				<li>
					<A>
						<xsl:attribute name="href">
							{POST_URL}
						</xsl:attribute>
						<xsl:value-of select="datafield[@tag=245]/subfield[@code='a']/text()"/>
					</A>
				</li>
			</ul>	
		</div>
    </xsl:template>
</xsl:stylesheet>