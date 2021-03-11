import java.io.Serializable;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.List;

public class Result implements Serializable {
    List<BigInteger> listOfDivisors;

    public Result() {
        listOfDivisors = new ArrayList<>();
    }

    public List<BigInteger> getList() {
        return listOfDivisors;
    }

    public void add(BigInteger bi) {
        listOfDivisors.add(bi);
    }

    public void setList(List<BigInteger> listOfDivisors) {
        this.listOfDivisors.clear();
        this.listOfDivisors = listOfDivisors;
    }
}
