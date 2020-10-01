import java.util.*;

public class Main {
    public static void main(String[] args) {
        //System.out.println(DbUtils.getTextInfo());
       // DbUtils.addText("tygrolovu", IOUtils.getStringFromFile("src/main/resources/tygrolovu.txt"));
        /*boolean similar = false;
        ArrayList<TextInfo> textInfoArrayList = (ArrayList<TextInfo>) DbUtils.getTextInfo();
        for(TextInfo textInfo:textInfoArrayList){
            similar= "tygrolovu-paraphraz".equals(textInfo.getName());
        }
        System.out.println(similar);*/
        System.out.println(System.getProperty("user.dir")+"\\synsets_ua.db");
    }
}
