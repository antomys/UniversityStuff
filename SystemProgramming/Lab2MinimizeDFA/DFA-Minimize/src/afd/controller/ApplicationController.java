package afd.controller;

import afd.model.AFD;
import afd.util.MinimizarAFD;
import afd.util.Util;
import afd.view.GraphicsAFD;
import afd.view.WindowMain;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

public class ApplicationController {

    private final String titleWindow = "Work Theory of Computation - DFA Minization";
    private WindowMain guiApplication;
    private AFD afd;
    private GraphicsAFD graphicsAFD;


    public ApplicationController() throws IOException {
        JMenuBar mainMenuBar = new JMenuBar();
        JMenu menu = new JMenu("Algorithm");
        mainMenuBar.add(menu);
        JMenuItem menuItemAFD = new JMenuItem("Minimize DFA");
        menuItemAFD.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                MinimizarAFD afdMin = new MinimizarAFD();
                boolean afdMinimized = afdMin.minimizar(afd);
                graphicsAFD.setAFD(afd);
                graphicsAFD.redrawAFD();
                try {
                    Util.writeFile("./resources/AFDMinimizado.txt", afd.toString());
                } catch (IOException io) {
                    io.printStackTrace();
                }
                JOptionPane.showMessageDialog(null, afdMinimized ? "DFA minimized." : "DFA is minimal.");
            }
        });
        menu.add(menuItemAFD);
        guiApplication = new WindowMain();
        guiApplication.setTitle(this.titleWindow);
        guiApplication.setJMenuBar(mainMenuBar);
        afd = AFD.fromFile("./resources/AFD.txt");
        graphicsAFD = new GraphicsAFD();
        graphicsAFD.setAFD(afd);
        guiApplication.getContentPane().add(graphicsAFD.drawAFD(), BorderLayout.CENTER);
        guiApplication.pack();
        guiApplication.setVisible(true);
    }
}