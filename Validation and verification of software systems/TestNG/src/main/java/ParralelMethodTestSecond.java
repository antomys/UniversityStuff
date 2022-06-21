import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;
import org.testng.asserts.*;
import java.util.ArrayList;

import static org.hamcrest.MatcherAssert.assertThat;

class ParallelClassesTestSecond
{
    @BeforeClass
    public void beforeClass() {
        long id = Thread.currentThread().getId();
        System.out.println("Before test-class. Thread id is: " + id);
    }

    @Test
    public void testMethodOne() {
        long id = Thread.currentThread().getId();
        System.out.println("Sample test-method One. Thread id is: " + id);
    }

    @Test
    public void testMethodTwo() {
        long id = Thread.currentThread().getId();
        ArrayList<String> results = new ArrayList<String>();
        results.add("Pikachu");
        results.add("erewrew");
        results.removeIf(q->(q.contains("ere")));

        System.out.println("Sample test-method Two. Thread id is: " + id);
    }

    @AfterClass
    public void afterClass() {
        long id = Thread.currentThread().getId();
        System.out.println("After test-class. Thread id is: " + id);
        ArrayList<String> results = new ArrayList<String>();
        results.add("Pikachu");
        Assert.assertEquals(results.get(0),"Pikachu");
    }
}