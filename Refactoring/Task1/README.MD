Завдання №1. – 10 балів.

Користуючись Lex&Yacc (3) (стор. 16-17) запрограмувати калькулятор з дужками.

## Deployment

**To deploy use this commands:**
 - ```bison -d calc.y```
 - ```flex calc.l```
 - ```gcc calc.tab.c lex.yy.c -lm -o calc```
 - ```\.calc.exe```

 **To install Lex & Yacc follow instruction on stackoverflow**:
 [Link to solution](https://stackoverflow.com/a/5545924)