package afd.model;

public class Transition {
    private State stateSource;
    private String Character;
    private State stateDestination;

    public Transition(State stateSource, String character, State stateDestination) {
        this.stateSource = stateSource;
        this.Character = character;
        this.stateDestination = stateDestination;
    }

    public State getStateDestination() {
        return stateDestination;
    }

    public State getStateSource() {
        return stateSource;
    }

    public String getCharacter() {
        return Character;
    }
}