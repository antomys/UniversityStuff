package Controllers;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.ChoiceBox;

import java.io.IOException;
import java.net.URL;
import java.security.GeneralSecurityException;
import java.util.Arrays;
import java.util.ResourceBundle;

import com.google.api.client.googleapis.javanet.GoogleNetHttpTransport;
import com.google.api.client.json.gson.GsonFactory;
import com.google.api.services.translate.Translate;
import com.google.api.services.translate.model.TranslationsListResponse;
import com.google.api.services.translate.model.TranslationsResource;
import javafx.scene.control.TextArea;


public class MainSceneController implements Initializable {
    ObservableList<String> langs = FXCollections.observableArrayList(
            "RU","de","zh","EN","UK", "ceb");
    @FXML ChoiceBox langChoiceBox;
    @FXML
    TextArea initTextArea;
    @FXML TextArea procTextArea;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        langChoiceBox.setItems(langs);
    }



    public void process() throws GeneralSecurityException, IOException {
        Translate translate= new Translate.Builder(
                GoogleNetHttpTransport.newTrustedTransport()
                , GsonFactory.getDefaultInstance(), null)
                .setApplicationName("DemoTranslateAPI")
                .build();
        procTextArea.setWrapText(true);
        initTextArea.setWrapText(true);
        Translate.Translations.List list = translate.new Translations().list(
                Arrays.asList(initTextArea.getText()), (String) langChoiceBox.getValue()
        );

        list.setKey(""); //TODO: API KEY
        TranslationsListResponse response = list.execute();
        for (TranslationsResource translationsResource : response.getTranslations())
        {
            procTextArea.setText(translationsResource.getTranslatedText());
        }
    }
}
