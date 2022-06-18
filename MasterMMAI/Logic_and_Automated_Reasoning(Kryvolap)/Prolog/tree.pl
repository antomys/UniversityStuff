%%%%%%%%%%%%%%%%%%%%%%%%%%
% Starter code

% tree(Tree)
% "Tree" is a binary tree.

tree(void).
tree(tree(_,Left,Right)) :-  tree(Left),
                             tree(Right).


% tree_member(Element,Tree)
% "Element" is an element of the binary tree "Tree".

tree_member(X,tree(X,_,_)).
tree_member(X,tree(_,Left,_)) :- tree_member(X,Left).
tree_member(X,tree(_,_,Right)) :- tree_member(X,Right).



% preorder(Tree,Pre)
% "Pre" is a list of elements of "Tree" accumulated during a
% preorder traversal.

preorder(tree(X,L,R),Xs) :- preorder(L,Ls), preorder(R,Rs),
                            append([X|Ls],Rs,Xs).
preorder(void,[]).



% append(Xs,Ys,XsYs)
% "XsYs" is the result of appending the lists "Xs" and "Ys".

append([],Ys,Ys).
append([X|Xs],Ys,[X|Zs]) :- append(Xs,Ys,Zs).


% Some sample trees
%
%    tree1       tree2         tree3
%
%      1           4             1
%     / \         / \           / \
%    2   3       5   6         2   3
%                             / \
%                            5   6
%                               /
%                              7
%

tree1(tree(1,tree(2,void,void),tree(3,void,void))).
tree2(tree(4,tree(5,void,void),tree(6,void,void))).
tree3(
        tree(   1,
                tree(   2,
                        tree(5,void,void),
                        tree(   6,
                                tree(7,void,void),
                                void
                        )
                ),
                tree(3,void,void)
        )
).



%%%%%%%%%%%%%%%%%%%%%%%%%%
% Place your code here


% Additional test data
tree4(tree(1,tree(2, tree(4, void, void), tree(5, void, void)),tree(3, void, void))).
tree5(
        tree(   5,
                tree(   2,
                        tree(1,void,void),
                        tree(   4,
                                tree(3,void,void),
                                void
                        )
                ),
                tree(7,void,void)
        )
).
tree6(tree(1,tree(2, tree(2, void, void), tree(5, void, void)),tree(2, void, void))).

% inorder

inorder(void, []).
inorder(tree(X,L,R),Xs) :- 
        inorder(L,Ls), 
        inorder(R,Rs),
        append(Ls,[X|Rs],Xs).
                            
% search
search(X,tree(X,_,_)).
search(X,tree(_,L,_)) :- search(X,L).
search(X,tree(_,_,R)) :- search(X,R).

% subtree
subtree(X, X).
subtree(X, tree(_,L,R)) :- 
        subtree(X, L) 
        ; 
        subtree(X, R).

% sumtree
sumtree(void, 0).
sumtree(tree(X,L,R), Sum) :- 
        sumtree(L, Leftsum), 
        sumtree(R, Rightsum), 
        Sum is X + Leftsum + Rightsum.

% ordered
bigger(_, void).
bigger(X, tree(H,L,R)) :- X > H, bigger(X, L), bigger(X, R).

smaller(_, void).
smaller(X, tree(H,L,R)) :- X < H, smaller(X, L), smaller(X, R).

ordered(void).
ordered(tree(H,L,R)) :- bigger(H, L), smaller(H, R), ordered(L), ordered(R).

% substitute
substitute(_, _, void, void).
substitute(X, Y, tree(X, L, R), tree(Y, L1, R1)) :- 
        substitute(X, Y, L, L1),
        substitute(X, Y, R, R1).
substitute(X, Y, tree(H, L, R), tree(H, L1, R1)) :- 
        substitute(X, Y, L, L1),
        substitute(X, Y, R, R1).

% binsearch
binsearch(tree(H, L, R), Key) :- 
        write(H), nl, Key == H ;
        ( 
                bigger(Key, L) -> binsearch(R, Key) 
                ; binsearch(L, Key) 
        ).

% prettyprint
prettyprint(T) :- prettyprint(T, "").
prettyprint(void, Tabs) :- 
        string_concat(Tabs, "   ", NewTabs),
        write(NewTabs),
        write("X"), 
        nl.
prettyprint(tree(H, L, R), Tabs) :-  
        string_concat(Tabs, "   ", NewTabs),
        prettyprint(R, NewTabs),
        write(Tabs), 
        write(H), 
        nl,
        prettyprint(L, NewTabs).

