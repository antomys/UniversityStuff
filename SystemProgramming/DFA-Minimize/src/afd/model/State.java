package afd.model;

public class State implements Comparable<State> {
    private String stateName;
    private Boolean stateEnd;
    private Boolean stateBegin;

    public State(String name) {
        this.stateName = name;
    }

    public String getName() {
        return stateName;
    }

    public Boolean isStateEnd() {
        return (stateEnd == Boolean.TRUE);
    }

    public Boolean isStateBegin() {
        return (stateBegin == Boolean.TRUE);
    }

    public void setStateEnd(Boolean end) {
        this.stateEnd = end;
    }

    public void setStateBegin(Boolean begin) {
        this.stateBegin = begin;
    }

    public int compareTo(State other)
    {
        return getName().compareTo(other.getName());
    }
}