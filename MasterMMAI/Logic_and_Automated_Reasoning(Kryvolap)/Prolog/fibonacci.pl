fibonacci_1(N) :- N > 0 -> write(0),nl, N>=1 -> write(1),nl, N>2 -> calc_fi(N, 0, 1).
calc_fi(N, F, F1) :- 
    N1 is N-1, 
    FIB is F+F1, 
    write(FIB),nl, 
    N1 > 2, 
    calc_fi(N1, F1, FIB).

fibonacci_2(X, Z):-
    Z is round((1.618**X - (1-1.618)**X)/sqrt(5)).

/** <examples>
?- fibonacci_1(10),
?- fibonacci_2(10, Z).
*/