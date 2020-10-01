package afd;

import afd.controller.ApplicationController;
import java.awt.EventQueue;

public class Main {
    public static void main(String[] args) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                try {
                    new ApplicationController();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }
}