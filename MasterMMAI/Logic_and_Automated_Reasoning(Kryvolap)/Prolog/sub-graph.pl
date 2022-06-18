subset( S, X) :- XS = [S|T], bfs_subsets(XS,T), member(X,XS).

bfs_subsets( [[] | _], []  ) :- !.
bfs_subsets( [[_]| _], [[]]) :- !.
bfs_subsets( [S  | T],   Q ) :-
    findall( N, select(_, S, N), NS ),
    append( NS,       Z, Q ),
    bfs_subsets(   T, Z ).