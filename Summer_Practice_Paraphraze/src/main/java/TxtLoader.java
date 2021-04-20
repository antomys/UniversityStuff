import java.io.*;
import java.nio.charset.StandardCharsets;

public class TxtLoader {

    static BufferedReader br;
    public static String readFile2(String file) throws IOException {

        StringBuilder ss = new StringBuilder();
        br = new BufferedReader(new InputStreamReader(new FileInputStream(file), StandardCharsets.UTF_8));
        int a; char[] buf = new char[1024];

        while (true){
            a = br.read(buf);
            if (a==-1) {br.close(); break;}
            if (a==1024) {ss.append(buf);} else {ss.append(buf, 0, a);}
        }
        return ss.substring(0);
    }
}