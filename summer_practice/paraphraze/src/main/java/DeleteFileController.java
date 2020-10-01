import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.control.Alert;
import javafx.scene.control.ComboBox;
import javafx.stage.Stage;
import java.net.URL;
import java.util.ArrayList;
import java.util.Optional;
import java.util.ResourceBundle;

public class DeleteFileController implements Initializable {
    @FXML private javafx.scene.control.Button backbutton;
    @FXML private ComboBox<String> textcombobox ;
    @FXML private javafx.scene.control.TextArea previewtextarea;
    public ArrayList<TextInfo> textInfoArrayList;
    Optional<String> returnValue;


    public void DeleteText(ActionEvent actionEvent) {
        DbUtils.removeText(textcombobox.getSelectionModel().getSelectedItem());
        if(previewtextarea.getText() != null) {
            DbUtils.removeText(textcombobox.getSelectionModel().getSelectedItem());
            UpdateCombobox();
            returnValue = Optional.of("Success");
            Alert alert = new Alert(Alert.AlertType.INFORMATION);
            alert.setTitle("Information Dialog");
            alert.setHeaderText(null);
            alert.setContentText("Removed Successfully!");
            alert.showAndWait();

        }
        else {
            Alert alert = new Alert(Alert.AlertType.ERROR);
            alert.setTitle("Error Dialog");
            alert.setHeaderText("Look, an Error Dialog");
            alert.setContentText("Unable to load to DB!");

            alert.showAndWait();

        }
        ((Node)(actionEvent.getSource())).getScene().getWindow().hide();
    }

    public void GoBackScene() {
        Stage stage = (Stage) backbutton.getScene().getWindow();
        returnValue = Optional.empty();
        // do what you have to do, CLOSE Motherfucker
        stage.close();
    }
    @FXML
    void Select() throws NullPointerException{
        String s = textcombobox.getSelectionModel().getSelectedItem();
        for(TextInfo textInfo:textInfoArrayList){
            if(s.equals(textInfo.getName())){
                previewtextarea.setText(textInfo.getAnnotation());
                break;
            }
        }


    }
    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        UpdateCombobox();


    }
    void UpdateCombobox(){
        ArrayList<String> Names = new ArrayList<>();
        textInfoArrayList = (ArrayList<TextInfo>) DbUtils.getTextInfo();
        for(TextInfo items : textInfoArrayList){
            Names.add(items.getName());
        }
        final ObservableList<String> options = FXCollections.observableArrayList(Names);
        textcombobox.setItems(options);

    }

    public Optional<String> getNewItem() { return  returnValue;
    }
}
