import java.io.*;
import java.net.URL;
import java.util.ArrayList;
import java.util.Optional;
import java.util.ResourceBundle;
import javafx.beans.binding.BooleanBinding;
import javafx.scene.Node;
import javafx.scene.control.*;
import org.apache.commons.io.FilenameUtils;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.stage.FileChooser;
import javafx.stage.Stage;
public class AddFileController implements Initializable {
    private File selectedFile;
    @FXML
    private TextArea previewbox = new TextArea();
    @FXML
    private TextField pathname;
    @FXML
    private TextField textname;
    @FXML private javafx.scene.control.Button backbutton;
    @FXML private Button uploadbutton;
    @FXML private TextArea tagtextarea;
    @FXML private TextArea anotationtextarea;
    @FXML private Label LabelError;
    Optional<String > returnvalue;
    boolean similar;
    @FXML
    public void InputFile() throws IOException {
        FileChooser fileChooser = new FileChooser();
        selectedFile = fileChooser.showOpenDialog(null);
        if (selectedFile != null) {
            textname.setText(FilenameUtils.removeExtension(selectedFile.getName()));
            pathname.setText(selectedFile.getAbsolutePath());
            String s = TxtLoader.readFile2(selectedFile.getAbsolutePath());
            previewbox.setText(s);
            ArrayList<TextInfo> textInfoArrayList = (ArrayList<TextInfo>) DbUtils.getTextInfo();
            for(TextInfo textInfo:textInfoArrayList){
                similar= textname.getText().equals(textInfo.getName());
            }
        }
        else { pathname.setText("File selection cancelled."); }
    }
    @FXML
    private void UploadFile(javafx.event.ActionEvent actionEvent){
        if (!similar) {
            DbUtils.addTextAnotationText(textname.getText(),
                    IOUtils.getStringFromFile(selectedFile.getAbsolutePath()),
                    anotationtextarea.getText(), tagtextarea.getText());
            Alert alert = new Alert(Alert.AlertType.INFORMATION);
            alert.setTitle("Information Dialog");
            alert.setHeaderText(null);
            alert.setContentText("Uploaded to DB Successfully!");
            alert.showAndWait();
            returnvalue = Optional.of(textname.getText());
        }
        else
            { Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setTitle("Error Dialog");
        alert.setHeaderText("Look, an Error Dialog");
        alert.setContentText("Unable to upload!");
        LabelError.setText("Fail! This text is already uploaded!");
        alert.showAndWait();
            }
            ((Node) (actionEvent.getSource())).getScene().getWindow().hide();
    }
    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        BooleanBinding booleanBind = tagtextarea.textProperty().isEmpty()
                .or(anotationtextarea.textProperty().isEmpty())
                .or(previewbox.textProperty().isEmpty());
        uploadbutton.disableProperty().bind(booleanBind);
    }
    public void BackMainScene() {
        Stage stage = (Stage) backbutton.getScene().getWindow();
        // do what you have to do, CLOSE Motherfucker
        returnvalue=Optional.empty();
        stage.close();
    }
    public Optional<String> getNewItem() {
        return returnvalue;
    }
}
