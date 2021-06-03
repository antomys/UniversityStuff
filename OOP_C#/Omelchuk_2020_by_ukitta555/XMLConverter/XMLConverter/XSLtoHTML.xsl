<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html"> </xsl:output>
	

    <xsl:template match="/">
		<html>
			<body>
				<table>
					<tr>
						<th>Language name</th>
						<th>Authors</th>
						<th>Release year</th>
						<th>Type of language</th>
						<th>Abstraction level</th>
						<th>Commonly used for</th>
					</tr>
					<xsl:for-each select = "ProgLanguages/ProgLanguage">
						<tr>
							<td>
								<xsl:value-of select ="@LanguageName"/>
							</td>
							<td>
							<xsl:value-of select ="@Authors"/>
							</td>
							<td>
							<xsl:value-of select ="@ReleaseYear"/>
							</td>
							<td>
							<xsl:value-of select ="@TypeOfLanguage"/>
							</td>
							<td>
							<xsl:value-of select ="@AbstractionLevel"/>
							</td>
							<td>
							<xsl:value-of select ="@CommonlyUsedFor"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
    </xsl:template>
</xsl:stylesheet>
