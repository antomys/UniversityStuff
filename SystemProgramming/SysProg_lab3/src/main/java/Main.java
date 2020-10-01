import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;

import javafx.geometry.HPos;
import javafx.geometry.Insets;
import javafx.scene.Group;

import javafx.scene.Scene;
import javafx.scene.control.Button;

import javafx.scene.control.TextArea;

import javafx.scene.layout.GridPane;
import javafx.scene.paint.Color;

import javafx.scene.web.HTMLEditor;
import javafx.stage.Stage;


import java.io.FileWriter;


public class Main extends Application {

    public void start(Stage primaryStage) {
        primaryStage.setTitle("JavaParser");
        Group root = new Group();
        Scene scene = new Scene(root, 1520, 900, Color.WHITE);

        GridPane gridpane = new GridPane();
        gridpane.setPadding(new Insets(5));
        gridpane.setHgap(10);
        gridpane.setVgap(10);
        final TextArea textEditor = new TextArea();
        final HTMLEditor textField = new HTMLEditor();
        textEditor.setPrefHeight(830);
        textEditor.setPrefWidth(750);
        textField.setPrefHeight(830);
        textField.setPrefWidth(750);


        textField.setStyle(
                "-fx-font: 14 cambria;"

        );
        GridPane.setHalignment(textEditor, HPos.LEFT);
        GridPane.setHalignment(textField, HPos.RIGHT);
        gridpane.add(textEditor, 0, 1);
        gridpane.add(textField, 1, 1);


        final Button btn = new Button("Parse");
        btn.setLayoutX(10);
        btn.setLayoutY(10);
        btn.setOnAction(new EventHandler<ActionEvent>() {

            public void handle(ActionEvent event) {

                textField.setHtmlText("<body><font face=\"Lucida Sans\">" + getParsedText(textEditor.getText()) + "</font face></body></html>");


            }
        });


        gridpane.getChildren().add(btn);
        root.getChildren().add(gridpane);
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    private String getParsedText(String str) {
        try {


            String path = "test.txt";
            FileWriter writer = new FileWriter(path);
            writer.write(str);
            writer.close();
            Scanner scanner = new Scanner(new java.io.FileReader(path));
            StringBuilder res = new StringBuilder();
            while (true) {
                JavaSymbol jSym = scanner.yylex();
                if (jSym.getLex() == Lexem.comment) {
                    res.append("<span style='color:#008000'>");
                    for (int i = 0; i < jSym.str.length(); i++) {
                        if (jSym.str.charAt(i) == '\n') res.append("<br>");
                        else if (jSym.str.charAt(i) == '\t') res.append("&nbsp;&nbsp;&nbsp;");
                        else if (jSym.str.charAt(i) == ' ') res.append("&nbsp;");
                        else res.append(jSym.str.charAt(i));
                    }
                    res.append("</span>");
                }
                //іваівафіва фі афіва фів а
                /*ів


                ва
                 */
                if (jSym.getLex() == Lexem.identifier)
                    res.append("<span style='color:#001000'>").append(jSym.str).append("</span>");
                if (jSym.getLex() == Lexem.keyword)
                    res.append("<span style='color:#808000'>").append(jSym.str).append("</span>");
                if (jSym.getLex() == Lexem.types)
                    res.append("<span style='color:#800080'>").append(jSym.str).append("</span>");
                if (jSym.getLex() == Lexem.number)
                    res.append("<span style='color:#000080 '>").append(jSym.str).append("</span>");
                if (jSym.getLex() == Lexem.string) {
                    res.append("<span style='color:008000'>");
                    for (int i = 0; i < jSym.str.length(); i++) {

                        if (jSym.str.charAt(i) == ' ') {
                            res.append("&nbsp;");
                        } else if (jSym.str.charAt(i) == '\t') res.append("&nbsp;&nbsp;&nbsp;");
                        else res.append(jSym.str.charAt(i));
                    }
                    res.append("</span>");
                }
                if (jSym.getLex() == Lexem.separator)
                    res.append("<span style='color:#800000'>").append(jSym.str).append("</span>");
                if (jSym.getLex() == Lexem.operator)
                    res.append("<span style='color:SlateGray'>").append(jSym.str).append("</span>");
                if (jSym.getLex() == Lexem.notLexem) res.append("<br>");
                if (jSym.getLex() == Lexem.whiteSpace) {
                    for (int i = 0; i < jSym.str.length(); i++) {

                        if (jSym.str.charAt(i) == ' ') {
                            res.append("&nbsp;");
                        } else if (jSym.str.charAt(i) == '\t') {
                            res.append("&nbsp;&nbsp;&nbsp;");
                        } else res.append(jSym.str.charAt(i));
                    }
                }
                if (jSym == null) break;
                if (jSym.getLex() == Lexem.eof) break;
                if (jSym.getLex() == Lexem.error) {
                    res.append("<span style='color:Red'>").append(jSym.str).append("</span>");
                }

            }
            return res.toString();
        } catch (Exception ex) {
            return ex.getMessage();
        }
    }

    public static void main(String[] args) {
        launch(args);
    }
}