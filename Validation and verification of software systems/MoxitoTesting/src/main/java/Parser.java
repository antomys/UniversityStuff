
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.HashSet;
import java.util.Set;
import java.util.stream.Stream;

public class Parser implements IParser {
    private StringBuilder file = new StringBuilder();
    private Token token;
    private String lexeme;
    public boolean has = false;
    private String ErrorMessage = "";
    private static Set<Character> blanks = new HashSet<Character>();

    public Parser(String filePath) {
        try (Stream<String> stream = Files.lines(Paths.get(filePath))) {
            stream.forEach(file::append);
        } catch (IOException ex) {
            has = true;
            ErrorMessage = "Could not read file: " + filePath;
            return;
        }

        blanks.add('\r');
        blanks.add('\n');
        blanks.add((char) 32);
    }
    public Parser(StringBuilder text){
        this.file=text;
    }

    @Override
    public void ReadNextToken() {
        if (has) {
            return;
        }

        if (file.length() == 0) {
            has = true;
            return;
        }

        DeleteNextSpaces();

        if (FindToken()) {
            if (file.length() == 0) {
                has = true;
                return;
            }
            return;
        }

        has = true;

        if (file.length() > 0) {
            ErrorMessage = "Error: unexpected symbol: '" + file.charAt(0) + "'";
        }
    }

    @Override
    public void DeleteNextSpaces() {
        int iter = 0;

        while (blanks.contains(file.charAt(iter))) {
            iter++;
        }

        if (iter > 0) {
            file.delete(0, iter);
        }
    }

    @Override
    public boolean FindToken() {
        for (Token t : Token.values()) {
            int end = t.Matching(file.toString());

            if (end != -1) {
                token = t;
                lexeme = file.substring(0, end);
                file.delete(0, end);
                return true;
            }
        }

        return false;
    }

    @Override
    public Token CurrentToken() {
        return token;
    }

    @Override
    public String CurrentLexeme() {
        return lexeme;
    }

    @Override
    public boolean IsSuccessful() {
        return ErrorMessage.isEmpty();
    }

    @Override
    public String ErrorMessage() {
        return ErrorMessage;
    }

    @Override
    public boolean HasNext() {
        return has;
    }
}