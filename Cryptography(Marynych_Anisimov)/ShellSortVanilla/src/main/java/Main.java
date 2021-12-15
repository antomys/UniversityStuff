import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;
import java.time.format.DateTimeFormatter;
import java.time.LocalDateTime;

public class Main {
    public static void main(String[] args) throws Exception {
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy/MM/dd HH:mm:ss");
        long beforeUsedMem=Runtime.getRuntime().totalMemory()-Runtime.getRuntime().freeMemory();
        LocalDateTime now = LocalDateTime.now();
        String path = "/home/antomys/IdeaProjects/ShellSort/src/main/resources";
        ShellSort shellSort = new ShellSort();
        shellSort.shellFiles(path, "input100000000.txt");

        File[] file = new File("src/main/resources/sorted").listFiles();
        int K = file.length;
        Arrays.sort(file);
        int L = (int) file[0].length();
        long start = System.nanoTime();
        ArrayList<ArrayList<Integer>> inputArray = new ArrayList<>();
        ArrayList<Integer> temp = new ArrayList<>();
        for (File file1 : file) {
            Scanner scanner = new Scanner(file1);
            while (scanner.hasNext())
                temp.add(scanner.nextInt());
            inputArray.add(new ArrayList<>(temp));
            temp.clear();
        }
        MinHeap.merge(inputArray);
        long elapsedTime = (System.nanoTime() - start)/1000000;
        cleartemp(path+"/sorted");
        long afterUsedMem=(Runtime.getRuntime().totalMemory()-Runtime.getRuntime().freeMemory())/(1024 * 1024);
        FileWriter tempfile = new FileWriter("src/main/resources/logging", true);
        tempfile.write("Current time: " + dtf.format(now) +
                " Method: MergeFiles " + "Time: "+ elapsedTime + " ms" + " Memory Used: "+ afterUsedMem+ " mB "+"\n\n");
        tempfile.close();
    }

    public static void cleartemp(String path) {
        File inputFile = new File(path);
        for(File file: inputFile.listFiles()){
            file.delete();
        }
    }
}
