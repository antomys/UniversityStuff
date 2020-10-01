public class JavaSymbol {
    String str;
    int charSt;
    private int line;
    private int column;
    private Lexem lex;

    Lexem getLex() {
        return lex;
    }

    public JavaSymbol(String str, int line, int column, Lexem lex, int charSt) {
        this.str = str;
        this.line = line;
        this.column = column;
        this.lex = lex;
        this.charSt = charSt;
    }

    public String toString() {
        if (lex == Lexem.eof) return null;
        if (lex == Lexem.notLexem) return "";
        if (lex == Lexem.error) return null;
        return "Class of Lexem : " + lex.toString() + " line: " + line + " column :" + column + " lexem: " + str + " \n";
    }
}