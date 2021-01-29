%{
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "mem.h"
#include "calc.tab.h"
%}

/* Stop the error "undefined reference to 'yywrap'" from Flex */
%option noyywrap

/* Grammar rules
	[ \t] -- Match space or tab
	
	[0-9]+(\.[0-9]+)?  -- Match any combination of numbers. Includes decimals. 
	
	[a-zA-Z0-9]+ -- Match any combination of letters (lower and upper case) and numbers.
	
	. -- Match any remaining character.
*/

%%
[ \t]
[0-9]+(\.[0-9]+)?          					{ yylval.num = atof(yytext); return NUMBER; }
"+"                         					{ return ADD; }
"-"                         					{ return SUB; }
"*"                         					{ return MUL; }
"/"                         					{ return DIV; }
"%"																		{ return MOD; }
"^"                         					{ return POW; }
"="																		{ return EQUALS; }
"("																		{ return L_BRACKET; }
")"																		{ return R_BRACKET; }
"!"                         					{ return FACTORIAL; }
("pi")|("PI")													{ return PI; }
("mod")|("MOD")												{ return MOD; }
("pow")|("POW")												{	return POW; }
("floor")|("FLOOR")				  					{ return FLOOR; }
("ceil")|("CEIL")											{ return CEIL; }
("abs")|("ABS")												{ return ABS; }
("cos")|("COS")												{ return COS; }
("sin")|("SIN")												{ return SIN; }
("tan")|("TAN")												{ return TAN; }
("sinh")|("SINH")											{ return SINH; }
("cosh")|("COSH")											{ return COSH; }
("tanh")|("TANH")											{ return TANH; }
("sqrt")|("SQRT")											{ return SQRT; }
("log2")|("LOG2")											{ return LOG2; }
("log10")|("LOG10")										{ return LOG10; }
("gbp_to_usd")|("GBP_TO_USD")					{ return GBP_TO_USD; }
("usd_to_gbp")|("USD_TO_GBP")					{ return USD_TO_GBP; }
("gbp_to_euro")|("GBP_TO_EURO")				{ return GBP_TO_EURO; }
("euro_to_gbp")|("EURO_TO_GBP") 			{ return EURO_TO_GBP; }
("usd_to_euro")|("USD_TO_EURO")				{ return USD_TO_EURO; }
("euro_to_usd")|("EURO_TO_USD")				{ return EURO_TO_USD; }
("cel_to_fah")|("CEL_TO_FAH")					{ return CEL_TO_FAH; }
("fah_to_cel")|("FAH_TO_CEL")					{ return FAH_TO_CEL; }
("km_to_m")|("KM_TO_M")								{ return KM_TO_M; }
("m_to_km")|("M_TO_KM")								{ return M_TO_KM; }
("var")|("VAR")												{ return VAR_KEYWORD; }
[a-zA-Z0-9]+  												{ yylval.index = add_variable(yytext); return VARIABLE; }
[\n]                        					{ return EOL; }
. 																		{ yyerror("Undefined symbol"); exit(1); } 
%%
