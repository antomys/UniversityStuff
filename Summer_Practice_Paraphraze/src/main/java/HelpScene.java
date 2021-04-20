import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.TextArea;

import java.net.URL;
import java.util.ResourceBundle;

public class HelpScene implements Initializable {
    @FXML private TextArea infotextarea;

    @FXML private TextArea usagetextarea;

    @FXML private TextArea authorcredits;


    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        infotextarea.setText("This programme is tend to look for paraphrazes in text. \n" +
                "User imports text to database and then compares it with other texts in Database\n");
        usagetextarea.setText("To use this programme you have to do:\n" +
                "1. Click button Add"+ "to pop up add window. There you need to choose path to your file\n" +
                "And import it(Don't forget about file name, tags and annotations!) Without this you could not" +
                "add new text. After this, proceed to 2.\n" +
                "2. Now your text is in DataBase. You need to choose it and press button Algorithm"+"for it to process and find similar text\n" +
                "3. To delete, click button Delete to pop up deletion window.\n" +
                "Now select your text and click button Delete. You did it!\n" +
                "That is it, you have managed to cope with this programme!");
        authorcredits.setText("Authors:\n" +
                "Main logic: Marko Yerkovic: 'https://github.com/YerkovichM'\n" +
                "Interface + interlogic: Ihor Volokhovych: 'https://github.com/antomys\n" +
                "Database and communication: Yulia Kruglyak\n" +
                "Main support and theoretical information:Maksym Shevchenko");
    }
}
