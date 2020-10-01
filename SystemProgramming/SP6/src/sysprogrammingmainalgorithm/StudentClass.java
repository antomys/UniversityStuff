package sysprogrammingmainalgorithm;

import java.io.IOException;
import java.lang.*;
import java.util.*;
import JavaTeacherLib.*;
public class StudentClass extends MyLang {
    private LinkedList<Node> language;



    public StudentClass(String fileLang, int llk1) {
        super(fileLang, llk1);
        language=super.getLanguarge();
    }



    public void LanguageIsEmpty() {
        if (this.getNonTerminals().length == 0) {
            System.out.println("Мова не містить правил");
            return;
        } else {
            int[] prodtmp = new int[this.getNonTerminals().length];
            int count = 0;
            Iterator i$ = this.language.iterator();
            boolean is_end=false;
            Node tmp;
            int axioma=getAxioma();

            while(i$.hasNext()) {
                tmp = (Node)i$.next();
                tmp.setTeg(0);
            }
            int ii;
            boolean upr;
            int[] rool1;
            label117:
            do {
                upr = false;
                i$ = this.language.iterator();

                while(true) {
                    int ii1;
                    do {
                        do {
                            if (!i$.hasNext()) {
                                continue label117;
                            }

                            tmp = (Node)i$.next();
                            rool1 = tmp.getRoole();
                        } while(tmp.getTeg() == 1);

                        for(ii = 1; ii < rool1.length; ++ii) {
                            if (rool1[ii] <= 0) {
                                for(ii1 = 0; ii1 < count && prodtmp[ii1] != rool1[ii]; ++ii1) {
                                }

                                if (ii1 == count) {
                                    break;
                                }
                            }
                        }
                    } while(ii != rool1.length);

                    for(ii1 = 0; ii1 < count && prodtmp[ii1] != rool1[0]; ++ii1) {
                    }

                    if (ii1 == count) {
                        prodtmp[count++] = rool1[0];
                    }

                    tmp.setTeg(1);
                    upr = true;
                }
            } while(upr);
            Iterator oi$ = this.language.iterator();
            while(oi$.hasNext()) {
                tmp = (Node)oi$.next();
                if(tmp.getTeg()==1){
                    if(tmp.getRoole()[0]==axioma)
                        is_end=true;
                }
            }

            if (is_end) {
                System.out.print("L(G) – не порожня множина\n");

            } else {
                System.out.print("L(G) – порожня множина\n");

            }
        }

    }

    public ArrayList<Integer> FirstKFollowAi(int rule){
        int[] term = this.getTerminals();
        int[] nonterm = this.getNonTerminals();

        LlkContext[] firstContext=this.firstK();
        this.setFirstK(firstContext);

        LlkContext [] followContext = this.followK();
        this.setFollowK(followContext);

        int tempo=this.getLlkConst();
        ArrayList<Integer> tempoList=new ArrayList<>();
        for(int i=0;i<language.size();++i){

            for(int k=0;k<language.get(i).getRoole().length;k++){
                if(k==0){
                    for(int j=0;j<nonterm.length;++j){
                        if(nonterm[j]==language.get(i).getRoole()[0]){
                            LlkContext temp=followContext[j];
                            for(int q=0;q<temp.getWord(0).length;++q){
                                tempoList.add(temp.getWord(0)[q]);
                            }
                        }
                    }
                }
                else {
                    if(this.language.get(i).getRoole()[k]>0){
                        tempoList.add(this.language.get(i).getRoole()[k]);
                    }
                    else{
                        for(int j=0;j<nonterm.length;++j){
                            if(nonterm[j]==language.get(i).getRoole()[k]){
                                LlkContext temp=firstContext[j];
                                for(int q=0;q<temp.getWord(0).length;++q){
                                    tempoList.add(temp.getWord(0)[q]);
                                }
                            }
                        }
                    }
                }
            }
            System.out.println(i+1+" rule:");
            /*for(int q=0;q<tempo&&q<tempoList.size();++q){
                System.out.print(this.getLexemaText(tempoList.get(q)));
            }*/
            for(int q=tempoList.size()-1,h=0;h<tempo&&q>=0;++q,++h){
                System.out.print(this.getLexemaText(tempoList.get(q)));
            }
            System.out.println("\n");
            tempoList.clear();
        }
        return tempoList;
    }
    private void asd() throws IOException
    {
        MyLang testLang=new MyLang("D://test1.txt",1);
        TestPascalParser pascalParser=  new TestPascalParser(testLang,"D://Example3.txt");
        int i=1;
        pascalParser.parserRecursive();
    }

}