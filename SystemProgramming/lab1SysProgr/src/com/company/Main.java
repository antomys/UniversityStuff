package com.company;
import java.io.*;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Main
{
    public static String readFileAsString(String fileName) throws Exception {
        String data = "";
        data = new String(Files.readAllBytes(Paths.get(fileName)));
        return data;
    }

    public static String regexChecker(String Theregex, String strcompare, List<String> string) {

        Pattern pattern = Pattern.compile(Theregex);
        Matcher matcher = pattern.matcher(strcompare);
        System.out.println("State:" + matcher.find());
        System.out.println("Input:");
        while (matcher.find()) {
            string.add(matcher.group());
            if (matcher.group().length() != 0) {
                System.out.println(matcher.group().trim());

            }
            //System.out.println(matcher.start()); System.out.println(matcher.end());

        }
        return strcompare.toString();
    }

    public static String removeDup(String remdep) {
        Set<String> duplicates = new HashSet<>();
        String[] words = remdep.split("[^\\w\\d]");
        //System.out.println("         "+remdep);
        Set<String> set = new HashSet<>();

        for (String word : words)
        {

            if (!duplicates.add(word))
            {
                set.add(word);
            }
        }
        //System.out.println("              "+duplicates);
        return duplicates.toString();

    }

    public static void main(String[] args) throws Exception {
        String data = readFileAsString("C:\\Users\\Igor Volokhovych\\Desktop\\The Hunger Games.txt");
        String theRegex = "\\b[aeouiy]+\\b";
        List<String> allMatches = new ArrayList<String>();
        regexChecker(theRegex,data,allMatches);
        //System.out.println("          "+allMatches.toString());
        System.out.println(removeDup(allMatches.toString()));

    }
}






