grammar FlyLang;
program: statement*;
statement:
	(assignment | returnStmt | methodCall | use) SC // Statements with ;
	| (ifStmt | whileStmt | forStmt | definition | classDef); // Without ;
assignment:
	name = ID EQ expression
	| name = ID SBL expression SBR EQ expression
	| name = ID (ADD ADD | SUB SUB);
returnStmt: 'return' expression;
use: 'use' id; // Library imports
definition: name = ID PRL names PRR LQ expression;
classDef: 'class' ID BRL ((assignment | definition) SC)* BRR;

ifStmt: 'if' COLON expression LQ (action) elifStmt* ( | elseStmt);
elifStmt: 'elif' COLON expression LQ (action) ( | elseStmt);
elseStmt: 'else' COLON LQ (action);
whileStmt: 'while' COLON expression LQ (action);
forStmt: 'for' COLON expression IN expression LQ (action);
boolean: TRUE | FALSE;
names: | ID (',' ID)*;
expression:
	intLit
	| boolean
	| floatLit
	| string
	| array
	| dictionary
	| newClass
	| methodCall
	| varCall
	| action
	| PRL expression PRR
	| op = '!' expression
	| left = expression op = BOOLOPERATOR right = expression
	| left = expression op = (MUL | DIV) right = expression
	| left = expression op = (SUB | ADD) right = expression;
action: BRL statement* BRR;
newClass: 'new' ID PRL  ( | expression (',' expression)*) PRR;
varCall: ID | ID SBL expression SBR;
methodCall: name = id PRL ( | expression (',' expression)*) PRR;
string: STRING;
intLit: INT;
floatLit: FLOAT;
literal: intLit | string | floatLit | boolean | array | dictionary;
array: SBL ((expression (',' expression)*) |) SBR;
dictionary: SBL ((keyItem (',' keyItem)*) | COLON) SBR;
keyItem: key = expression COLON value = expression;

id: (ID | ID ('.' ID)*);
// Lexer:
LET: 'let';
IN: 'in';
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

SBL: '[';
SBR: ']';
PRL: '(';
PRR: ')';
BRL: '{';
BRR: '}';
SC: ';';
COLON: ':';
INT: [0-9]+;
FLOAT: [0-9]+ 'f' | [0-9]+ '.'+ [0-9]+;
STRING: '"' ('\\' ["\\] | ~["\\\r\n])* '"';
ID: ('a' ..'z'|'A'..'Z')+ ('a' ..'z' | 'A' ..'Z' | '0' ..'9')*;
ALPHA: ('a' ..'z' | 'A' ..'Z')+;
COMMENT: ('//' ~[\r\n]* '\r'? '\n' | '/*' .*? '*/') -> channel(HIDDEN);
WS: [ \t\r\n]+ -> skip;