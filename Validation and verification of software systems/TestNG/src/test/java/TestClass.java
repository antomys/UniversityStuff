
import org.testng.Assert;
import org.testng.annotations.*;
import org.testng.annotations.Test;

import static org.testng.AssertJUnit.assertEquals;

public class TestClass {
    private Class aClass;
    @BeforeSuite
    public void Init() {
        aClass = new Class();
    }

    @Test
    public void testSum() throws Exception {
        Assert.assertEquals(5, aClass.sum(2,3));
    }

    @DataProvider
    public Object[][] data() {
        return new Object[][] { { 0, 0 }, { 1, 1 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 5 }, { 6, 8 } };
    }

    @Test(dataProvider = "data")
    public void test(int fInput, int fExpected) {
        assertEquals(fExpected, Class.F_compute(fInput));

    }

    @BeforeMethod
    public void beforeMethod() {
        long id = Thread.currentThread().getId();
        System.out.println("Before test-method. Thread id is: " + id);
    }

    @Test
    public void testMethodsOne() {
        long id = Thread.currentThread().getId();
        System.out.println("Simple test-method One. Thread id is: " + id);
    }

    @Test
    public void testMethodsTwo() {
        long id = Thread.currentThread().getId();
        System.out.println("Simple test-method Two. Thread id is: " + id);
    }

    @AfterMethod
    public void afterMethod() {
        long id = Thread.currentThread().getId();
        System.out.println("After test-method. Thread id is: " + id);
    }
}