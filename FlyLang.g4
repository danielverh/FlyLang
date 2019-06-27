grammar FlyLang;
program: statement*;
statement:
	(assignment | definition | return | methodCall | use) SC // Statements with ;
	| (if | while); // Without ;
assignment: name = ID EQ expression;
return: 'return' expression;
use: 'use' ID; // Library imports
definition: name = ID PRL names PRR LQ expression;
if: 'if' COLON expression LQ (action) elif* (|else);
elif: 'elif' COLON expression LQ (action) (|else);
else: 'else' COLON LQ (action);
while: 'while' COLON expression LQ (action);
boolean: TRUE | FALSE;
names: | ID (',' ID)*;
expression:
	int
	| boolean
	| float
	| string
	| methodCall
	| varCall
	| action
	| PRL expression PRR
	| left = expression op = BOOLOPERATOR right = expression
	| left = expression op = (MUL | DIV) right = expression
	| left = expression op = (SUB | ADD) right = expression;
action: BRL statement* BRR;
varCall: ID;
methodCall: name = ID PRL ( | expression (',' expression)*) PRR;
string: STRING;
int: INT;
float: FLOAT;
id: ID;
// Lexer:
LET: 'let';
TRUE: 'true';
FALSE: 'false';
BOOLOPERATOR: BEQ | '<' | '>' | '<=' | '>=' | '!=';

LQ: '=>';
EQ: '=';
BEQ: '==';
ADD: '+';
SUB: '-';
MUL: '*';
DIV: '/';

PRL: '(';
PRR: ')';
BRL: '{';
BRR: '}';
SC: ';';
COLON: ':';
INT: [0-9]+;
FLOAT: [0-9]+ 'f' | [0-9]+ '.'+ [0-9]+;
STRING: '"' ('\\' ["\\] | ~["\\\r\n])* '"';
ID: ('a' ..'z')+ ('a' ..'z' | 'A' ..'Z' | '0' ..'9')*;
ALPHA: ('a' ..'z' | 'A' ..'Z')+;
COMMENT: ('//' ~[\r\n]* '\r'? '\n' | '/*' .*? '*/') -> channel(HIDDEN);
WS: [ \t\r\n]+ -> skip;