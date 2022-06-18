gcd(A,B,X):- A=0,X=B. % base case
gcd(A,B,X):- B=0,X=A. % base case
gcd(A,B,X):- A>B, gcd(B, A, X).
gcd(A,B,X):- A<B, T is B mod A, gcd(A, T, X).

%   Sample query: gcd(14,21,GCD).
