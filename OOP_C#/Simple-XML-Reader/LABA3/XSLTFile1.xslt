<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
  
    <xsl:template match="DataBaseTable">

      <table border="1">

        <TR bgcolor="#008000">
          <TD><strong>Name</strong></TD>
          <TD><strong>Country</strong></TD>
          <TD><strong>Manufacturer</strong></TD> 
          <TD><strong>Year</strong></TD>
          <TD><strong>Rating</strong></TD>
          <TD><strong>NATO</strong></TD>
          <TD><strong>GA</strong></TD>
        </TR>

        <xsl:for-each select="Game">
          <xsl:sort order="ascending" select="@Year"/>
          
        <TR>
          <TD><b><xsl:value-of select="@NAME"/></b></TD>
          <TD><xsl:value-of select="@COUNTRY"/></TD>
          <TD><xsl:value-of select="@COMPANY"/></TD>
          <TD><xsl:value-of select="@YEAR"/></TD>
          <TD><xsl:value-of select="@RATE"/></TD>
          <TD><xsl:value-of select="@NATO"/></TD>
          <TD><xsl:value-of select="@GA"/></TD>
        </TR>
        </xsl:for-each>
 
      </table>

  
    </xsl:template>
  
</xsl:stylesheet>
