import javax.swing.plaf.synth.SynthDesktopIconUI;
import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

public class Lab2 {
    public static void main(String[] args) throws IOException {
        //заповнення автомату А
        List<String> linesA = Files.readAllLines(Paths.get("inputA.txt"), Charset.defaultCharset());
        int nA = Integer.parseInt(linesA.get(0));
        String[][] matrA = new String[nA][nA];
        for (int i = 0; i < nA; i++)
            for (int j = 0; j < nA; j++)
                matrA[i][j] = "-";
        int startA = Integer.parseInt(linesA.get(1));
        ArrayList<Integer> finalA = new ArrayList<>();
        for (String s : linesA.get(2).split(" ")){
            finalA.add(Integer.parseInt(s));
        }
        int x;
        int y;
        for (int i = 3; i < linesA.size(); i++){
            String[] split = linesA.get(i).split(" ");
            x = Integer.parseInt(split[0]);
            y = Integer.parseInt(split[2]);
            matrA[x][y] = split[1];
        }

        //заповнення автомату В
        List<String> linesB = Files.readAllLines(Paths.get("inputB.txt"), Charset.defaultCharset());
        int nB = Integer.parseInt(linesB.get(0));
        String[][] matrB = new String[nB][nB];
        for (int i = 0; i < nB; i++)
            for (int j = 0; j < nB; j++)
                matrB[i][j] = "-";
        int startB = Integer.parseInt(linesB.get(1));
        ArrayList<Integer> finalB = new ArrayList<>();
        for (String s : linesB.get(2).split(" ")){
            finalB.add(Integer.parseInt(s));
        }
        for (int i = 3; i < linesB.size(); i++){
            String[] split = linesB.get(i).split(" ");
            x = Integer.parseInt(split[0]);
            y = Integer.parseInt(split[2]);
            matrB[x][y] = split[1];
        }

        ArrayList<String> wordsA = new ArrayList<>();
        wordsA.add("");
        ArrayList<Integer> headsA = new ArrayList<>();
        headsA.add(startA);
        ArrayList<String> finalWordsA = new ArrayList<>();
        ArrayList<String> wordsB = new ArrayList<>();
        wordsB.add("");
        ArrayList<Integer> headsB = new ArrayList<>();
        headsB.add(startB);
        ArrayList<String> finalWordsB = new ArrayList<>();

        String bufWord = "";
        int bufHead = 0;
        ArrayList<String> bufWords = new ArrayList<>();
        ArrayList<Integer> bufHeads = new ArrayList<>();

        boolean cont = true;
        int N = nsk(nA-1, nB-1);
        if (finalA.contains(startA) && finalB.contains(startB)){
            cont = false;
            finalWordsA.add("");
            finalWordsB.add("");
        }
        if (finalA.contains(startA)){
            finalWordsA.add("");
        }
        if (finalB.contains(startB)){
            finalWordsB.add("");
        }
        //
        //TODO START THERE
        //
        for (int k = 0; k < N && cont; k++){
            bufWords.clear();
            bufHeads.clear();
            for (int i = 0; i < headsA.size(); i++){
                bufWord = wordsA.get(i);
                bufHead = headsA.get(i);
                for (int j = 0; j < nA; j++){
                    if (!matrA[bufHead][j].equals("-")){
                        for (String s : matrA[bufHead][j].split(",")){
                            if (finalA.contains(j)){
                                finalWordsA.add(bufWord+s);
                            }else{
                                bufWords.add(bufWord+s);
                                bufHeads.add(j);
                            }
                        }
                    }
                }
            }
            wordsA.clear();
            headsA.clear();
            wordsA.addAll(bufWords);
            headsA.addAll(bufHeads);

            bufWords.clear();
            bufHeads.clear();
            for (int i = 0; i < headsB.size(); i++){
                bufWord = wordsB.get(i);
                bufHead = headsB.get(i);
                for (int j = 0; j < nB; j++){
                    if (!matrB[bufHead][j].equals("-")){
                        for (String s : matrB[bufHead][j].split(",")){
                            if (finalB.contains(j)){
                                finalWordsB.add(bufWord+s);
                            }else{
                                bufWords.add(bufWord+s);
                                bufHeads.add(j);
                            }
                        }
                    }
                }
            }
            wordsB.clear();
            headsB.clear();
            wordsB.addAll(bufWords);
            headsB.addAll(bufHeads);

            if (wordsA.size() == 0 && wordsB.size() == 0){
                cont = false;
            }
                /*
                if (cont){
                    bufWords.clear();
                    bufHeads.clear();
                    for (int i = 0; i < wordsA.size(); ++i){
                        if (wordsB.contains(wordsA.get(i))){
                            bufWords.add(wordsA.get(i));
                            bufHeads.add(headsA.get(i));
                        }
                    }
                    wordsA.clear();
                    headsA.clear();
                    wordsA.addAll(bufWords);
                    headsA.addAll(bufHeads);

                    bufWords.clear();
                    bufHeads.clear();
                    for (int i = 0; i < wordsB.size(); ++i){
                        if (wordsA.contains(wordsB.get(i))){
                            bufWords.add(wordsB.get(i));
                            bufHeads.add(headsB.get(i));
                        }
                    }
                    wordsB.clear();
                    headsB.clear();
                    wordsB.addAll(bufWords);
                    headsB.addAll(bufHeads);


                }
                else{
                    System.out.println("there are next min words:");
                    for (String s : res){
                        System.out.println(s);
                    }
                }*/
        }

        if (finalWordsA.size() == 0 || finalWordsB.size() == 0)
        {
            System.out.println("There are no min words");
        }
        else
        {
            ArrayList<String> res = new ArrayList<>();
            int min = N;
            for(String s : finalWordsA){
                if (finalWordsB.contains(s) && !res.contains(s))
                {
                    if (s.length() == min){
                        res.add(s);
                    }
                    if (s.length() < min){
                        res.clear();
                        res.add(s);
                        min = s.length();
                    }
                }
            }
            if (res.size() == 0){
                System.out.println("There are no min words");
            } else {
                System.out.println("There are next min words:");
                for (String s : res){
                    System.out.println("\""+s+"\"");
                }
            }
        }
    }

    private static int nsk(int a, int b){
        if (a < 1) a = 1;
        if (b < 1) b = 1;
        int res = a * b;
        while (a != b) {
            if (a > b) a -= b;
            else b -= a;
        }
        return res / a;
    }
}
