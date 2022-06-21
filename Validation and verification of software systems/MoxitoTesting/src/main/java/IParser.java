public interface IParser {
    void ReadNextToken();

    void DeleteNextSpaces();

    boolean FindToken();

    Token CurrentToken();

    String CurrentLexeme();

    boolean IsSuccessful();

    String ErrorMessage();

    boolean HasNext();

}