remove_dups([], []).   % Empty list is empty list with dups removed
remove_dups([X], [X]). % Single element list is itself with dups removed

% The result of removing duplicates from `[X,X|T]` should be the same
%   as the result of removing duplicates from `[X|T]`
remove_dups([X,X|T], [X|R]) :-
    remove_dups([X|T], [X|R]).

% The result of removing duplicates from `[X,Y|T]` where X and Y are different
%   should be the result [X|R] where R is the result of removing duplicates
%   from [Y|T]
remove_dups([X,Y|T], [X|R]) :-
    X \== Y,
    remove_dups([Y|T], R).
    
/*
% Sample queries
remove_dups([x,x,y,y,y,x],X)
*/
