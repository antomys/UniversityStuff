package afd.view;

import javax.swing.*;
import java.awt.*;

public class WindowMain extends JFrame {

    private static final long serialVersionUID = 1L;
    private JPanel contentPane;
    public WindowMain() {
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setBounds(100, 100, 450, 300);
        setMinimumSize(new Dimension(450, 500));
        setLayout(new BorderLayout());
        contentPane = new JPanel();
        contentPane.setBorder(BorderFactory.createTitledBorder("Viewing DFA"));
        contentPane.setLayout(new BorderLayout(0, 0));
        contentPane.setBackground(Color.WHITE);
        setContentPane(contentPane);
    }
}