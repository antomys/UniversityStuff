import java.util.regex.Matcher;
import java.util.regex.Pattern;

public enum Token {
    Token_not("!"),
    Token_assign("="),
    Token_tilde("~"),
    Token_questionmark("\\?"),
    Token_colon(":"),
    Token_isequal("\\=\\="),
    Token_notequal("!="),
    Token_and("\\&\\&"),
    Token_increment("\\+\\+"),
    Token_decrement("--"),
    Token_topower("\\^"),
    Token_euclidiandivision("%"),
    Token_leftshift("\\<\\<\\b"),
    Token_rightshift("\\>\\>\\b"),
    Token_unsignedshiftright("\\>\\>\\>\b"),
    Token_minusequal("-="),
    Token_plusequal("\\+\\="),
    Token_multiplyequal("\\*="),
    Token_divideequal("/="),
    Token_andequal("&="),
    Token_orequal("\\|="),
    Token_powerequal("\\^\\="),
    Token_percentequal("%="),
    Token_shiftleftequal("<<="),
    Token_shiftrightequal(">>="),
    Token_unsignedshiftrightequal("\\>\\>\\>\\="),
    Token_minus("-"),
    Token_plus("\\+"),
    Token_multiply("\\*\\b"),
    Token_divide("/\b"),
    Token_or("\\|\\|"),
    Token_lessorequal("\\<\\="),
    Token_greaterorequal("\\>\\="),
    Token_greater(">"),
    Token_less("\\<"),


    Token_abstract("abstract\\b"),
    Token_default("default\\b"),
    Token_if("if\\b"),
    Token_private("private\\b"),
    Token_this("this\\b"),
    Token_boolean("boolean\\b"),
    Token_do("do\\b"),
    Token_implements("implements\\b"),
    Token_protected("protected\\b"),
    Token_throw("throw\\b"),
    Token_break("break\\b"),
    Token_double("double\\b"),
    Token_import("import\\b"),
    Token_public("public\\b"),
    Token_throws("throws\\b"),
    Token_byte("byte\\b"),
    Token_else("else\\b"),
    Token_instanceof("instanceof\\b"),
    Token_return("return\\b"),
    Token_transient("transient\\b"),
    Token_case("case\\b"),
    Token_extends("extends\\b"),
    Token_int("int\\b"),
    Token_short("short\\b"),
    Token_try("try\\b"),
    Token_catch("catch\\b"),
    Token_final("final\\b"),
    Token_interface("interface\\b"),
    Token_static("static\\b"),
    Token_void("void\\b"),
    Token_char("char\\b"),
    Token_finally("finally\\b"),
    Token_long("long\\b"),
    Token_strictfp("strictfp\\b"),
    Token_volatile("volatile\\b"),
    Token_class("class\\b"),
    Token_float("float\\b"),
    Token_native("native\\b"),
    Token_super("super\\b"),
    Token_while("while\\b"),
    Token_const("const\\b"),
    Token_for("for\\b"),
    Token_new("new\\b"),
    Token_switch("switch\\b"),
    Token_continue("continue\\b"),
    Token_goto("goto\\b"),
    Token_package("package\\b"),
    Token_synchronized("synchronized\\b"),

    //Token_commentstart("\\/\\*\\b"),
    //Token_commentend("\\*\\/\\b"),
    Token_bracketopen("\\("),
    Token_bracketclose("\\)"),
    Token_curlybracketopen("\\{"),
    Token_curlybracketclose("\\}"),
    Token_semi("\\;"),
    Token_coma("\\,"),
    Token_point("\\."),
    Token_sqbracketopen("\\["),
    Token_sqbracketclose("\\]"),
    Token_bracket("\""),
    Token_minibracket("\'"),
    Token_slash("\\\\"),
    Token_comment("//"),


    Token_string ("\"[^\"]+\""),
    Token_number ("\\d+(\\.\\d+)?"),
    Token_identifier ("\\w+\\b");

    private final Pattern pattern;

    Token(String s) {
        pattern = Pattern.compile("^" + s);
    }

    int Matching(String s) {
        Matcher matcher = pattern.matcher(s);

        if (matcher.find()) {
            return matcher.end();
        }

        return -1;
    }
}