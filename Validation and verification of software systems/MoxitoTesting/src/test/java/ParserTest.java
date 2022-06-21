import org.mockito.Mockito.*;
import org.mockito.Mock;
import org.mockito.Mockito;
import static org.mockito.Mockito.*;
import org.mockito.MockitoAnnotations;
import org.mockito.Spy;
import org.mockito.invocation.InvocationOnMock;
import org.mockito.stubbing.Answer;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;
import java.util.*;

import java.io.IOException;

public class ParserTest {
    private IParser ip;
    FileReader fileReader;
    @Mock
    private Parser p;
    @Spy
    private Parser p_spy;



    @BeforeMethod
    public void setIParser() throws IOException {
        fileReader=new FileReader("C:\\Users\\Mei\\IdeaProjects\\MoxitoTesting\\src\\main\\resources\\source.txt");
        this.ip=new Parser(fileReader.file);
        p=Mockito.mock(Parser.class);
        Parser parser=new Parser(fileReader.file);
        p_spy=Mockito.spy(parser);
    }

    @Test//then answer
    public void testFindToken() {
        p.ReadNextToken();
        verify(p,atLeastOnce()).ReadNextToken();

        Answer<Boolean>answer=invocation -> false;
        when(p.FindToken()).thenAnswer(invocation -> answer);

        p=new Parser(fileReader.file);
        p.ReadNextToken();
        Assert.assertFalse(p.HasNext());
        boolean s=ip.FindToken();
        Assert.assertFalse(s);


    }

    @Test//then return
    public void assertIsSuccessful() throws IOException {
        when(p.IsSuccessful()).thenReturn(p.has);
        when(p.CurrentLexeme()).thenReturn("blank");
        boolean is=false;
        Assert.assertEquals(p.IsSuccessful(),is);

    }

    @Test//then throw
    public void assertCurrentToken() throws IOException {
        when(p.HasNext()).thenReturn(true);
        p=new Parser(fileReader.file);
        Random r = new Random();
        ip=Mockito.mock(Parser.class);
        for(int i=0;i<r.nextInt(10000000);i++){
            p.ReadNextToken();
            ip.ReadNextToken();
        }

        when(ip.HasNext()).thenReturn(p.HasNext());
        when(ip.HasNext()).thenThrow(new RuntimeException("Error"));

        verify(ip,atLeast(10)).ReadNextToken();
    }
    @Test
    public void IsSpyPog(){
        when(p_spy.CurrentToken()).thenReturn(Token.Token_unsignedshiftrightequal);
        when(p_spy.CurrentToken()).thenReturn(Token.Token_unsignedshiftright);
        when(p_spy.FindToken()).thenReturn(false);
        ip.ReadNextToken();
        Assert.assertEquals(ip.IsSuccessful(),p_spy.FindToken());
    }


}