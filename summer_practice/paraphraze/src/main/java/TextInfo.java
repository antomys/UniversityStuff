import java.util.ArrayList;
import java.util.List;

public class TextInfo {
    int id;
    String name;
    String text;
    List<String> annotations = new ArrayList<>();
    List<String> tags = new ArrayList<>();
    String replaced;

    @Override
    public String toString() {
        return "TextInfo{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", text='" + text + '\'' +
                ", annotations=" + annotations +
                ", tags=" + tags +
                '}';
    }
    public String getName() { return name; }
    public String getText(){
        return text;
    }
    public String getAnnotation(){
        Replace(annotations);
        return replaced;
    }
    public String getTags(){
        Replace(tags);
        return replaced;
    }
    private String Replace(List<String> list){
        for(String items:list){
            replaced=items.replace('[','"');
        }
        return replaced;
    }


}
