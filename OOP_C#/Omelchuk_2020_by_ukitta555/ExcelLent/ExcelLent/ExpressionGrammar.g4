grammar ExpressionGrammar;

compileUnit : expression EOF;

expression: LPAREN expression RPAREN  #ParenthesizedExpression
			| expression EXPONENT expression #ExponentExpression
			| expression MOD expression  #ModExpression
			| expression DIV expression  #DivExpression
			| MINUS expression  #MinusExpression
			| PLUS expression  #PlusExpression
			| 'mmin' LPAREN expression','(expression ',')* expression RPAREN  #MMinExpression
			| 'mmax' LPAREN expression','(expression ',')* expression RPAREN #MMaxExpression
			| NUMBER #NumberExpression
			| IDENTIFIER #IdentifierExpression;

NUMBER: INT ('.'INT)?;

IDENTIFIER: [A-Z]+[0-9][0-9]*;

INT: [0-9]+;

EXPONENT: '^';
PLUS: '+';
MINUS: '-';
LPAREN: '(';
RPAREN: ')';
MOD: 'mod';
DIV: 'div';

WS : [ \t\r\n] -> channel(HIDDEN);