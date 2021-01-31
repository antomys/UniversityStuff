package afd.util;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

public class Util {
    public static void writeFile(String pathFile, String content) throws IOException {
        File file = new File(pathFile);
        FileWriter fw = new FileWriter(file);
        fw.write(content);
        fw.flush();
        fw.close();
    }
}