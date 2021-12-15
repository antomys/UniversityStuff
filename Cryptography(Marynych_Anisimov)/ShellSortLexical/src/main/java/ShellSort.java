import OldClasses.Split_30;

import java.io.File;
import java.io.FileWriter;
import java.util.*;

public class ShellSort {

    private ShellSort() { }

     //Rearranges the array in ascending order, using the natural order.
    public static void sort(Comparable[] a) {
        int n = a.length;

        // 3x+1 increment sequence:  1, 4, 13, 40, 121, 364, 1093, ...
        int h = 1;
        while (h < n/3) h = 3*h + 1;

        while (h >= 1) {
            // h-sort the array
            for (int i = h; i < n; i++) {
                for (int j = i; j >= h && less(a[j], a[j-h]); j -= h) {
                    exch(a, j, j-h);
                }
            }
            assert isHsorted(a, h);
            h /= 3;
        }
        assert isSorted(a);
    }

     //Helper sorting functions.

    // is v < w ?
    private static boolean less(Comparable v, Comparable w) {
        return v.compareTo(w) < 0;
    }

    // exchange a[i] and a[j]
    private static void exch(Object[] a, int i, int j) {
        Object swap = a[i];
        a[i] = a[j];
        a[j] = swap;
    }



     //Check if array is sorted - useful for debugging.

    private static boolean isSorted(Comparable[] a) {
        for (int i = 1; i < a.length; i++)
            if (less(a[i], a[i-1])) return false;
        return true;
    }

    // is the array h-sorted?
    private static boolean isHsorted(Comparable[] a, int h) {
        for (int i = h; i < a.length; i++)
            if (less(a[i], a[i-h])) return false;
        return true;
    }

    // print array to standard output
    private static void show(Comparable[] a) {
        for (int i = 0; i < a.length; i++) {
            System.out.println(a[i]);
        }
    }

    public static void ShellSortFiles(String path, String fileName) throws Exception {
        Split_30 split_30 = new Split_30(path+"/"+fileName);
        split_30.splitFile(1);

        FileWriter fileWriter;
        File outfile = new File(path+"/sorted");
        outfile.mkdir();
        File[] infile = new File(path+"/tmp").listFiles();
        Arrays.sort(infile);
        int sortedFileIndex = 0;
        for (File file: infile) {
            Scanner scanner = new Scanner(file);
            List<String> temps = new LinkedList<String>();
            while(scanner.hasNext()) {
                temps.add(scanner.next());
            }
            String[] tempsArray = temps.toArray(new String[0]);
            ShellSort.sort(tempsArray);

            fileWriter = new FileWriter(path+"/sorted/"+ sortedFileIndex +".splitPart",false);
            for (String aString: tempsArray) {
                fileWriter.write(aString+'\n');
            }
            sortedFileIndex++;
            fileWriter.close();
            temps.clear();
        }

    }

      //Reads in a sequence of strings from standard input; Shellsorts them;
      //and prints them to standard output in ascending order.

    public static void main(String[] args) throws Exception {
        String path = "src/main/resources";
        ShellSortFiles(path,"test.txt");
    }

}