public class StringRecord implements Comparable<StringRecord>
{
    private String value;
    private int file_index;
    public StringRecord(String value, int file_index)
    {
        this.value = value;
        this.file_index = file_index;
    }
    public String getValue()
    {
        return value;
    }
    public int getFileIndex()
    {
        return file_index;
    }
    public int compareTo(StringRecord string_record)
    {
        return this.value.compareTo(string_record.value);
    }
}
