import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.stream.Stream;

public class FileReader {
    public StringBuilder file = new StringBuilder();
    public FileReader(String path) throws IOException {
        try (Stream<String> stream = Files.lines(Paths.get(path))) {
            stream.forEach(file::append);
        } catch (IOException ex) {
        }
    }
}
