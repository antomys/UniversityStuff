
/* A Bison parser, made by GNU Bison 2.4.1.  */

/* Skeleton interface for Bison's Yacc-like parsers in C
   
      Copyright (C) 1984, 1989, 1990, 2000, 2001, 2002, 2003, 2004, 2005, 2006
   Free Software Foundation, Inc.
   
   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.
   
   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.  */

/* As a special exception, you may create a larger work that contains
   part or all of the Bison parser skeleton and distribute that work
   under terms of your choice, so long as that work isn't itself a
   parser generator using the skeleton or a modified version thereof
   as a parser skeleton.  Alternatively, if you modify or redistribute
   the parser skeleton itself, you may (at your option) remove this
   special exception, which will cause the skeleton and the resulting
   Bison output files to be licensed under the GNU General Public
   License without this special exception.
   
   This special exception was added by the Free Software Foundation in
   version 2.2 of Bison.  */


/* Tokens.  */
#ifndef YYTOKENTYPE
# define YYTOKENTYPE
   /* Put the tokens into the symbol table, so that GDB and other debuggers
      know about them.  */
   enum yytokentype {
     NUMBER = 258,
     L_BRACKET = 259,
     R_BRACKET = 260,
     DIV = 261,
     MUL = 262,
     ADD = 263,
     SUB = 264,
     EQUALS = 265,
     PI = 266,
     POW = 267,
     SQRT = 268,
     FACTORIAL = 269,
     MOD = 270,
     LOG2 = 271,
     LOG10 = 272,
     FLOOR = 273,
     CEIL = 274,
     ABS = 275,
     GBP_TO_USD = 276,
     USD_TO_GBP = 277,
     GBP_TO_EURO = 278,
     EURO_TO_GBP = 279,
     USD_TO_EURO = 280,
     EURO_TO_USD = 281,
     COS = 282,
     SIN = 283,
     TAN = 284,
     COSH = 285,
     SINH = 286,
     TANH = 287,
     CEL_TO_FAH = 288,
     FAH_TO_CEL = 289,
     M_TO_KM = 290,
     KM_TO_M = 291,
     VAR_KEYWORD = 292,
     VARIABLE = 293,
     EOL = 294
   };
#endif



#if ! defined YYSTYPE && ! defined YYSTYPE_IS_DECLARED
typedef union YYSTYPE
{

/* Line 1676 of yacc.c  */
#line 29 "calc.y"

	int index;
	double num;



/* Line 1676 of yacc.c  */
#line 98 "calc.tab.h"
} YYSTYPE;
# define YYSTYPE_IS_TRIVIAL 1
# define yystype YYSTYPE /* obsolescent; will be withdrawn */
# define YYSTYPE_IS_DECLARED 1
#endif

extern YYSTYPE yylval;


