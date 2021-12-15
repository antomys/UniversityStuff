import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.channels.FileChannel;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.time.format.DateTimeFormatter;
import java.time.LocalDateTime;

public class Split_30 {
    private static final String dir = "/home/antomys/IdeaProjects/ShellSort/src/main/resources/tmp/";
    private static final String suffix = ".splitPart";
    private String path;
    public int mBperSplit;

    public Split_30 (String path) {
        this.path = path;
        File tomakedir = new File(dir);
        tomakedir.mkdir();
    }

    public List<Path> splitFile(final int mBperSplit) throws IOException {
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy/MM/dd HH:mm:ss");
        LocalDateTime now = LocalDateTime.now();
        String fileName = this.path;
        if (mBperSplit <= 0) {
            throw new IllegalArgumentException("mBperSplit must be more than zero");
        }

        List<Path> partFiles = new ArrayList<>();
        final long sourceSize = Files.size(Paths.get(fileName));
        final long bytesPerSplit = 1024L * 1024L * mBperSplit;
        final long numSplits = sourceSize / bytesPerSplit;
        final long remainingBytes = sourceSize % bytesPerSplit;
        int position = 0;
        int index = 0;
        long start = System.nanoTime();
        try (RandomAccessFile sourceFile = new RandomAccessFile(fileName, "r");
             FileChannel sourceChannel = sourceFile.getChannel()) {

            for (; position < numSplits; position++) {
                //write multipart files.
                writePartToFile(bytesPerSplit, position * bytesPerSplit, sourceChannel, partFiles, index);
                index ++;
            }

            if (remainingBytes > 0) {
                writePartToFile(remainingBytes, position * bytesPerSplit, sourceChannel, partFiles, index);
                index++;
            }
        }
        long elapsedTime = (System.nanoTime() - start)/1000000;
        FileWriter file = new FileWriter("src/main/resources/logging", true);
        file.write("Current time: " + dtf.format(now) +
                " Method: SplitFiles " + "Time: "+ elapsedTime + " ms"+"\n");
        file.close();
        return partFiles;
    }

    private static void writePartToFile(long byteSize, long position, FileChannel sourceChannel, List<Path> partFiles) throws IOException {
        Path fileName = Paths.get(dir + UUID.randomUUID() + suffix);
        //Path fileName = Paths.get(dir + UUID.randomUUID() + suffix);
        try (RandomAccessFile toFile = new RandomAccessFile(fileName.toFile(), "rw");
             FileChannel toChannel = toFile.getChannel()) {
            sourceChannel.position(position);
            toChannel.transferFrom(sourceChannel, 0, byteSize);
        }
        partFiles.add(fileName);
    }
    private static void writePartToFile(long byteSize, long position, FileChannel sourceChannel, List<Path> partFiles, int i) throws IOException {
        Path fileName = Paths.get(dir + i + suffix);
        try (RandomAccessFile toFile = new RandomAccessFile(fileName.toFile(), "rw");
             FileChannel toChannel = toFile.getChannel()) {
            sourceChannel.position(position);
            toChannel.transferFrom(sourceChannel, 0, byteSize);
        }
        partFiles.add(fileName);
    }
    public void clearTemp(String path) {
        File inputFile = new File(path);
        for(File file: inputFile.listFiles()){
            file.delete();
        }
    }
}
