import java.io.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Scanner;
import java.time.format.DateTimeFormatter;
import java.time.LocalDateTime;

public class ShellSort {

    private static int getFileSizeMegaBytes(File file) {
        return (int) file.length() / (1024 * 1024);
    }

    public void shellFiles(String path, String fileName) throws IOException {
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy/MM/dd HH:mm:ss");
        LocalDateTime now = LocalDateTime.now();
        long start;
        File inputFile = new File(path+"/"+fileName);
        int mBperSplit = 1;
        int filesize = getFileSizeMegaBytes(inputFile);
        if(filesize >100) {
            mBperSplit = 100;
        } else if (filesize > 10) {
            mBperSplit = 10;
        }
        Split_30 split_30 = new Split_30(path+"/"+fileName);
        split_30.splitFile(mBperSplit);
        FileWriter fileWriter;
        File outfile = new File(path+"/sorted");
        outfile.mkdir();
        File[] infile = new File(path+"/tmp").listFiles();
        Arrays.sort(infile);
        int i = 0;
        start = System.nanoTime();
        for (File files: infile) {
            Scanner scanner = new Scanner(files);
            ArrayList<Integer> input = new ArrayList<Integer>();
            while (scanner.hasNext()) {
                input.add(scanner.nextInt());
            }

            shellSort(input);
            fileWriter = new FileWriter(path+"/sorted/"+ i +".splitPart",false);
            for (Integer integer: input) {
                fileWriter.write(integer.toString()+'\n');
            }
            i++;
            fileWriter.close();
            input.clear();
        }
        long elapsedTime = (System.nanoTime() - start)/1000000;
        split_30.clearTemp(path+ "/tmp");
        FileWriter file = new FileWriter("src/main/resources/logging", true);
        file.write("Current time: " + dtf.format(now) +
                " Method: ShellSort " + "Time: "+ elapsedTime + " ms" + " File size:" +filesize+"\n");
        file.close();
    }
    private static ArrayList<Integer> shellSort(ArrayList<Integer> arr){
        int interval = 1;
        int temp;
        // interval calculation using Knuth's interval sequence
        while(interval <= arr.size()/3){
            interval = (interval * 3) + 1;
        }
        while(interval > 0){
            for(int i = interval; i < arr.size(); i++){
                temp = arr.get(i);
                int j;
                for(j = i; j > interval - 1 && arr.get(j-interval) >= temp; j=j-interval){
                    Collections.swap(arr,j,j-interval);
                }
                arr.set(j,temp);
            }
            // reduce interval
            interval = (interval - 1)/3;
        }
        return arr;
    }
}