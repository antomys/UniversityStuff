import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.stream.Collectors;

public class IOUtils {

    public static String getStringFromFile(String path) {
        try {
            return Files.readString(Path.of(path));
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static List<String> getWords(String str) {
        Scanner scanner = new Scanner(str);
        List<String> words = new ArrayList<>();
        while (scanner.hasNext()) {
            words.add(scanner.next().toLowerCase());
        }
        return words;
    }

    public static List<String> getFilesInDir(String dir) {
        try {
            return Files.walk(Paths.get(dir))
                    .filter(Files::isRegularFile)
                    .map(Path::toString)
                    .collect(Collectors.toList());
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static List<String> getWordsFromFile(String filePath) {
        return getWords(getStringFromFile(filePath));
    }
}
