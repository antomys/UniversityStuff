%{
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include "func.h"
#include "calc.tab.h"

/* Used for variable stores. Defined in mem.h */
extern double variable_values[100];
extern int variable_set[100];

/* Flex functions */
extern int yylex(void);
extern void yyterminate();
void yyerror(const char *s);
extern FILE* yyin;
%}

/* Bison declarations */

/* 
If you have used %union to specify a variety of data types, then you must declare a choice among these 
types for each terminal or nonterminal symbol that can have a semantic value. 

Then each time you use $$ or $n, its data type is determined by which symbol it refers to in the rule. 
(http://dinosaur.compilertools.net/bison/bison_6.html)
*/
%union {
	int index;
	double num;
}

%token<num> NUMBER
%token<num> L_BRACKET R_BRACKET
%token<num> DIV MUL ADD SUB EQUALS
%token<num> PI
%token<num> POW SQRT FACTORIAL MOD
%token<num> LOG2 LOG10
%token<num> FLOOR CEIL ABS
%token<num> GBP_TO_USD USD_TO_GBP 
%token<num> GBP_TO_EURO EURO_TO_GBP 
%token<num> USD_TO_EURO EURO_TO_USD
%token<num> COS SIN TAN COSH SINH TANH
%token<num> CEL_TO_FAH FAH_TO_CEL
%token<num> M_TO_KM KM_TO_M
%token<num> VAR_KEYWORD 
%token<index> VARIABLE
%token<num> EOL
%type<num> program_input
%type<num> line
%type<num> calculation
%type<num> constant
%type<num> expr
%type<num> function
%type<num> log_function
%type<num> trig_function
%type<num> hyperbolic_function
%type<num> assignment
%type<num> conversion
%type<num> temperature_conversion
%type<num> distance_conversion

/* Set operator precedence, follows BODMAS rules. */
%left SUB 
%left ADD
%left MUL 
%left DIV 
%left POW SQRT 
%left L_BRACKET R_BRACKET

/* Grammar rules */
/*Symbols in Bison grammars represent the grammatical classifications of the language.

A terminal symbol (also known as a token type) represents a class of syntactically equivalent tokens. You use the symbol in grammar rules to mean that a token in that class is allowed. The symbol is represented in the Bison parser by a numeric code, and the yylex function returns a token type code to indicate what kind of token has been read. You don't need to know what the code value is; you can use the symbol to stand for it.

A nonterminal symbol stands for a class of syntactically equivalent groupings. The symbol name is used in writing grammar rules. By convention, it should be all lower case. 

/*  A Bison grammar rule has the following general form:

result: components...
        ;

where result is the nonterminal symbol that this rule describes and components are various terminal and nonterminal symbols that are put together by this rule.

For example,

exp:      exp '+' exp
        ;

says that two groupings of type exp, with a `+' token in between, can be combined into a larger grouping of type exp.

Whitespace in rules is significant only to separate symbols. You can add extra whitespace as you wish.

Multiple rules for the same result can be written separately or can be joined with the vertical-bar character `|' as follows:

result:    rule1-components...
        | rule2-components...
        ...
        ;
				
(http://dinosaur.compilertools.net/bison/bison_6.html)				
*/
%%
program_input:
	| program_input line
	;
	
line: 
			EOL 						 { printf("Please enter a calculation:\n"); }
		| calculation EOL  { printf("=%.2f\n",$1); }
    ;

calculation:
		  expr
		| function
		| assignment
		;
		
constant: PI { $$ = 3.142; }
		;
		
expr:
			SUB expr					{ $$ = -$2; }
    | NUMBER            { $$ = $1; }
		| VARIABLE					{ $$ = variable_values[$1]; }
		| constant	
		| function
		| expr DIV expr     { if ($3 == 0) { yyerror("Cannot divide by zero"); exit(1); } else $$ = $1 / $3; }
		| expr MUL expr     { $$ = $1 * $3; }
		| L_BRACKET expr R_BRACKET { $$ = $2; }
		| expr ADD expr     { $$ = $1 + $3; }
		| expr SUB expr   	{ $$ = $1 - $3; }
		| expr POW expr     { $$ = pow($1, $3); }
		| expr MOD expr     { $$ = modulo($1, $3); }
    ;
		
function: 
			conversion
		| log_function
		| trig_function
		| hyperbolic_function
		|	SQRT expr      		{ $$ = sqrt($2); }
		| expr FACTORIAL		{ $$ = factorial($1); }
		| ABS expr 					{ $$ = abs($2); }
		| FLOOR expr 				{ $$ = floor($2); }
		| CEIL expr 				{ $$ = ceil($2); }
		;

trig_function:
			COS expr  			  { $$ = cos($2); }
		| SIN expr 					{ $$ = sin($2); }
		| TAN expr 					{ $$ = tan($2); }
		;
	
log_function:
			LOG2 expr 				{ $$ = log2($2); }
		| LOG10 expr 				{ $$ = log10($2); }
		;
		
hyperbolic_function:
			COSH expr  			  { $$ = cosh($2); }
		| SINH expr 				{ $$ = sinh($2); }
		| TANH expr 				{ $$ = tanh($2); }
		;
		
conversion:
		temperature_conversion
		| distance_conversion
		|	expr GBP_TO_USD   { $$ = gbp_to_usd($1); }
		| expr USD_TO_GBP   { $$ = usd_to_gbp($1); }
		| expr GBP_TO_EURO  { $$ = gbp_to_euro($1); }
		| expr EURO_TO_GBP  { $$ = euro_to_gbp($1); }
		| expr USD_TO_EURO  { $$ = usd_to_euro($1); }
		| expr EURO_TO_USD  { $$ = euro_to_usd($1); }
		;

temperature_conversion:
			expr CEL_TO_FAH 	{ $$ = cel_to_fah($1); }
		| expr FAH_TO_CEL 	{ $$ = fah_to_cel($1); }

distance_conversion:
			expr M_TO_KM 			{ $$ = m_to_km($1); }
		| expr KM_TO_M 			{ $$ = km_to_m($1); }
		
assignment: 
		VAR_KEYWORD VARIABLE EQUALS calculation { $$ = set_variable($2, $4); }
		;
%%

/* Entry point */
int main(int argc, char **argv)
{
		// Command line
	yyin = stdin;
	yyparse();
}

/* Display error messages */
void yyerror(const char *s)
{
	printf("ERROR: %s\n", s);
}
