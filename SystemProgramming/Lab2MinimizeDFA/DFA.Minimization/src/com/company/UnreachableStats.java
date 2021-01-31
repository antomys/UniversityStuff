package com.company;

import java.io.*;
import java.util.ArrayList;
import java.util.Scanner;

public class UnreachableStats {

    public static void main(String[] args) {
        try {
            File file = new File("ex1.txt");
            Scanner sc = new Scanner(file);
            ArrayList alphabet=new ArrayList();
            String line = sc.nextLine();
            for(int i=0;i<line.length();i++){
                if(!Symbol.isSymbol((line.charAt(i)))){
                    alphabet.add(line.charAt(i));
                }
            }
            line=sc.nextLine();
            ArrayList<Integer> states= new ArrayList();
            for(int i=0;i<line.length();i++){
                if(!Symbol.isSymbol(line.charAt(i))){
                    states.add((int)(line.charAt(i)-'0'));
                }
            }
            int first_state;
            first_state=sc.nextInt();
            ArrayList final_states=new ArrayList();
            int count_final_states=sc.nextInt();
            for(int i=0;i<count_final_states;i++){
                final_states.add(sc.nextInt());
            }
            ArrayList<Transition> transitions= new ArrayList<>();

            while(sc.hasNextLine()) {
                Transition tempo = new Transition();
                tempo.state_1 = sc.nextInt();
                tempo.symbol = sc.next();
                tempo.state_2 = sc.nextInt();
                transitions.add(tempo);
            }
            int size=states.size();
            int [][]incidence_matrix=new int[states.get(states.size()-1)+1][states.get(states.size()-1)+1];
            for(int i=0;i<transitions.size();i++){
                incidence_matrix[transitions.get(i).state_1][transitions.get(i).state_2]=1;
            }
            /*for(int i=0;i<incidence_matrix[0].length;i++){
                for(int j=0;j<incidence_matrix[0].length;j++){
                    System.out.print(incidence_matrix[i][j]+" ");
                }
                System.out.println();
            }*/
            boolean[] reachable=new boolean[states.get(states.size()-1)+1];
            System.out.println("Unreachable states:");
            reachable=DFS.DFS(first_state,incidence_matrix,states,reachable);
            for(int i=0;i<states.get(states.size()-1)+1;i++){
                if(!reachable[i]&&states.contains(i)){
                    System.out.println(i);
                    DFS.delete_state(transitions,i);
                    states.remove((Integer) i);
                }
            }
            System.out.println("Dead end states:");
            boolean[] deadend=new boolean[states.get(states.size()-1)+1];
            for(int i=0;i<states.get(states.size()-1)+1;i++){
                if(DFS.DFS_dead_end(i,i,incidence_matrix,states,final_states)&&states.contains(i)){
                    deadend[i]=true;
                }
            }
            for(int i=0;i<states.get(states.size()-1)+1;i++){
                if(!deadend[i]&&states.contains(i)){
                    System.out.println(i);
                    DFS.delete_state(transitions,i);
                    states.remove((Integer) i);
                }
            }
            //ArrayList<String> test=DFS.DFS_word_builder(0,incidence_matrix,transitions,states,"");
            //System.out.println(test);

            ArrayList<EqualityClasses> equalityClasses=new ArrayList<>();
            for(int i=0;i<states.size();i++){
                for(int j=0;j<states.size();j++){
                    DFS.tempowordlul.clear();
                    DFS.DFS_word_builder(i,incidence_matrix,transitions,states,"",0);
                    ArrayList<String> tempo1=new ArrayList<>();
                    for(int k=0;k<DFS.tempowordlul.size();k++){
                        tempo1.add(DFS.tempowordlul.get(k));
                    }

                    DFS.tempowordlul.clear();
                    DFS.DFS_word_builder(j,incidence_matrix,transitions,states,"",0);
                    ArrayList<String> tempo2=new ArrayList<>();
                    for(int k=0;k<DFS.tempowordlul.size();k++){
                        tempo2.add(DFS.tempowordlul.get(k));
                    }
                    if(tempo1.equals(tempo2) &&i!=j){
                        EqualityClasses tempo=new EqualityClasses();
                        tempo.state1=i;
                        tempo.state2=j;
                        equalityClasses.add(tempo);
                    }
                }
            }
            /*for(int i=0;i<equalityClasses.size();i++){
                for(int j=0;j<equalityClasses.size();j++){
                    if(equalityClasses.get(i).state1== equalityClasses.get(i).state2&&equalityClasses.get(j).state2==equalityClasses.get(i).state1){
                        equalityClasses.remove(j);

                    }
                }
            }*/
            /*for(int i=0;i<equalityClasses.size();i++){
                System.out.println(equalityClasses.get(i));
            }*/
            for(int i=0;i<equalityClasses.size();i++){
                if(equalityClasses.get(i).state1<equalityClasses.get(i).state2){
                    int tempo=equalityClasses.get(i).state1;
                    int tempo2=equalityClasses.get(i).state2;
                    int tempo3=tempo;
                    tempo=tempo2;
                    tempo2=tempo3;
                    EqualityClasses neww=new EqualityClasses();
                    neww.state1=tempo;
                    neww.state2=tempo2;
                    equalityClasses.set(i,neww);
                }
            }
            for(int i=0;i<equalityClasses.size();i++){
                for(int j=0;j<equalityClasses.size();j++){
                    if(equalityClasses.get(i).equals(equalityClasses.get(j))){
                        equalityClasses.remove(j);
                    }
                }
            }

            for(int i=0;i<equalityClasses.size();i++){
                transitions=DFS.delete_class_equivalence(transitions,equalityClasses.get(i).state1,equalityClasses.get(i).state2);
            }

            DFS.OutputAutomat(alphabet,states,first_state,final_states,transitions);


        }
        catch (Exception e){
            System.out.println("Something went wrong");
        }
    }
    protected static class Symbol{
        public static boolean isSymbol(char s){
            if(s>='A'&&s<='Z'||s>='a'&&s<='z'||s>='А'&&s<='Я'||s>='а'&&s<='я'||s<='9'&&s>='0')return false;
            else return  true;
        }
    }

    static class DFS{
        static boolean[] is_dead_end=new boolean[1000];
        static ArrayList<String> tempowordlul=new ArrayList<>();

        public static boolean[] DFS(int v,int[][]matrix,ArrayList states,boolean[]visited){
            visited[v]=true;
            for(int nv=0;nv<matrix.length;nv++){
                if(matrix[v][nv]>0&&!visited[nv]){
                    DFS(nv,matrix,states,visited);
                }
            }
            return visited;
        }


        public static boolean DFS_dead_end(int initial,int v,int[][]matrix,ArrayList states,ArrayList final_states){
            if(isFinal(final_states,v)){
                is_dead_end[initial]=true;
                return true;
            }
            for(int nv=0;nv<matrix.length;nv++){
                if(matrix[v][nv]>0&&v!=nv){
                    DFS_dead_end(initial, nv,matrix,states,final_states);
                }
            }
            if(is_dead_end[initial])return true;
            return false;
        }

        public static void  DFS_word_builder(int v,int[][]matrix,ArrayList<Transition> transitions,ArrayList states,String word,int depth){
            if(depth>40)return;
            tempowordlul.add(word);
            for(int nv=0;nv<matrix.length;nv++){
                if(matrix[v][nv]>0&&word.length()<50){
                    ArrayList<Character>temp=new ArrayList<>();
                    temp=find_transition(transitions,v,nv);
                    for(int i=0;i<temp.size();i++){
                        String tempo= (word.concat(temp.get(i).toString()));
                        tempowordlul.add(tempo);
                    }
                    DFS_word_builder(nv,matrix,transitions,states,word,depth+1);
                }
            }

        }


        public static boolean isFinal(ArrayList states,int state){
            for(int i=0;i<states.size();i++){
                if(state==(int)states.get(i))return true;
            }
            return false;
        }
        public static ArrayList<Character> find_transition(ArrayList<Transition> transitions,int state1,int state2){
            ArrayList<Character> tempo=new ArrayList<>();
            for(int i=0;i<transitions.size();++i){
                if(transitions.get(i).state_1==state1&&transitions.get(i).state_2==state2){
                    tempo.add(transitions.get(i).symbol.charAt(0));
                }
            }
            return tempo;
        }

        public static ArrayList<Transition> delete_state(ArrayList<Transition> transitions, int state){
            for(int i=0;i<transitions.size();i++){
                if(transitions.get(i).state_2==state||transitions.get(i).state_1==state){
                    transitions.remove(i);
                    i--;
                }
            }
            return transitions;
        }

        public static ArrayList<Transition> delete_class_equivalence(ArrayList<Transition>transitions,int state1,int state2){
            ArrayList<Integer>mark=new ArrayList<>();

            for(int i=0;i<transitions.size();i++){
                if(transitions.get(i).state_2==state2){
                    Transition tempo=new Transition();
                    tempo.state_1=transitions.get(i).state_1;
                    tempo.symbol=transitions.get(i).symbol;
                    tempo.state_2=state1;
                    transitions.add(tempo);
                    mark.add(state2);
                }
            }
            for(int i=0;i<mark.size();i++){
                transitions=delete_state(transitions,mark.get(i));
            }
            return transitions;
        }

        public static void OutputAutomat(ArrayList alphabet,ArrayList<Integer> states,int first_state,ArrayList final_states,ArrayList<Transition> transitions){
            System.out.println("Minimized finite state machine:\n_______________________");
            for(int i=0;i<alphabet.size()-1;i++){
                System.out.print(alphabet.get(i)+", ");
            }
            System.out.println(alphabet.get(alphabet.size()-1));

            for(int i=0;i<states.size()-1;i++){
                System.out.print(states.get(i)+", ");
            }
            System.out.println(states.get(states.size()-1));

            System.out.println(first_state);
            System.out.print(final_states.size()+" ");
            for(int i=0;i<final_states.size()-1;i++){
                System.out.print(final_states.get(i)+" ");
            }
            System.out.println(final_states.get(final_states.size()-1));


            for(int i=0;i<transitions.size()-1;i++){
                System.out.println(transitions.get(i).state_1+" "+transitions.get(i).symbol+" "+transitions.get(i).state_2);
            }
            System.out.print(transitions.get(transitions.size()-1).state_1+" "+transitions.get(transitions.size()-1).symbol+" "+transitions.get(transitions.size()-1).state_2);
        }
    }
}
