import java.io.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.PriorityQueue;
import java.util.Scanner;

public class PMSS
{
    static long data_read = 0;
    static String next_run_element;
    static int output_file_index;
    static int old_output_file_index;
    static int runs_per_level;
    static int missing_runs[];
    static int distribution_array[];
    static int allow_read[];
    static String last_elements[];

    static String run_last_elements[];

    static StringRecord next_run_first_elements[];
    static PriorityQueue<StringRecord> q = new PriorityQueue<StringRecord>();


    private static int getFileSizeMegaBytes(File file) {
        return (int) file.length() / (1024 * 1024);
    }

    public static void main(String args[]) throws Exception
    {
        long beforeUsedMem=Runtime.getRuntime().totalMemory()-Runtime.getRuntime().freeMemory();
        int temp_files = 24;
        String file_extension = ".splitPart";
        String working_dir = "src/main/resources/";
        File main_file = new File(working_dir + "20000000.txt"); //TODO: CHANGE FILENAME AND DIRECTORY
        File sorted_file = new File(working_dir + "/sorted.txt");
        BufferedReader main_file_reader = new BufferedReader(new FileReader(main_file));
        long main_file_length = main_file.length();

        if (getFileSizeMegaBytes(main_file)>100)
            temp_files = 50;
        else if(getFileSizeMegaBytes(main_file)<10)
            temp_files = 2;



        File working_files[] = new File[temp_files + 1];

        sorted_file.delete();

        allow_read = new int[temp_files + 1];
        missing_runs = new int[temp_files + 1];
        last_elements = new String[temp_files + 1];
        distribution_array = new int[temp_files + 1];
        run_last_elements = new String[temp_files + 1];
        next_run_first_elements = new StringRecord[temp_files + 1];

        String working_file_name = "working_file_";
        BufferedReader run_file_readers[] = new BufferedReader[temp_files + 1];

        for(int i=0; i<working_files.length; i++)
        {
            working_files[i] = new File(working_dir + working_file_name + (i+1) + file_extension);
        }

        /* START - initial run distribution */

        distribute(temp_files, working_files, main_file_length, main_file_reader);

        /* END - initial run distribution */

        /* START - polyphase merge */

        long start = System.currentTimeMillis();

        int min_dummy_values = getMinDummyValue();
        initMergeProcedure(min_dummy_values);
        BufferedWriter writer = new BufferedWriter(new FileWriter(working_files[output_file_index]));

        for(int i=0; i<run_file_readers.length-1; i++)
        {
            run_file_readers[i] = new BufferedReader(new FileReader(working_files[i]));
        }

        while(runs_per_level > 1)
        {
            last_elements[output_file_index] = null;
            merge(distribution_array[getMinFileIndex()] - min_dummy_values, run_file_readers, writer);

            setPreviousRunDistributionLevel();
            updateOutputFileIndex();
            resetAllowReadArray();

            min_dummy_values = getMinDummyValue();
            writer = new BufferedWriter(new FileWriter(working_files[output_file_index]));
            run_file_readers[old_output_file_index] = new BufferedReader(new FileReader(working_files[old_output_file_index]));
        }

        writer.close();
        main_file_reader.close();
        closeReaders(run_file_readers);
        clearTempFiles(working_dir, main_file, working_files);

        long end = System.currentTimeMillis();
        /* END - polyphase merge */
        long afterUsedMem=Runtime.getRuntime().totalMemory()-Runtime.getRuntime().freeMemory();
        long actualMemUsed=afterUsedMem-beforeUsedMem;

        System.out.println("Merge phase done in " + (end-start) + " ms");
        System.out.println("Memory used: "+actualMemUsed/(1024L * 1024L));
    }

    private static void distribute(int temp_files, File working_files[], long main_file_length, BufferedReader main_file_reader)
    {
        try
        {
            long start = System.currentTimeMillis();

            runs_per_level = 1;
            distribution_array[0] = 1;
            output_file_index = working_files.length - 1;

            int write_sentinel[] = new int[temp_files];
            BufferedWriter run_file_writers[] = new BufferedWriter[temp_files];

            for(int i=0; i<temp_files; i++)
            {
                run_file_writers[i] = new BufferedWriter(new FileWriter(working_files[i],false));
            }

            /* START - distribute runs */
            while(data_read < main_file_length)
            {
                for(int i=0; i<temp_files; i++)
                {
                    while(write_sentinel[i] != distribution_array[i])
                    {
                        while(runsMerged(main_file_length, i, next_run_element))
                        {
                            writeNextStringRun(main_file_length, main_file_reader, run_file_writers[i], i);
                        }

                        writeNextStringRun(main_file_length, main_file_reader, run_file_writers[i], i);
                        missing_runs[i]++;
                        write_sentinel[i]++;
                    }
                }
                setNextDistributionLevel();
            }

            closeWriters(run_file_writers);

            //Begin of Shell Sorting Files

            File dir = new File("src/main/resources");
            File[] files = dir.listFiles((dir1, name) -> name.toLowerCase().startsWith("working"));
            Arrays.sort(files);
            for(File file: files) {
                //int sortedFileIndex = 1;
                Scanner scanner = new Scanner(file);
                ArrayList<String> strings = new ArrayList<>();
                while(scanner.hasNext()) {
                    strings.add(scanner.next());
                }
                String[] tempsArray = strings.toArray(new String[0]);
                ShellSort.sort(tempsArray);

                FileWriter fileWriter = new FileWriter("src/main/resources/tmp/"+file.getName(),false);
                for (String aString: tempsArray) {
                    fileWriter.write(aString+'\n');
                }
                //sortedFileIndex++;
                fileWriter.close();
                strings.clear();
            }


            long end = System.currentTimeMillis();
            System.out.println("Distribute phase done in " + (end-start) + " ms");

            setPreviousRunDistributionLevel();
            setMissingRunsArray();



            /* END - distribute runs */
        }

        catch(Exception e)
        {
            System.out.println("Exception thrown in distribute(): " + e.getMessage());
        }
    }

    private static void initMergeProcedure(int min_dummy)
    {
        try
        {
            for(int i=0; i<missing_runs.length - 1; i++)
            {
                missing_runs[i] -= min_dummy;
            }

            missing_runs[output_file_index] += min_dummy;

            resetAllowReadArray();
        }

        catch(Exception e)
        {
            System.out.println("Exception thrown in initMergeProcedure(): " + e.getMessage());
        }
    }

    private static void merge(int min_file_values, BufferedReader run_file_readers[], BufferedWriter writer)
    {
        String line;
        int min_file;
        int heap_empty = 0;
        StringRecord record;

        /* Initial heap population */
        populateHeap(run_file_readers);

        try
        {
            while(heap_empty != min_file_values)
            {
                record = q.poll();
                writer.write(record.getValue() + "\n");
                min_file = record.getFileIndex();

                if(allow_read[min_file] == 1 && (line = readFileLine(run_file_readers[min_file],min_file)) != null)
                {
                    q.add(new StringRecord(line,min_file));
                }

                try
                {
                    /* Once heap is empty all n-th runs have merged */
                    if(q.size() == 0)
                    {
                        heap_empty++;

                        for(int i=0; i<next_run_first_elements.length; i++)
                        {
                            try
                            {
                                q.add(new StringRecord(next_run_first_elements[i].getValue(),i));
                                last_elements[i] = next_run_first_elements[i].getValue();
                            }
                            catch(Exception e){}
                        }

                        populateHeap(run_file_readers);
                        resetAllowReadArray();

                        if(heap_empty == min_file_values)
                        {
                            writer.close();
                            return;
                        }
                    }
                }
                catch(Exception e){}
            }
        }
        catch(Exception e)
        {
            System.out.println("Exception thrown in merge(): " + e.getMessage());
        }
    }

    private static void updateOutputFileIndex()
    {
        if(output_file_index > 0)
        {
            output_file_index--;
        }
        else
        {
            output_file_index = distribution_array.length - 1;
        }
    }

    private static void populateHeap(BufferedReader run_file_readers[])
    {
        try
        {
            String line;

            for(int i=0;i<run_file_readers.length;i++)
            {
                if(missing_runs[i] == 0)
                {
                    if((allow_read[i] == 1) && (line = readFileLine(run_file_readers[i],i)) != null)
                    {
                        q.add(new StringRecord(line,i));
                    }
                }

                else
                {
                    missing_runs[i]--;
                }
            }
        }
        catch(Exception e)
        {
            System.out.println("Exception thrown while initial heap population: " + e.getMessage());
        }
    }

    private static String readFileLine(BufferedReader file_reader, int file_index)
    {
        try
        {
            String current_line = file_reader.readLine();

            /* End of run */
            if(last_elements[file_index] != null && current_line.compareTo(last_elements[file_index]) < 0)
            {
                next_run_first_elements[file_index] = new StringRecord(current_line,file_index);
                allow_read[file_index] = 0;

                return null;
            }

            else
            {
                last_elements[file_index] = current_line;
                return current_line;
            }
        }
        catch(Exception e)
        {
            allow_read[file_index] = 0;
            next_run_first_elements[file_index] = null;
        }

        return null;
    }

    private static void resetAllowReadArray()
    {
        for(int i=0; i<allow_read.length; i++)
        {
            allow_read[i] = 1;
        }

        allow_read[output_file_index] = 0;
    }

    private static int getMinDummyValue()
    {
        int min = Integer.MAX_VALUE;

        for(int i=0; i<missing_runs.length; i++)
        {
            if(i != output_file_index && missing_runs[i] < min)
            {
                min = missing_runs[i];
            }
        }

        return min;
    }

    private static int getMinFileIndex()
    {
        int min_file_index = -1;
        int min = Integer.MAX_VALUE;

        for(int i=0; i<distribution_array.length; i++)
        {
            if(distribution_array[i] != 0 && distribution_array[i] < min)
            {
                min_file_index = i;
            }
        }

        return min_file_index;
    }

    private static void writeNextStringRun(long main_file_length, BufferedReader main_file_reader, BufferedWriter run_file_writer, int file_index)
    {
        if(data_read >= main_file_length)
        {
            missing_runs[file_index]--;
            return;
        }

        try
        {
            if(next_run_element != null)
            {
                run_file_writer.write(next_run_element + "\n");
                data_read += next_run_element.length() + 1;
            }

            String min_value = "";
            String current_line = main_file_reader.readLine();

            /* Case if run is a single element: acordingly update variables and return */
            if(next_run_element != null)
            {
                if(next_run_element.compareTo(current_line) > 0)
                {
                    run_last_elements[file_index] = next_run_element;
                    next_run_element = current_line;

                    return;
                }
            }

            while(current_line !=  null)
            {
                if(current_line.compareTo(min_value) >= 0)
                {
                    run_file_writer.write(current_line + "\n");
                    data_read += current_line.length() + 1;

                    min_value = current_line;
                    current_line = main_file_reader.readLine();
                }

                else
                {
                    next_run_element = current_line;
                    run_last_elements[file_index] = min_value;

                    return;
                }
            }
        }
        catch(Exception e){}
    }

    private static boolean runsMerged(long main_file_length, int file_index, String first_element)
    {
        if(data_read < main_file_length && run_last_elements[file_index] != null && first_element != null)
        {
            return run_last_elements[file_index].compareTo(first_element) <= 0 ? true:false;
        }
        return false;
    }

    /**
     * Calculates next run distribution level. The new level is calculated following
     * Finonacchi sequence rule.
     */
    private static void setNextDistributionLevel()
    {
        runs_per_level = 0;
        int current_distribution_array[] = distribution_array.clone();

        for(int i=0; i<current_distribution_array.length - 1; i++)
        {
            distribution_array[i] = current_distribution_array[0] + current_distribution_array[i+1];
            runs_per_level +=  distribution_array[i];
        }
    }

    private static void setPreviousRunDistributionLevel()
    {
        int diff;
        int current_distribution_array[] = distribution_array.clone();
        int last = current_distribution_array[current_distribution_array.length - 2];

        old_output_file_index = output_file_index;

        runs_per_level = 0;
        runs_per_level += last;
        distribution_array[0] = last;

        for(int i=current_distribution_array.length - 3; i>=0; i--)
        {
            diff = current_distribution_array[i] - last;
            distribution_array[i+1] = diff;
            runs_per_level += diff;
        }
    }

    /**
     * Calculates the amount of dummy runs for every input file after distribute phase of the algorithm.
     */
    private static void setMissingRunsArray()
    {
        for(int i=0; i<distribution_array.length - 1; i++)
        {
            missing_runs[i] = (distribution_array[i] - missing_runs[i]);
        }
    }

    private static void closeReaders(BufferedReader run_file_readers[])
    {
        try
        {
            for(int i=0; i<run_file_readers.length; i++)
            {
                run_file_readers[i].close();
            }
        }

        catch(Exception e){}
    }

    private static void closeWriters(BufferedWriter run_file_writers[])
    {
        try
        {
            for(int i=0; i<run_file_writers.length; i++)
            {
                run_file_writers[i].close();
            }
        }

        catch(Exception e){}
    }

    private static void clearTempFiles(String working_dir, File main_file, File temp_files[])
    {
        File sorted_file = new File(working_dir+"sorted.txt");

        for(int i=0; i<temp_files.length; i++)
        {
            if(temp_files[i].length() == main_file.length())
            {
                temp_files[i].renameTo(sorted_file);
                temp_files[i].delete();
            }

            temp_files[i].delete();
        }
    }
}
