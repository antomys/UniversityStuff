package sysprogrammingmainalgorithm;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.lang.*;
import java.util.*;
import JavaTeacherLib.*;

public class TestPascalParser{
    private int[] scanerUprTable;
    private int[][] parserUprTable;
    private MyLang lang;
    private byte[] lexema;
    private int lexemaCode;
    private int lexemaLine;
    private int lexemaPos;
    private int lexemaLen;
    private int lexemaNumb;
    private int errors;
    private int ungetChar;
    private String fileData;
    private int ungetLexemaCode;
    private static final int MAX_IDN = 32;
    private BufferedReader fs;
    private int maxLenRecord;
    private StudentClass mylang;
    private String newlexema;
    private LinkedList<Node> language;

    public TestPascalParser(MyLang lang0, String fname) throws IOException {
        this.lang = lang0;
        this.lexema = new byte[180];
        this.lexemaLen = 0;
        this.lexemaLine = 0;
        this.lexemaNumb = 0;
        this.errors = 0;
        this.ungetChar = 0;
        this.ungetLexemaCode = 0;
        this.lexemaPos = 0;
        this.parserUprTable = this.lang.getUprTable();
        this.scanerUprTable = new int[256];
        this.maxLenRecord = 0;

        language=lang0.getLanguarge();
        for(int ii = 0; ii < this.scanerUprTable.length; ++ii) {
            this.scanerUprTable[ii] = 0;
        }

        this.LetterClass("()[];,+-^=*", 1);
        this.LetterClass("abcdefghijklmnopqrstuvwxyz_", 2);
        this.LetterClass("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 2);
        this.LetterClass("0123456789", 3);

        try {
            this.fs = new BufferedReader(new FileReader(fname));
        } catch (Exception var4) {
            System.out.println("Файл програми " + fname + " не відкрито");
            throw new IOException();
        }
    }
    public void parserRecursive2() {
        this.lexemaCode = this.pascalScaner();
        if (this.localrecursive(this.lang.getAxioma())) {
            System.out.println("\nПрограма синтаксично вірна");
        } else {
            while (true) {
                if (this.lexemaCode == -1) {
                    System.out.println("\nПрограма має синтаксичні помилки");
                    break;
                }

                this.lexemaCode = this.pascalScaner();
            }
        }

        try {
            this.fs.close();
        } catch (Exception var7) {
        }
    }

    public void parserRecursive()
    {
        this.lexemaCode = this.pascalScaner();
        if (this.method(this.lang.getAxioma())) {
            System.out.println("\nПрограма синтаксично вірна");
        }
        else
        {
            System.out.println("\nПрограма має синтаксичні помилки");
        }

    }
    public boolean method(int noterm)
    {
        ArrayList<Integer> tempoList=new ArrayList<>();
        for(int i=0;i<language.size();++i) {
            tempoList=mylang.FirstKFollowAi(i);
            if(tempoList.contains(this.lang.getLexemaText(this.lexemaCode)))
            {
                if(!recroole(i))
                {
                    return false;
                }
            }
        }
        return true;
    }
    public boolean recroole(int roole)
    {
        for(int k=0;k<language.get(roole).getRoole().length;k++) {
            if(this.language.get(roole).getRoole()[k]>0){
                if(this.language.get(roole).getRoole()[k]==this.lexemaCode)
                {
                    this.lexemaCode = this.pascalScaner();
                    return true;
                }
            }
            else
            {
                for(int j=0;j<language.get(roole).getRoole().length;j++) {
                    if(language.get(j).getRoole()[0]==this.language.get(roole).getRoole()[k])
                        recroole(j);
                }

            }
        }
        return false;
    }



    private void LetterClass(String uprString, int initCode) {
        for (int ii = 0; ii < uprString.length(); ++ii) {
            this.scanerUprTable[uprString.charAt(ii)] = initCode;
        }

    }

    private void setUngetLexema(int secLexema) {
        this.ungetLexemaCode = secLexema;
    }

    public String getLexemaText() {
        byte[] textLexema = new byte[this.lexemaLen];

        for (int ii = 0; ii < this.lexemaLen; ++ii) {
            textLexema[ii] = this.lexema[ii];
        }

        return new String(textLexema);
    }

    public int pascalScaner() {
        int q = 0;
        this.lexemaLen = 0;
        int litera = 0;
        int lexemaClass = 0;
        String errorLoc = "^[]{},?: ; \t\n\u0000";

        try {
            int tmp1;
            while (this.fs.ready() || this.lexemaPos <= this.maxLenRecord) {
                if (this.fileData == null) {
                    this.fileData = this.fs.readLine();
                    //  System.out.println(this.fileData);
                    ++this.lexemaLine;
                    this.lexemaPos = 0;
                    this.maxLenRecord = this.fileData.length();
                }

                while (this.ungetChar != 0 || this.lexemaPos <= this.maxLenRecord) {
                    if (this.ungetChar != 0) {
                        litera = this.ungetChar;
                        this.ungetChar = 0;
                    } else {
                        if (this.lexemaPos < this.fileData.length()) {
                            litera = this.fileData.charAt(this.lexemaPos);
                        }

                        if (this.lexemaPos == this.fileData.length()) {
                            litera = 10;
                        }

                        ++this.lexemaPos;
                    }

                    litera &= 255;
                    int lexClass = this.scanerUprTable[litera];
                    switch (q) {
                        case 0:
                            if (litera == 10) {
                                ++this.lexemaLine;
                            } else if (litera != 9 && litera != 13 && litera != 32 && litera != 8) {
                                ++this.lexemaNumb;
                                this.lexemaLen = 0;
                                switch (lexClass) {
                                    case 1:
                                        this.lexema[this.lexemaLen++] = (byte) litera;
                                        return this.lang.getLexemaCode(this.lexema, this.lexemaLen, 0);
                                    case 2:
                                        this.lexema[this.lexemaLen++] = (byte) litera;
                                        lexemaClass = 1;
                                        q = 21;
                                        break;
                                    case 3:
                                        this.lexema[this.lexemaLen++] = (byte) litera;
                                        lexemaClass = 2;
                                        q = 31;
                                }

                                if (q == 0) {
                                    this.lexema[this.lexemaLen++] = (byte) litera;
                                    lexemaClass = 5;
                                    if (litera == 46) {
                                        q = 4;
                                    } else if (litera == 58) {
                                        q = 5;
                                    } else if (litera == 47) {
                                        q = 6;
                                    } else if (litera == 62) {
                                        q = 7;
                                    } else if (litera == 60) {
                                        q = 8;
                                    } else if (litera == 33) {
                                        q = 9;
                                    } else if (litera == 39) {
                                        q = 10;
                                    } else {
                                        q = 11;
                                        ++this.errors;
                                    }
                                }
                            }
                        case 1:
                        case 2:
                        case 3:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57:
                        case 58:
                        case 59:
                        case 60:
                        default:
                            break;
                        case 4:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera >= 48 && litera <= 57) {
                                q = 32;
                                break;
                            }

                            if (litera == 46) {
                                return 268435741;
                            }

                            this.ungetChar = litera;
                            --this.lexemaLen;
                            return 268435464;
                        case 5:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera == 61) {
                                return 268435577;
                            }

                            this.ungetChar = litera;
                            --this.lexemaLen;
                            return 268435530;
                        case 6:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera != 42) {
                                this.ungetChar = litera;
                                --this.lexemaLen;
                                return 268435710;
                            }

                            q = 61;
                            break;
                        case 7:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera == 61) {
                                return 268435683;
                            }

                            this.ungetChar = litera;
                            --this.lexemaLen;
                            return 268435681;
                        case 8:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera == 61) {
                                return 268435684;
                            }

                            this.ungetChar = litera;
                            --this.lexemaLen;
                            return 268435680;
                        case 9:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera == 61) {
                                return 268435682;
                            }

                            this.ungetChar = litera;
                            --this.lexemaLen;
                            q = 11;
                            break;
                        case 10:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera == 39) {
                                return 268435472;
                            }
                            break;
                        case 11:
                            if (errorLoc.indexOf(litera) >= 0) {
                                this.ungetChar = litera;
                                q = 0;
                            }
                            break;
                        case 21:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (lexClass != 2 && lexClass != 3) {
                                this.ungetChar = litera;
                                --this.lexemaLen;
                                tmp1 = this.lang.getLexemaCode(this.lexema, this.lexemaLen, 0);
                                if (tmp1 == -1) {
                                    return 268435470;
                                }

                                return tmp1;
                            }
                            break;
                        case 31:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera >= 48 && litera <= 57) {
                                break;
                            }

                            if (litera != 46) {
                                this.ungetChar = litera;
                                --this.lexemaLen;
                                return 268435468;
                            }

                            q = 32;
                            break;
                        case 32:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera >= 48 && litera <= 57) {
                                break;
                            }

                            if (litera != 101 && litera != 69) {
                                this.ungetChar = litera;
                                --this.lexemaLen;
                                return 268435468;
                            }

                            q = 33;
                            break;
                        case 33:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera >= 48 && litera <= 57) {
                                q = 35;
                            } else {
                                if (litera != 43 && litera > 45) {
                                    q = 11;
                                    continue;
                                }

                                q = 34;
                            }
                            break;
                        case 34:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera >= 48 && litera <= 57) {
                                q = 35;
                                break;
                            }

                            q = 11;
                            this.ungetChar = litera;
                            --this.lexemaLen;
                            break;
                        case 35:
                            this.lexema[this.lexemaLen++] = (byte) litera;
                            if (litera >= 48 && litera <= 57) {
                                break;
                            }

                            this.ungetChar = litera;
                            --this.lexemaLen;
                            return 268435468;
                        case 61:
                            if (litera == 42) {
                                q = 62;
                            }
                            break;
                        case 62:
                            if (litera == 47) {
                                q = 0;
                                this.lexemaLen = 0;
                            } else {
                                q = 61;
                            }
                    }
                }

                this.fileData = null;
            }

            if (this.lexemaLen == 0) {
                return -1;
            } else {
                switch (lexemaClass) {
                    case 1:
                        tmp1 = this.lang.getLexemaCode(this.lexema, this.lexemaLen, 0);
                        if (tmp1 != -1) {
                            return tmp1;
                        }

                        return 268435462;
                    case 2:
                        return 268435464;
                    case 3:
                        return -1;
                    case 4:
                        return 268435464;
                    case 5:
                        this.lang.getLexemaCode(this.lexema, this.lexemaLen, 0);
                    default:
                        return -1;
                }
            }
        } catch (IOException var8) {

            return -1;
        }

    }
    private boolean localrecursive(int nonterm) {
        int nontermcol = this.lang.indexNonterminal(nonterm);
        int termCol;
        if (this.lexemaCode == -1) {
            termCol = this.lang.getTerminals().length;
        } else {
            termCol = this.lang.indexTerminal(this.lexemaCode);
        }

        if (this.parserUprTable[nontermcol][termCol] == 0) {

            try {
                this.fs.close();
                return false;
            } catch (Exception var9) {
                return false;
            }
        } else {
            int numrole = this.parserUprTable[nontermcol][termCol];
            int iitmp = 0;
            Node tmp = null;
            Iterator i$ = this.lang.getLanguarge().iterator();

            while (i$.hasNext()) {
                Node tmp1 = (Node) i$.next();
                ++iitmp;
                if (numrole == iitmp) {
                    tmp = tmp1;
                    break;
                }
            }

            int[] pravilo = tmp.getRoole();

            for (int ii = 1; ii < pravilo.length; ++ii) {
                if (pravilo[ii] > 0) {
                    if (pravilo[ii] == this.lexemaCode) {
                        this.lexemaCode = this.pascalScaner();
                    } else {
                        if (!this.lang.getLexemaText(pravilo[ii]).equals("else")) {
                            System.out.println("\nПропущена лексема :" + this.lang.getLexemaText(pravilo[ii]));
                            return false;
                        }

                        ii += 2;
                    }
                } else if (!this.localrecursive(pravilo[ii])) {
                    return false;
                }
            }

            return true;
        }
    }
}